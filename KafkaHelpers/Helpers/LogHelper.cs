using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafkaHelpers.Helpers
{
	public static class LogHelper
	{
		private static StringBuilder log = new StringBuilder();

		public static void AppendLog(string message)
		{
			log.AppendLine(message) ;
		}

		public static void ClearLog()
		{
			log.Clear();
		}

		public static string GetLog()
		{
			return log.ToString();
		}
	}
}
