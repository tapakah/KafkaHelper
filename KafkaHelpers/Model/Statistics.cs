using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

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

	public enum GroupType
	{
		Second = 1000,
		Min = 60000,
		Hour = 60000 * 60
	}


	public class Statistics
	{
		public List<DataTopic> TimeValues { get; set; }
		private int Count;

		public Statistics(int cnt)
		{
			TimeValues = new List<DataTopic>();
			Count = cnt;
		}

		public void DrowChart(Chart chrt, GroupType grp = GroupType.Second)
		{
			DataPoint _point;

			chrt.Series.Clear();
			chrt.Titles.Clear();
			chrt.Titles.Add($"Total messages {TimeValues.Count}");
			chrt.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM HH:mm:ss";
			chrt.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 6);
			chrt.ChartAreas[0].AxisX.MajorTickMark.Interval = 1;
			chrt.ChartAreas[0].AxisX.MajorTickMark.IntervalType = DateTimeIntervalType.Seconds;
			chrt.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
			chrt.ChartAreas[0].CursorX.AutoScroll = true;
			chrt.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;

			foreach (string _topic in TimeValues.Select(x => x.Topic).Distinct<string>())
			{
				Series series = new Series();
				series = chrt.Series.Add(_topic);
				series.ChartType = SeriesChartType.Point;
				series.XValueType = ChartValueType.DateTime;
				series.YValueType = ChartValueType.Int32;

				int cnt = 0;
				DateTime? current = null;

				foreach (var _value in TimeValues.Where(x => x.Topic == _topic).OrderBy(z => z.Value).Select(y => y.Value))
				{
					if (current is null) { current = _value; }

					if (current.Value.Second == _value.Second)
					{
						cnt++;
					}
					else
					{
						_point = new DataPoint();
						_point.SetValueXY(current.Value.Trim(TimeSpan.TicksPerSecond).ToOADate(), cnt);
						_point.ToolTip = string.Format("{0} : {1} messages", current.Value.ToString("dd/MM HH:mm:ss"), cnt);
						series.Points.Add(_point);

						while (current.Value.Second < _value.Second)
						{
							current = current.Value.AddSeconds(1);

							_point = new DataPoint();
							_point.SetValueXY(current.Value.Trim(TimeSpan.TicksPerSecond).ToOADate(), 0);
							series.Points.Add(_point);
						}
						
						current = _value;

						cnt = 0;
					}

				}
			}

		}
	}
}
