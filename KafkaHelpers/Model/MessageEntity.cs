using Confluent.Kafka;
using System;
using System.ComponentModel;

namespace KafkaHelpers
{
    public class MessageEntity<TKey>
    {
        private int id;
        private TKey key;
        private string keystring;
        private string topic;
        private string message;
        private bool defaultJsonParse = true;

        public int Id
        { get { return id; } set { this.id = value; } }

        public TKey Key
        {
            get
            {
                return this.key;
            }

            set
            {
                this.key = value;
            }
        }

        public string KeyString
        {
            get { return keystring; }
            set
            {
                this.keystring = value;
                this.key = ConvertStringToGenericType<TKey>(keystring);
            }
        }

        public string Topic
        { get { return topic; } set { this.topic = value; } }

        public string Message
        { get { return message; } set { this.message = value; } }

        public bool DefaultJsonParse
        { get { return defaultJsonParse; } set { this.defaultJsonParse = value; } }

        public T ConvertStringToGenericType<T>(string stringValue)
        {
            Type type = typeof(T);

            if (Nullable.GetUnderlyingType(type) != null)
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(type);
                return (T)typeConverter.ConvertFromString(stringValue);
            }
            else if (type == typeof(Ignore))
            {
                return default(T);
            }

            return (T)Convert.ChangeType(stringValue, type);
        }
    }
}