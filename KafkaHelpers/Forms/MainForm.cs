using Confluent.Kafka;
using KafkaHelpers.Model;
using Moq;
using Polly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafkaHelpers
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource cancelSource;
        private static string Caption = "KafkaHelper by TaPaKaH";
        private static int DEFAULT_COUNTER = 100;
        private static int DELAY_TIMER_STATUS = 1000;
        private string kafkaServer = string.Empty;
        private readonly string kafkaServerList = string.Empty;
        private readonly string kafkaTopics = string.Empty;
        private readonly string kafkaChkTopics = string.Empty;
        private List<string> topics = new List<string>();
        private Terms terms = new Terms();
        private KafkaHelper _kafka;
        private static System.Threading.Timer TTimer;
        private static string STATUS_READ = "READ";
        private static string STATUS_WAIT = "WAIT";
        private static int ID_COUNTER;

        public static event EventHandler<string> StatusTextChanged;

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

            kafkaServerList = GetSettingValue("KafkaServerList");

            if (!string.IsNullOrEmpty(kafkaServerList))
            {
                List<string> kafkaServers = kafkaServerList.Split(',').ToList();

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

                kafkaServer = ctbKafkaServer.Text;
            }
            dateTimeStart.Enabled = dateTimeEnd.Enabled = chkTimestamp.Checked;

            kafkaTopics = GetSettingValue("KafkaReadTopics");

            if (!string.IsNullOrEmpty(kafkaTopics))
            {
                List<string> topics = kafkaTopics.Split(',').ToList();

                foreach (string topic in topics)
                {
                    cmbProducerTopic.Items.Add(topic.Trim());
                    chklTopics.Items.Add(topic.Trim());
                }
            }

            kafkaChkTopics = GetSettingValue("KafkaTopics");

            if (!string.IsNullOrEmpty(kafkaChkTopics))
            {
                List<string> topics = kafkaChkTopics.Split(',').ToList();

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
            dataGridViewSubscriber.AutoGenerateColumns = true;

            dataGridViewSubscriber.Columns.Add("Id", "Id");
            dataGridViewSubscriber.Columns["Id"].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 7, FontStyle.Regular);
            dataGridViewSubscriber.Columns["Id"].Width = 40;
            dataGridViewSubscriber.Columns["Id"].ValueType = typeof(long);

            dataGridViewSubscriber.Columns.Add("Reccived", "TimeStamp");
            dataGridViewSubscriber.Columns["Reccived"].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 7, FontStyle.Regular);
            dataGridViewSubscriber.Columns["Reccived"].Width = 150;
            dataGridViewSubscriber.Columns["Reccived"].ValueType = typeof(DateTime);

            dataGridViewSubscriber.Columns.Add("Topic", "Topic");
            dataGridViewSubscriber.Columns["Topic"].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 7, FontStyle.Bold);
            dataGridViewSubscriber.Columns["Topic"].Width = 150;
            dataGridViewSubscriber.Columns["Topic"].ValueType = typeof(string);

            dataGridViewSubscriber.Columns.Add("Key", "Key");
            dataGridViewSubscriber.Columns["Key"].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 7, FontStyle.Regular);
            dataGridViewSubscriber.Columns["Key"].Width = 100;
            dataGridViewSubscriber.Columns["Key"].ValueType = typeof(string);

            dataGridViewSubscriber.Columns.Add("Value", "Value");
            dataGridViewSubscriber.Columns["Value"].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 7, FontStyle.Regular);
            dataGridViewSubscriber.Columns["Value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSubscriber.Columns["Value"].ValueType = typeof(string);

            _toolStripLabel.Text = "UNSUBSCRIBE";

            _kafka = new KafkaHelper(CreateConsumingPolicy());

            terms.Counter = DEFAULT_COUNTER;

            tbCounter.Text = terms.Counter.ToString();
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

            _consumerDataSet.Messages.Clear();
            dataGridViewSubscriber.Rows.Clear();
            ctbKafkaServer.Enabled = btnCheckAll.Enabled = btnUncheckAll.Enabled = btnSubscribe.Enabled = btnSubscribe2.Enabled = chklTopics.Enabled = false;
            btnUnSubscribe.Enabled = btnUnSubscribe2.Enabled = true;

            topics.Clear();

            for (int i = 0; i <= chklTopics.CheckedItems.Count - 1; i++)
            {
                topics.Add(chklTopics.CheckedItems[i].ToString().Trim());
            }

            if (topics.Count == 0) { return; }

            AddUpdateAppSettings("KafkaTopics", String.Join(", ", topics.ToArray()));

            _toolStripLabel.Text = STATUS_READ;
            _tsStatusConsumer.Text = string.Empty;
            _toolStripKafkaServerValue.Text = kafkaServer;

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

            using (IConsumer<string, string> consumer = KafkaHelper.CreateKafkaConsumer(kafkaServer))
            {
                try
                {
                    consumer.Subscribe(topics);

                    AddRow(new GridRow(-1, "Info", "KEY:Consumer Subscribe", string.Format("SYSTEM:{0}", string.Join(", ", topics.ToArray())), DateTime.UtcNow));

                    stoppingToken.ThrowIfCancellationRequested();

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        TTimer = new System.Threading.Timer(
                                        new TimerCallback(TickTimer),
                                        null,
                                        DELAY_TIMER_STATUS,
                                        0);

                        var consumeResult = await _kafka.ConsumeMessage(consumer, stoppingToken);

                        if (consumeResult == null || consumeResult.IsPartitionEOF)
                        {
                            continue;
                        }

                        TTimer.Change(
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
            dataGridViewSubscriber.Invoke(new Action(() =>
            {
                DataRow _r = RowFormatter.CreateRow(row, terms);

                if (_r != null) dataGridViewSubscriber.Rows.Add(_r.ItemArray);
            }
            ));
        }

        private void btnUnSubscribe_Click(object sender, EventArgs e)
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
            var config = new ProducerConfig
            {
                BootstrapServers = ctbKafkaServer.Text
            };

            try
            {
                using (var adminClient = new AdminClientBuilder(config).Build())
                {
                    List<TopicMetadata> topicsMeta = adminClient.GetMetadata(TimeSpan.FromSeconds(20)).Topics;
                    chklTopics.Items.Clear();
                    cmbProducerTopic.Items.Clear();
                    List<string> _t = new List<string>();

                    foreach (TopicMetadata topic in topicsMeta)
                    {
                        _t.Add(topic.Topic.Trim());
                    }
                    _t.Sort();

                    foreach (string topic in _t)
                    {
                        cmbProducerTopic.Items.Add(topic);
                        chklTopics.Items.Add(topic);
                    }

                    AddUpdateAppSettings("KafkaReadTopics", String.Join(", ", _t.ToArray()));
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

        private void dataGridViewSubscriber_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                DataGridViewRow r = dataGridViewSubscriber.Rows[e.RowIndex];

                using (MessageDetailForm detail = new MessageDetailForm())
                {
                    detail.Entity.Id = Convert.ToInt32(r.Cells["Id"]?.Value);
                    detail.Entity.Key = r.Cells["Key"].Value?.ToString();
                    detail.Entity.Topic = r.Cells["Topic"].Value?.ToString();
                    detail.Entity.Message = r.Cells["Value"].Value?.ToString();

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
            kafkaServer = ctbKafkaServer.Text.Trim();
            this.Text = string.Concat(Caption, " : ", kafkaServer);
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

            MessageDetailEntity _e = new MessageDetailEntity();
            _e.Topic = cmbProducerTopic.Text;
            _e.Key = tbProducerKey.Text;
            _e.Message = tbProducerValue.Text;

            if (cancelSource == null)
            { cancelSource = new CancellationTokenSource(); }

            var mock = new Mock<IProducer<long, string>>();
            var Testproducer = mock.Object;

            var produceCount = 0;
            var flushCount = 0;

            mock.Setup(m =>
                       m.Produce(It.IsAny<string>(), It.IsAny<Message<long, string>>(), It.IsAny<Action<DeliveryReport<long, string>>>()))
                .Callback<string, Message<long, string>, Action<DeliveryReport<long, string>>>((topic, message, action) =>
                    {
                        var result = new DeliveryReport<long, string>
                        {
                            Topic = topic,
                            Partition = 0,
                            Offset = 0,
                            Error = new Error(ErrorCode.NoError),
                            Message = message
                        };
                        action.Invoke(result);
                        produceCount += 1;
                    });
            mock.Setup(m => m.Flush(It.IsAny<TimeSpan>())).Returns(0).Callback(() => flushCount += 1);

            try
            {
                using (IProducer<long, string> producer = _kafka.CreateKafkaProducer(kafkaServer))
                {
                    
                    bool _result = false;

                    DeliviryReport sr = new DeliviryReport
                    {
                        Start = DateTime.Now,
                        MessageSize = RowFormatter.FormatSize(System.Text.ASCIIEncoding.Unicode.GetByteCount(_e.Message))
                    };

                    for (int i = 1; i <= _cnt; i++)
                    {
                        _result = await _kafka.SendToKafka(_e.Key, _e.Topic, _e.Message, producer, cancelSource.Token);
                        
                        sr.Count = i;

                        if (!_result) break;
                    }

                    if (!_result)
                    {
                        MessageBox.Show("Send to kafka failed");
                    }
                    else
                    {
                        sr.Finish = DateTime.Now;

                        using (DeliviryReportForm report = new DeliviryReportForm())
                        {
                            report.Report = sr;
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
            dataGridViewSubscriber.Rows.Clear();
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
        }
    }
}