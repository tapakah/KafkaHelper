using System;

namespace KafkaHelpers.Model
{
    public class DeliviryReport
    {
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public int Count { get; set; }
        public string MessageSize { get; set; }

        public DeliviryReport() { }
    }
}