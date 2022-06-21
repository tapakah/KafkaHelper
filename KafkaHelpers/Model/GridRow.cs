using System;

namespace KafkaHelpers
{
    public class GridRow
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime Timestamp { get; set; }

        public GridRow(int _id, string _topic, string _key, string _value, DateTime _time)
        {
            Id = _id;
            Topic = _topic;
            Key = _key;
            Value = _value;
            Timestamp = _time;
        }
    }
}