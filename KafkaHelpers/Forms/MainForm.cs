using Confluent.Kafka;
using KafkaHelpers.Model;
using Polly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafkaHelpers
{
	public partial class MainForm : Form
	{
		private CancellationTokenSource cancelSource;
		private static readonly string Caption = "KafkaHelper by TaPaKaH";
		private static readonly string STATUS_READ = "READ";
		private static readonly string STATUS_WAIT = "WAIT";
		private static int ID_COUNTER;
		private static readonly int MAX_MESSAGE_LENGTH = 30000;
		private static readonly int MAX_TOOLTIP_TEXT = 2000;
		private static readonly int DEFAULT_ROWS = 2000;
		private static readonly int DEFAULT_COUNTER = 100;
		private static readonly int DELAY_TIMER_STATUS = 1000;
		private string KAFKA_SERVER = string.Empty;
		private readonly string KAFKA_SERVER_LIST = string.Empty;
		private readonly string KAFKA_TOPICS = string.Empty;
		private readonly string KAFKA_CHKED_TOPICS = string.Empty;
		private readonly Terms terms = new Terms();
		private readonly KafkaHelper _kafka;
		private static System.Threading.Timer tTimer;
		public static event EventHandler<string> StatusTextChanged;
		private readonly List<string> TOPICS = new List<string>();
		private readonly List<string> TopicsSubscriber = new List<string>();
		public static event EventHandler<string> ConsumerStatusTextChanged;

		private AsyncPolicy CreateConsumingPolicy()
		{
			return Policy
				.Handle<System.Exception>()
				.WaitAndRetryForeverAsync( //Sychronous policy does not honor cancellationToken while waiting.
					(tryNumber) => TimeSpan.FromSeconds(Math.Min((tryNumber * 2) - 1, 60)),
					(ex, tryNumber, delayTime) =>
					{
						try
						{
							AddRow(new GridRow(-1, "KEY:InternalError", "SYSTEM:Error consumer", ex.Message, DateTime.UtcNow));
						}
						catch
						{
							MessageBox.Show(ex.Message);
						}
					}
				);
		}

		private string GetSettingValue(string paramName)
		{
			return ConfigurationManager.AppSettings[paramName];
		}

		private static void AddUpdateAppSettings(string key, string value)
		{
			try
			{
				var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				var settings = configFile.AppSettings.Settings;
				if (settings[key] == null)
				{
					settings.Add(key, value);
				}
				else
				{
					settings[key].Value = value;
				}
				configFile.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
			}
			catch (ConfigurationErrorsException)
			{
				Console.WriteLine("Error writing app settings");
			}
		}

		public MainForm()
		{
			InitializeComponent();

			KAFKA_SERVER_LIST = GetSettingValue("KafkaServerList");

			if (!string.IsNullOrEmpty(KAFKA_SERVER_LIST))
			{
				List<string> kafkaServers = KAFKA_SERVER_LIST.Split(',').ToList();

				foreach (string kafkaserver in kafkaServers)
				{
					ctbKafkaServer.Items.Add(kafkaserver);
				}
			}

			if (ctbKafkaServer.Items.Count > 0)
			{
				if (int.TryParse(GetSettingValue("SelectedServerIndex"), out int _index))
				{
					ctbKafkaServer.SelectedIndex = _index;
				}
				else
				{
					ctbKafkaServer.SelectedItem = ctbKafkaServer.Items[0];
				}

				KAFKA_SERVER = ctbKafkaServer.Text;
			}
			dateTimeStart.Enabled = dateTimeEnd.Enabled = chkTimestamp.Checked;

			KAFKA_TOPICS = GetSettingValue("KafkaReadTopics");

			if (!string.IsNullOrEmpty(KAFKA_TOPICS))
			{
				TOPICS = KAFKA_TOPICS.Split(',').Select(x=>x.Trim()).ToList();

				cmbProducerTopic.DataSource = TOPICS;

				foreach (string topic in TOPICS)
				{
					chklTopics.Items.Add(topic.Trim());
				}
			}

			KAFKA_CHKED_TOPICS = GetSettingValue("KafkaTopics");

			if (!string.IsNullOrEmpty(KAFKA_CHKED_TOPICS))
			{
				List<string> topics = KAFKA_CHKED_TOPICS.Split(',').Select(x=>x.Trim()).ToList();

				for (int i = 0; i < chklTopics.Items.Count; i++)
				{
					if (topics.FirstOrDefault(x => x.Contains(chklTopics.Items[i].ToString())) != null)
					{
						chklTopics.SetItemCheckState(i, CheckState.Checked);
					}
				}
			}

			btnCheckAll.Enabled = btnUncheckAll.Enabled = btnSubscribe.Enabled = btnSubscribe2.Enabled = true;
			btnUnSubscribe.Enabled = btnUnSubscribe2.Enabled = false;

			_toolStripLabel.Text = "UNSUBSCRIBE";

			_kafka = new KafkaHelper(CreateConsumingPolicy());

			terms.Counter = DEFAULT_COUNTER;

			tbCounter.Text = terms.Counter.ToString();

			terms.MaxRows = DEFAULT_ROWS;

			tbMaxRows.Text = terms.MaxRows.ToString();

			_dataGridViewSubscriber.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));

		}


		private void CheckWorker(bool toDo)
		{
			for (int i = 0; i <= (chklTopics.Items.Count - 1); i++)
			{
				if (toDo)
				{
					chklTopics.SetItemCheckState(i, CheckState.Checked);
				}
				else
				{
					chklTopics.SetItemCheckState(i, CheckState.Unchecked);
				}
			}
		}

		private void btnCheckAll_Click(object sender, EventArgs e)
		{
			CheckWorker(true);
		}

		private void btnUncheckAll_Click(object sender, EventArgs e)
		{
			CheckWorker(false);
		}

		private async void btnSubscribe_Click(object sender, EventArgs e)
		{
			await Subscribe();
		}

		private async Task Subscribe()
		{
			AddUpdateAppSettings("SelectedServerIndex", ctbKafkaServer.SelectedIndex.ToString());
			tabControl.SelectedTab = tabPageSubsriber;
			cancelSource = new CancellationTokenSource();
			_toolStripStatus.Text = string.Empty;
			_toolStripProgressBar.Value = 0;

			if (chkTimestamp.Checked)
			{
				terms.Start = dateTimeStart.Value;
				terms.End = dateTimeEnd.Value;
			}
			else
			{
				terms.Start = null;
				terms.End = null;
			}

			if (!string.IsNullOrEmpty(tbKey.Text))
			{
				terms.Key = tbKey.Text;
			}
			else
			{
				terms.Key = null;
			}

			if (!string.IsNullOrEmpty(tbValue.Text))
			{
				terms.Value = tbValue.Text;
			}
			else
			{
				terms.Value = null;
			}

			if (!string.IsNullOrEmpty(tbCounter.Text))
			{
				terms.Counter = Convert.ToInt32(tbCounter.Text);
			}
			else
			{
				terms.Counter = DEFAULT_COUNTER;
			}

			if (!string.IsNullOrEmpty(tbMaxRows.Text))
			{
				terms.MaxRows = Convert.ToInt32(tbMaxRows.Text);
			}
			else
			{
				terms.MaxRows = DEFAULT_ROWS;
			}

			_consumerDataSet.Messages.Clear();
			ctbKafkaServer.Enabled = btnCheckAll.Enabled = btnUncheckAll.Enabled = btnSubscribe.Enabled = btnSubscribe2.Enabled = chklTopics.Enabled = false;
			btnUnSubscribe.Enabled = btnUnSubscribe2.Enabled = true;

			TopicsSubscriber.Clear();
			TopicsSubscriber.AddRange(chklTopics.CheckedItems.Cast<string>().ToList());

			if (TopicsSubscriber.Count == 0) { return; }

			AddUpdateAppSettings("KafkaTopics", String.Join(", ", TopicsSubscriber.ToArray()));

			_toolStripLabel.Text = STATUS_READ;
			_tsStatusConsumer.Text = string.Empty;
			_toolStripKafkaServerValue.Text = KAFKA_SERVER;

			var taskConsume = Task.Run(() => ActivateConsume(cancelSource.Token), cancelSource.Token);

			try
			{
				await taskConsume;
			}
			catch (OperationCanceledException ex)
			{
				Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {ex.Message}");

				ctbKafkaServer.Enabled = btnCheckAll.Enabled = btnUncheckAll.Enabled = btnSubscribe.Enabled = btnSubscribe2.Enabled = chklTopics.Enabled = true;
				btnUnSubscribe.Enabled = btnUnSubscribe2.Enabled = false;
				_toolStripLabel.Text = "ERROR";
			}
		}

		private void TickTimer(object state)
		{
			SetConsumerStatusText(STATUS_WAIT);
			SetStatusText(ID_COUNTER.ToString());
		}

		private async void ActivateConsume(CancellationToken stoppingToken)
		{
			ID_COUNTER = 0;

			StatusTextChanged += (sender, text) =>
			{
				SetStatusText(text);
			};

			ConsumerStatusTextChanged += (sender, text) =>
			{
				SetConsumerStatusText(text);
			};

			using (IConsumer<string, string> consumer = KafkaHelper.CreateKafkaConsumer(KAFKA_SERVER))
			{
				try
				{
					consumer.Subscribe(TopicsSubscriber);

					AddRow(new GridRow(-1, "Info", "KEY:Consumer Subscribe", string.Format("SYSTEM:{0}", string.Join(", ", TopicsSubscriber.ToArray())), DateTime.UtcNow));

					stoppingToken.ThrowIfCancellationRequested();

					while (!stoppingToken.IsCancellationRequested)
					{
						tTimer = new System.Threading.Timer(
										new TimerCallback(TickTimer),
										null,
										DELAY_TIMER_STATUS,
										0);

						var consumeResult = await _kafka.ConsumeMessage(consumer, stoppingToken);

						if (consumeResult == null || consumeResult.IsPartitionEOF)
						{
							continue;
						}

						tTimer.Change(
										Timeout.Infinite,
										Timeout.Infinite
									);

						ID_COUNTER++;

						if (ID_COUNTER % terms.Counter == 0) StatusTextChanged?.Invoke(this, ID_COUNTER.ToString());
						if (_toolStripLabel.Text != STATUS_READ) ConsumerStatusTextChanged?.Invoke(this, STATUS_READ);

						try
						{
							AddRow(new GridRow(ID_COUNTER, consumeResult.Topic, consumeResult.Message.Key ?? "-1", consumeResult.Message.Value, consumeResult.Message.Timestamp.UtcDateTime));
						}
						catch
						{
							AddRow(new GridRow(ID_COUNTER, consumeResult.Topic, consumeResult.Message.Key ?? "is null", consumeResult.Message.Value ?? "is null", DateTime.UtcNow));
						}

						if (stoppingToken.IsCancellationRequested)
						{
							stoppingToken.ThrowIfCancellationRequested();
						}
					}
				}
				catch (OperationCanceledException)
				{
				}
				catch (Exception ex)
				{
					AddRow(new GridRow(-1, "InternalError", "KEY:Error:", string.Format("SYSTEM:{0}", ex.Message), DateTime.UtcNow));
				}
				finally
				{
					consumer.Unsubscribe();
				}
			}
		}

		private void SetConsumerStatusText(string text)
		{
			Invoke((MethodInvoker)
			delegate
			{
				_toolStripLabel.Text = text;
			});
		}

		private void SetStatusText(string text)
		{
			Invoke((MethodInvoker)
			delegate
			{
				_toolStripStatus.Text = text;
				_toolStripProgressBar.Value = (_toolStripProgressBar.Value + 1) % 100;
			});
		}

		private void AddRow(GridRow row)
		{
			_dataGridViewSubscriber.Invoke(new Action(() =>
			{
			var rw = RowFormatter.CreateRow(row, terms);

			if (rw != null)
			{
				if (_consumerDataSet.Messages.Rows.Count > terms.MaxRows)
				{
						UnSubscribe();
						return;
					}

					var message = string.IsNullOrEmpty(rw.Value) ? string.Empty : rw.Value;

					if (message.Length > MAX_MESSAGE_LENGTH)
					{
						message = message.Substring(0, message.Length > MAX_MESSAGE_LENGTH ? MAX_MESSAGE_LENGTH : message.Length) + $"[Message was substringed ({message.Length} to {MAX_MESSAGE_LENGTH} )]";
					}

					ConsumerDataSet.MessagesRow nRow = _consumerDataSet.Messages.NewMessagesRow();

					nRow.Id = rw.Id;
					nRow.Value = message;
					nRow.Recived = rw.Timestamp;
					nRow.Key = rw.Key;
					nRow.Topic = rw.Topic;
					nRow.Size = RowFormatter.FormatSize(System.Text.ASCIIEncoding.Unicode.GetByteCount(string.IsNullOrEmpty(rw.Value) ? string.Empty : rw.Value));

					_consumerDataSet.Messages.Rows.Add(nRow);
				}
			}
			));
		}

		private void btnUnSubscribe_Click(object sender, EventArgs e)
		{
			UnSubscribe();
		}

		private void UnSubscribe()
		{
			cancelSource.Cancel();

			ctbKafkaServer.Enabled = btnCheckAll.Enabled = btnUncheckAll.Enabled = btnSubscribe.Enabled = btnSubscribe2.Enabled = chklTopics.Enabled = true;
			btnUnSubscribe.Enabled = btnUnSubscribe2.Enabled = false;

			_toolStripLabel.Text = "UNSUBSCRIBE";
			_tsStatusConsumer.Text = string.Empty;
			_toolStripStatus.Text = string.Empty;
			_toolStripProgressBar.Value = 0;
		}

		private void btnReadTopics_Click(object sender, EventArgs e)
		{
			ReadTopics();
		}

		private void ReadTopics()
		{
			var config = new ProducerConfig
			{
				BootstrapServers = ctbKafkaServer.Text
			};

			try
			{
				using (var adminClient = new AdminClientBuilder(config).Build())
				{
					var topicsMeta = adminClient.GetMetadata(TimeSpan.FromSeconds(20)).Topics;

					TOPICS.Clear();
					TOPICS.AddRange(topicsMeta.Select(x => x.Topic.Trim()).ToList());

					FillTopicBox(TOPICS);
				}
			}
			catch (Confluent.Kafka.KafkaException ex)
			{
				MessageBox.Show(
								ex.Message,
								"Error Kafka",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error
								);
			}
			finally
			{
				AddUpdateAppSettings("SelectedServerIndex", ctbKafkaServer.SelectedIndex.ToString());
			}
		}

		private void FillTopicBox(List<string> topics)
		{
			topics.Sort();

			chklTopics.Items.Clear();

			cmbProducerTopic.DataSource = topics;

			foreach (string topic in topics)
			{
				chklTopics.Items.Add(topic);
			}

			AddUpdateAppSettings("KafkaReadTopics", String.Join(", ", topics.ToArray()));
		}

		private void dataGridViewSubscriber_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex > 0)
			{
				var r = _consumerDataSet.Messages.Rows[e.RowIndex];

				using (MessageDetailForm detail = new MessageDetailForm())
				{
					detail.Entity.Id = Convert.ToInt32(r["Id"]);
					detail.Entity.KeyString = r["Key"].ToString();
					detail.Entity.Topic = r["Topic"].ToString();
					detail.Entity.Message = r["Value"].ToString();
					detail.Entity.DefaultJsonParse = chbDefaultJsonParse.Checked;
					detail.ShowDialog(this);
				}
			}
		}

		private void chkTimestamp_CheckedChanged(object sender, EventArgs e)
		{
			dateTimeStart.Enabled = dateTimeEnd.Enabled = chkTimestamp.Checked;
		}

		private void ctbKafkaServer_Validating(object sender, CancelEventArgs e)
		{
			KAFKA_SERVER = ctbKafkaServer.Text.Trim();
			this.Text = string.Concat(Caption, " : ", KAFKA_SERVER);
			if (!ctbKafkaServer.Items.Contains(ctbKafkaServer.Text.Trim()))
			{
				ctbKafkaServer.Items.Add(ctbKafkaServer.Text.Trim());
			}

			string[] items = new string[ctbKafkaServer.Items.Count];

			for (int i = 0; i < ctbKafkaServer.Items.Count; i++)
			{
				items[i] = ctbKafkaServer.Items[i].ToString();
			}

			AddUpdateAppSettings("KafkaServerList", String.Join(",", items.ToArray()));
		}

		private async void btnSendMessage_Click(object sender, EventArgs e)
		{
			int _cnt = Convert.ToInt32(cntToSend.Value);
			string messageText = string.Empty;
			if (string.IsNullOrEmpty(textBoxFileToSend.Text))
			{
				messageText = tbProducerValue.Text;
			}
			else
			{
				if (File.Exists(textBoxFileToSend.Text))
				{
					messageText = File.ReadAllText(textBoxFileToSend.Text);
				}
			}

			if (!Int64.TryParse(tbProducerKey.Text, out long keyValue))
			{
				keyValue = 0;
			}

			MessageDetailEntity _e = new MessageDetailEntity
			{
				Topic = cmbProducerTopic.Text,
				Key = keyValue,
				Message = messageText
			};

			var cancelSource = new CancellationTokenSource();

			try
			{
				using (IProducer<long, string> producer = _kafka.CreateKafkaProducer(KAFKA_SERVER))
				{
					DeliveryResult<long, string> _result = null;
					DeliviryStatus ds = new DeliviryStatus() { Start = DateTime.Now, Count = 0 };

					for (int i = 1; i <= _cnt; i++)
					{
						_result = await _kafka.SendToKafka(_e.Key, _e.Topic, _e.Message, producer, cancelSource.Token);

						if (_result.Status != PersistenceStatus.Persisted) break;
						ds.Count++;
					}

					if (_result.Status != PersistenceStatus.Persisted)
					{
						MessageBox.Show($"Send to kafka failed {_result.Status}");
					}
					else
					{
						ds.Finish = DateTime.Now;

						using (DeliviryReportForm report = new DeliviryReportForm())
						{
							report.Report = ds;
							report.ShowDialog(this);
						}
					}
				}
			}
			catch (Exception)
			{

				throw;
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			_consumerDataSet.Messages.Rows.Clear();
		}

		private void btnMax_Click(object sender, EventArgs e)
		{
			dateTimeEnd.Value = dateTimeEnd.MaxDate;
		}

		private void tbCounter_TextChanged(object sender, EventArgs e)
		{
			if (System.Text.RegularExpressions.Regex.IsMatch(tbCounter.Text, "[^0-9]"))
			{
				tbCounter.Text = tbCounter.Text.Remove(tbCounter.Text.Length - 1);
			}

			if (tbCounter.Text.Length == 0)
			{
				tbCounter.Text = DEFAULT_COUNTER.ToString();
			}
		}

		private void ctbKafkaServer_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13)
			{
				ReadTopics();
			}
		}

		private void btnView_Click(object sender, EventArgs e)
		{
			if (_dataGridViewSubscriber.SelectedRows != null)
			{
				var r = _consumerDataSet.Messages.Rows[_dataGridViewSubscriber.SelectedRows[0].Index];

				using (MessageDetailForm detail = new MessageDetailForm())
				{
					detail.Entity.Id = Convert.ToInt32(r["Id"]);
					detail.Entity.KeyString = r["Key"].ToString();
					detail.Entity.Topic = r["Topic"].ToString();
					detail.Entity.Message = r["Value"].ToString();

					detail.ShowDialog(this);
				}
			}
		}

		private void tbProducerKey_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
				(e.KeyChar != '.'))
			{
				e.Handled = true;
			}

			// only allow one decimal point
			if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
			{
				e.Handled = true;
			}
		}

		private void buttonFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog open = new OpenFileDialog();

			open.Filter = "Text File | *.txt";

			if (open.ShowDialog() == DialogResult.OK)
			{

				textBoxFileToSend.Text = open.FileName;
			}
			else
			{
				textBoxFileToSend.Text = String.Empty;
			}
		}


		private void _dataGridViewSubscriber_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
		{
			if (e.RowIndex > 0)
			{
				var r = _consumerDataSet.Messages.Rows[e.RowIndex];
				var message = r["Value"].ToString();

				if (message.Length > MAX_TOOLTIP_TEXT) { message = "Message very big for tooltip :("; }

				if (chbDefaultJsonParse.Checked)
				{
					message = (string)JsonHelper.FormatJsonText(message);
				}


				e.ToolTipText = message;


			}
		}

		private void tbFilterTopics_TextChanged(object sender, EventArgs e)
		{
			var filter = tbFilterTopics.Text.Trim().ToUpper();

			var chkitems = chklTopics.CheckedItems.Cast<string>().ToList();

			List<string> result;

			if (!string.IsNullOrEmpty(filter))
			{
				result = TOPICS.Where(y => y.ToUpper().Contains(filter)).ToList();
			}
			else
			{
				result = TOPICS;
			}

			result.AddRange(chkitems.Distinct());

			List<string> uniq = new List<string>();

			uniq.AddRange(result.Select(x=>x).Distinct().ToList());

			uniq.Sort();

			chklTopics.Items.Clear();

			cmbProducerTopic.DataSource = result;

			foreach (string topic in uniq)
			{
				chklTopics.Items.Add(topic);

				if (chkitems.Find(x => x == topic) != null)
				{
					chklTopics.SetItemChecked(chklTopics.Items.IndexOf(topic), true);
				}

			}
		}

		private void tbTop_TextChanged(object sender, EventArgs e)
		{
			if (System.Text.RegularExpressions.Regex.IsMatch(tbMaxRows.Text, "[^0-9]"))
			{
				tbMaxRows.Text = tbMaxRows.Text.Remove(tbCounter.Text.Length - 1);
			}

			if (tbMaxRows.Text.Length == 0)
			{
				tbMaxRows.Text = DEFAULT_ROWS.ToString();
			}
		}
	}
}