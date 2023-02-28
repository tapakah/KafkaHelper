using System;

namespace KafkaHelpers.Model
{
    public class DateTimeFormatProvider : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            return this;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            DateTime val = (DateTime)arg;

            return val.ToString("HH:mm:ss");
        }
    }
}