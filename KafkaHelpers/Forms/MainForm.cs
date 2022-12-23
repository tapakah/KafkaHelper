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
                private static readonly int DEFAULT_COUNTER = 100;
                private static readonly int DELAY_TIMER_STATUS = 1000;
                private string kafkaServer = string.Empty;
                private readonly string kafkaServerList = string.Empty;
                private readonly string kafkaTopics = string.Empty;
                private readonly string kafkaChkTopics = string.Empty;
                private readonly List<string> topics = new List<string>();
                private readonly Terms terms = new Terms();
                private readonly KafkaHelper _kafka;
                private static System.Threading.Timer tTimer;
                private static readonly string STATUS_READ = "READ";
                private static readonly string STATUS_WAIT = "WAIT";
                private static int ID_COUNTER;
                private static readonly int maxMessageLength = 30000;
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

                        _toolStripLabel.Text = "UNSUBSCRIBE";

                        _kafka = new KafkaHelper(CreateConsumingPolicy());

                        terms.Counter = DEFAULT_COUNTER;

                        tbCounter.Text = terms.Counter.ToString();

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

                        _consumerDataSet.Messages.Clear();
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
                                        var message = string.IsNullOrEmpty(rw.Value) ? string.Empty : rw.Value;

                                        if (message.Length > maxMessageLength)
                                        {
                                                message = message = message.Substring(0, message.Length > maxMessageLength ? maxMessageLength : message.Length) +  $"[Message was substringed ({message.Length} to {maxMessageLength} )]";
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
                                using (IProducer<long, string> producer = _kafka.CreateKafkaProducer(kafkaServer))
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

                private void dataGridViewSubscriber_CellContentClick(object sender, DataGridViewCellEventArgs e)
                {

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
        }
}