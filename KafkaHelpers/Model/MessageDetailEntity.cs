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
        private string key;
        private string topic;
        private string message;


        public int Id { get { return id; } set { this.id = value; } }
        public string Key 
        { get 
            { 
            if (this.key == null) 
                { return string.Empty; }
            else
                {
                    return this.key ;
                }
            }

            set { this.key = value; } 
        }
        public string Topic { get { return topic; } set { this.topic = value; } }
        public string Message { get { return message; } set { this.message = value; } }
    }
}
