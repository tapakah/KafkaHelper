using KafkaHelpers.Model;
using System;
using System.Data;

namespace KafkaHelpers
{
    public static class RowFormatter
    {
        private static ConsumerDataSet ds = new ConsumerDataSet();
        
        static readonly string[] suffixes =
            { "Bytes", "KB", "MB", "GB", "TB", "PB" };

        public static string FormatSize(Int64 bytes)
        {
            int counter = 0;
            decimal number = (decimal)bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number = number / 1024;
                counter++;
            }
            return string.Format("{0:n1}{1}", number, suffixes[counter]);
        }

        public static DataRow CreateRow(GridRow rw, Terms terms)
        {
            DataRow row = ds.Messages.NewRow();

            try
            {
                var _timeStamp = TimeZoneInfo.ConvertTimeFromUtc(rw.Timestamp, TimeZoneInfo.Local);

                if (terms.Start != null && terms.End != null && rw.Id != -1)
                {
                    if ((_timeStamp <= terms.Start) || (_timeStamp >= terms.End))
                    {
                        return null;
                    }
                }

                if (!string.IsNullOrEmpty(terms.Key))
                {
                    if (string.IsNullOrEmpty(rw.Key)
                        || rw.Key != terms.Key
                        && !rw.Key.Contains("KEY:"))
                    {
                        return null;
                    }
                }

                if (!string.IsNullOrEmpty(terms.Value))
                {
                    if (string.IsNullOrEmpty(rw.Value)
                        || !rw.Value.Contains(terms.Value)
                        && !rw.Value.Contains("SYSTEM:"))
                    {
                        return null;
                    }
                }

                row["Id"] = rw.Id;
                row["Recived"] = _timeStamp;
                row["Topic"] = rw.Topic;
                row["Key"] = rw.Key;
                row["Value"] = rw.Value;
            }
            catch (Exception ex)
            {
                row["Id"] = -1;
                row["Recived"] = "InternalError";
                row["Topic"] = "KEY:Error grid";
                row["Key"] = string.Format("SYSTEM:{0}", ex.Message);
                row["Value"] = DateTime.UtcNow;
            }

            return row;
        }
    }
}