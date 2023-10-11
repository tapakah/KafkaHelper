using Confluent.Kafka;
using KafkaHelpers.Model;
using Polly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
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
        private string KAFKA_KEY_TYPE = string.Empty;

        private Terms setting = new Terms();
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
                TOPICS = KAFKA_TOPICS.Split(',').Select(x => x.Trim()).ToList();

                cmbProducerTopic.DataSource = TOPICS;

                foreach (string topic in TOPICS)
                {
                    chklTopics.Items.Add(topic.Trim());
                }
            }

            KAFKA_CHKED_TOPICS = GetSettingValue("KafkaTopics");

            if (!string.IsNullOrEmpty(KAFKA_CHKED_TOPICS))
            {
                List<string> topics = KAFKA_CHKED_TOPICS.Split(',').Select(x => x.Trim()).ToList();

                for (int i = 0; i < chklTopics.Items.Count; i++)
                {
                    if (topics.FirstOrDefault(x => x.Contains(chklTopics.Items[i].ToString())) != null)
                    {
                        chklTopics.SetItemCheckState(i, CheckState.Checked);
                    }
                }
            }

            KAFKA_KEY_TYPE = GetSettingValue("KafkaKeyType");

            switch (KAFKA_KEY_TYPE)
            {
                case "ignore":
                    rbKeyIgnore.IsChecked = true;
                    break;
                case "long":
                    rbKeyLong.IsChecked = true;
                    break;
                case "int":
                    rbKeyInt.IsChecked = true;
                    break;
                default:
                    rbKeyString.IsChecked = true;
                    break;
            }

            rbMessageType_CheckStateChanged(null, null);

            btnCheckAll.Enabled = btnUncheckAll.Enabled = btnSubscribe.Enabled = btnSubscribe2.Enabled = true;
            btnUnSubscribe.Enabled = btnUnSubscribe2.Enabled = false;

            _toolStripLabel.Text = "UNSUBSCRIBE";

            _kafka = new KafkaHelper(CreateConsumingPolicy());

            setting.Counter = DEFAULT_COUNTER;

            tbCounter.Text = setting.Counter.ToString();

            setting.MaxRows = DEFAULT_ROWS;

            tbMaxRows.Text = setting.MaxRows.ToString();

            _dataGridViewSubscriber.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));

            this._radChartView.Series.Clear();
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
            _toolStatisticsLabel.Text = "0";

            if (chkTimestamp.Checked)
            {
                setting.Start = dateTimeStart.Value;
                setting.End = dateTimeEnd.Value;
            }
            else
            {
                setting.Start = null;
                setting.End = null;
            }

            if (!string.IsNullOrEmpty(tbKey.Text))
            {
                setting.Key = tbKey.Text;
            }
            else
            {
                setting.Key = null;
            }

            if (!string.IsNullOrEmpty(tbValue.Text))
            {
                setting.Message = tbValue.Text;
            }
            else
            {
                setting.Message = null;
            }

            if (!string.IsNullOrEmpty(tbCounter.Text))
            {
                setting.Counter = Convert.ToInt32(tbCounter.Text);
            }
            else
            {
                setting.Counter = DEFAULT_COUNTER;
            }

            if (!string.IsNullOrEmpty(tbMaxRows.Text))
            {
                setting.MaxRows = Convert.ToInt32(tbMaxRows.Text);
            }
            else
            {
                setting.MaxRows = DEFAULT_ROWS;
            }

            setting.IsStatistic = !radModeSwitch.Value;

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

            Statistics.Clear();

            if (rbKeyString.IsChecked) KAFKA_KEY_TYPE = "string";
            else if (rbKeyLong.IsChecked) KAFKA_KEY_TYPE = "long";
            else if (rbKeyInt.IsChecked) KAFKA_KEY_TYPE = "int";
            else if (rbKeyIgnore.IsChecked) KAFKA_KEY_TYPE = "ignore";

            var taskConsume = Task.Run(() =>
            {
                switch (KAFKA_KEY_TYPE)
                {
                    case "ignore":
                        ActivateConsume<Ignore, string>(cancelSource.Token);
                        break;
                    case "int":
                        ActivateConsume<int, string>(cancelSource.Token);
                        break;

                    case "long":
                        ActivateConsume<long, string>(cancelSource.Token);
                        break;
                    default:
                        ActivateConsume<string, string>(cancelSource.Token);
                        break;
                }
            }, cancelSource.Token);

            AddUpdateAppSettings("KafkaKeyType", KAFKA_KEY_TYPE);

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

        private async void ActivateConsume<TKey, TMessage>(CancellationToken stoppingToken)
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

            using (IConsumer<TKey, TMessage> consumer = KafkaHelper.CreateKafkaConsumer<TKey, TMessage>(KAFKA_SERVER))
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

                        if (ID_COUNTER % setting.Counter == 0) StatusTextChanged?.Invoke(this, ID_COUNTER.ToString());
                        if (_toolStripLabel.Text != STATUS_READ) ConsumerStatusTextChanged?.Invoke(this, STATUS_READ);

                        try
                        {
                            AddRow(new GridRow(ID_COUNTER,
                                                consumeResult.Topic,
                                                consumeResult.Message.Key != null ? consumeResult.Message.Key.ToString() : string.Empty,
                                                consumeResult.Message.Value != null ? consumeResult.Message.Value.ToString() : string.Empty,
                                                consumeResult.Message.Timestamp.UtcDateTime));
                        }
                        catch
                        {
                            AddRow(new GridRow(ID_COUNTER,
                                                consumeResult.Topic,
                                                consumeResult.Message.Key != null ? consumeResult.Message.Key.ToString() : string.Empty,
                                                consumeResult.Message.Value != null ? consumeResult.Message.Value.ToString() : string.Empty,
                                                DateTime.UtcNow));
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

                if (setting.IsStatistic)
                {
                    _toolStatisticsLabel.Text = Statistics.GetCount().ToString();
                }
            });
        }

        private void AddRow(GridRow row)
        {
            _dataGridViewSubscriber.Invoke(new Action(() =>
            {
                var rw = RowFormatter.CreateRow(row, setting);

                if (rw != null)
                {
                    if (setting.IsStatistic && rw.Id > 0)
                    {
                        Statistics.TimeValues.Add(new DataTopic(rw.Topic, rw.Timestamp));
                        return;
                    }

                    if (_consumerDataSet.Messages.Rows.Count > setting.MaxRows)
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

            if (setting.IsStatistic)
            {
                Statistics.DrowChart(this._radChartView);
            }
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
            BindDeliveryResult(null);

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

            var message = _kafka.GetMessage(KAFKA_KEY_TYPE);            

            message.Topic = cmbProducerTopic.Text;
            message.KeyString = tbProducerKey.Text;
            message.Message = messageText;

            var cancelSource = new CancellationTokenSource();

            try
            {
                var deliveryResult = _kafka.GetDeliviryResult(KAFKA_KEY_TYPE);
                var producer = _kafka.GetProducer(KAFKA_SERVER, KAFKA_KEY_TYPE);

                using (producer)
                {
                    DeliviryStatus ds = new DeliviryStatus() { Start = DateTime.Now, Count = 0 };

                    for (int i = 1; i <= _cnt; i++)
                    {
                        deliveryResult = await _kafka.SendToKafka(message.Key, message.Topic, message.Message, producer, cancelSource.Token);

                        if (deliveryResult.Status != PersistenceStatus.Persisted) break;
                        ds.Count++;
                    }

                    if (deliveryResult.Status != PersistenceStatus.Persisted)
                    {
                        MessageBox.Show($"Send to kafka failed {deliveryResult.Status}");
                    }
                    else
                    {
                        ds.Finish = DateTime.Now;

                        BindDeliveryResult(ds);
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
            OpenFileDialog open = new OpenFileDialog
            {
                Filter = "Text File | *.txt"
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                textBoxFileToSend.Text = open.FileName;
            }
            else
            {
                textBoxFileToSend.Text = String.Empty;
            }
        }

        private void dataGridViewSubscriber_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
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

            uniq.AddRange(result.Select(x => x).Distinct().ToList());

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

        private void rbMessageType_CheckStateChanged(object sender, EventArgs e)
        {
            if (rbKeyString.IsChecked) KAFKA_KEY_TYPE = "string";
            else if (rbKeyLong.IsChecked) KAFKA_KEY_TYPE = "long";
            else if (rbKeyInt.IsChecked) KAFKA_KEY_TYPE = "int";
            else if (rbKeyIgnore.IsChecked) KAFKA_KEY_TYPE = "ignore";

            radMessageSetting.HeaderText = string.Format("Key type [{0}]", KAFKA_KEY_TYPE);
        }


        private void BindDeliveryResult(DeliviryStatus delivery)
        {            
            if (delivery== null)
            {
                tbResult.Text = string.Empty;
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Message sended: {0}", delivery.Count.ToString()));
            sb.AppendLine(string.Format("Message size: {0}", delivery.MessageSize));
            sb.AppendLine(string.Format("Total time: {0}ms", (delivery.Finish - delivery.Start).TotalMilliseconds));
            sb.AppendLine(string.Format("AVG time: {0}ms", ((delivery.Finish - delivery.Start).TotalMilliseconds) / delivery.Count).ToString());

            tbResult.Text = sb.ToString();
        }
    }
}