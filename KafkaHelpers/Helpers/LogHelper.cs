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
		private const int MAXLINES = 2000;
		private static StringBuilder log = new StringBuilder();

		public static void AppendLog(string message)
		{
			log.AppendLine($"[{DateTime.Now.ToString("dd.MM.yy HH:mm:ss:ffff")}]{message}");
			RemoveExcessLines(log, MAXLINES);
		}

		public static void ClearLog()
		{
			log.Clear();
		}

		public static string GetLog()
		{
			return log.ToString();
		}

		static void RemoveExcessLines(StringBuilder sb, int maxLines)
		{
			try
			{
				if (sb == null || sb.Length == 0)
				{
					throw new InvalidOperationException("No lines were found in the log.");
				}
				else
				{
					string content = sb.ToString().Replace("\r\n", "\n").Replace("\r", "\n");
					string[] lines = content.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

					if (lines.Length > maxLines)
					{
						string[] lastLines = lines.Skip(lines.Length - maxLines).ToArray();

						sb.Clear();
						foreach (string line in lastLines)
						{
							sb.AppendLine(line);
						}
					}
				}
			}
			catch { }
		}
	}
}
