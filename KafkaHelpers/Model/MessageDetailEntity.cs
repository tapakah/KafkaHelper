using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaHelpers
{
    public class MessageDetailEntity
    {
        private int id;
        private long key;
        private string keystring;
        private string topic;
        private string message;


        public int Id { get { return id; } set { this.id = value; } }
        public long Key
        {
            get
            {

                return this.key;
            }

            set { this.key = value; }
        }
        public string KeyString { get { return keystring; } set { this.keystring = value; } }
        public string Topic { get { return topic; } set { this.topic = value; } }
        public string Message { get { return message; } set { this.message = value; } }
    }
}
