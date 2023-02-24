using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using Telerik.Charting;
using Telerik.WinControls.UI;

namespace KafkaHelpers.Model
{
	public enum GroupType
	{
		Second = 1000,
		Min = 60000,
		Hour = 60000 * 60
	}

	public static class Statistics
	{
		public static List<DataTopic> TimeValues { get; set; } = new List<DataTopic>();

		private static IEnumerable<DateTime> GetTopicValues(this List<DataTopic> data, string topic)
		{
			foreach (var _value in data.Where(x => x.Topic == topic).OrderBy(z => z.Value).Select(y => y.Value))
			{
				yield return _value;
			}
		}

		private static IEnumerable<string> GetTopic(this List<DataTopic> data)
		{
			foreach (string _topic in TimeValues.Select(x => x.Topic).Distinct<string>())
			{
				yield return _topic;
			}
		}
		
		public static void Clear()
		{
			TimeValues.Clear();
		}

		private static void tooltipController_DataPointTooltipTextNeeded(object sender, DataPointTooltipTextNeededEventArgs e)
		{
			var dot = e.DataPoint as CategoricalDataPoint;
			e.Text = string.Format("time: {0} \ncount: {1}", (dot.Category as DateTime?).Value.ToString("dd/MM/yy HH:mm:ss"), dot.Value.ToString());
		}

		private static void trackballController_TextNeeded(object sender, TextNeededEventArgs e)
		{
			e.Text = string.Empty;
			
			foreach (var point in e.Points)
			{
				var dot = point.DataPoint as CategoricalDataPoint;
				var series = point.Series;
				e.Text += string.Format("{0} : {1} - {2} \n", series.Name, (dot.Category as DateTime?).Value.ToString("dd/MM/yy HH:mm:ss"), dot.Value.ToString());
			}
		}

		public static void DrowChart(RadChartView chrt, GroupType grp = GroupType.Second)
		{
			int maxCnt = 0;
			chrt.Series.Clear();

			chrt.Controllers.Add(new ChartTooltipController());
			chrt.ShowToolTip = true;
			ChartTooltipController tooltipController = new ChartTooltipController();
			tooltipController.DataPointTooltipTextNeeded += tooltipController_DataPointTooltipTextNeeded;
			chrt.Controllers.Add(tooltipController);

			ChartPanZoomController panZoomController = new ChartPanZoomController();
			panZoomController.PanZoomMode = ChartPanZoomMode.Horizontal;
			chrt.Controllers.Add(panZoomController);
			chrt.ShowPanZoom = true;


			ChartTrackballController trackballController = new ChartTrackballController();
			trackballController.TextNeeded += trackballController_TextNeeded;
			chrt.Controllers.Add(trackballController);

			DateTimeCategoricalAxis categoricalAxis = new DateTimeCategoricalAxis();
			categoricalAxis.DateTimeComponent = DateTimeComponent.Second;
			categoricalAxis.PlotMode = AxisPlotMode.OnTicks;
			categoricalAxis.LabelFitMode = AxisLabelFitMode.Rotate;
			categoricalAxis.LabelRotationAngle = 310;			
			categoricalAxis.LabelFormatProvider = new DateTimeFormatProvider();
			
			int GetTickInterval()
			{
				int cnt = TimeValues.Select(x => x.Value.ToString("ddmmss")).Distinct<string>().Count();
				return cnt/20 ;
			}

			categoricalAxis.MajorTickInterval = GetTickInterval();

			foreach (string _topic in TimeValues.GetTopic())
			{
				LineSeries graph = new LineSeries();
				graph.Name = _topic;
				graph.PointSize = new SizeF(5, 5);

				int cnt = 0;
				DateTime? current = null;

				foreach (var _value in TimeValues.GetTopicValues(_topic))
				{
					if (current is null) { current = _value; }

					if (current.Value.Second == _value.Second)
					{
						cnt++;
					}
					else
					{
						var dot = new CategoricalDataPoint(cnt, current.Value.Trim(TimeSpan.TicksPerSecond));

						graph.DataPoints.Add(dot);

						current = _value;

						maxCnt = maxCnt < cnt ? cnt : maxCnt;

						cnt = 1;
					}

				}

				if (cnt != 0)
				{
					var dot = new CategoricalDataPoint(cnt, current.Value.Trim(TimeSpan.TicksPerSecond));

					graph.DataPoints.Add(dot);
				}

				graph.HorizontalAxis = categoricalAxis;
				chrt.Series.Add(graph);
			}

			LinearAxis verticalAxis = chrt.Axes[1] as LinearAxis;

			int getStep(int max)
			{
				if (max < 500) { return 10; }

				if (max < 1000) { return 50; }

				if (max > 1000 && max < 10000) return 100;

				return 1000;
			};

			verticalAxis.MajorStep = getStep(maxCnt);

			verticalAxis.Minimum = 0;
		}
	}
}
