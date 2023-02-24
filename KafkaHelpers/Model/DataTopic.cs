using System;
using System.Collections.Generic;

namespace KafkaHelpers.Model
{
	public class DataTopic
	{
		private string topic { get; set; }
		private DateTime value { get; set; }

		public string Topic { get { return topic; } }
		public DateTime Value { get { return value; } }

		public DataTopic(string _topic, DateTime _value)
		{
			topic = _topic;
			value = _value;
		}
	}

	
}
