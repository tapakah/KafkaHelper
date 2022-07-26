using KafkaHelpers.Model;
using System;

namespace KafkaHelpers
{
    public static class RowFormatter
    {
        private static readonly string[] suffixes =
            { "Bytes", "KB", "MB", "GB", "TB", "PB" };

        public static string FormatSize(Int64 bytes)
        {
            int counter = 0;
            decimal number = (decimal)bytes;

            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;

                counter++;
            }
            return string.Format("{0:n1}{1}", number, suffixes[counter]);
        }

        public static GridRow CreateRow(GridRow rw, Terms terms)
        {
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
            }
            catch (Exception ex)
            {
                rw.Id = -1;
                rw.Timestamp = DateTime.UtcNow;
                rw.Topic = "KEY:Error grid";
                rw.Key = string.Format("SYSTEM:{0}", ex.Message);
                rw.Value = "InternalError";
            }

            rw.Timestamp = TimeZoneInfo.ConvertTimeFromUtc(rw.Timestamp, TimeZoneInfo.Local).ToLocalTime();

            return rw;
        }
    }
}