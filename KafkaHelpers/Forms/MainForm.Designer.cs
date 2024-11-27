namespace KafkaHelpers
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}


		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			Telerik.WinControls.UI.CartesianArea cartesianArea1 = new Telerik.WinControls.UI.CartesianArea();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageSetting = new System.Windows.Forms.TabPage();
			this.btn_Setting = new System.Windows.Forms.Button();
			this.radMessageSetting = new Telerik.WinControls.UI.RadCollapsiblePanel();
			this.rbKey = new Telerik.WinControls.UI.RadGroupBox();
			this.rbKeyIgnore = new Telerik.WinControls.UI.RadRadioButton();
			this.rbKeyInt = new Telerik.WinControls.UI.RadRadioButton();
			this.rbKeyString = new Telerik.WinControls.UI.RadRadioButton();
			this.rbKeyLong = new Telerik.WinControls.UI.RadRadioButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label10 = new System.Windows.Forms.Label();
			this.tbFilterTopics = new System.Windows.Forms.TextBox();
			this.ctbKafkaServer = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radModeSwitch = new Telerik.WinControls.UI.RadToggleSwitch();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.tbMaxRows = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tbCounter = new System.Windows.Forms.TextBox();
			this.tbValue = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tbKey = new System.Windows.Forms.TextBox();
			this.groupBoxTime = new System.Windows.Forms.GroupBox();
			this.btnMax = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.chkTimestamp = new System.Windows.Forms.CheckBox();
			this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
			this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
			this.label9 = new System.Windows.Forms.Label();
			this.btnReadTopics = new System.Windows.Forms.Button();
			this.btnUnSubscribe = new System.Windows.Forms.Button();
			this.btnUncheckAll = new System.Windows.Forms.Button();
			this.btnCheckAll = new System.Windows.Forms.Button();
			this.chklTopics = new System.Windows.Forms.CheckedListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnSubscribe = new System.Windows.Forms.Button();
			this.tabPageSubsriber = new System.Windows.Forms.TabPage();
			this._panel = new System.Windows.Forms.Panel();
			this.chbDefaultJsonParse = new System.Windows.Forms.CheckBox();
			this.btnView = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnSubscribe2 = new System.Windows.Forms.Button();
			this.btnUnSubscribe2 = new System.Windows.Forms.Button();
			this._statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripKafkaServer = new System.Windows.Forms.ToolStripStatusLabel();
			this._toolStripKafkaServerValue = new System.Windows.Forms.ToolStripStatusLabel();
			this._toolStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this._toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this._toolStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this._tsStatusConsumer = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this._toolStatisticsLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabPageProducer = new System.Windows.Forms.TabPage();
			this.tbResult = new System.Windows.Forms.TextBox();
			this.textBoxFileToSend = new System.Windows.Forms.TextBox();
			this.cntToSend = new System.Windows.Forms.NumericUpDown();
			this.tbProducerValue = new System.Windows.Forms.TextBox();
			this.tbProducerKey = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.cmbProducerTopic = new System.Windows.Forms.ComboBox();
			this.buttonFile = new System.Windows.Forms.Button();
			this.btnSendMessage = new System.Windows.Forms.Button();
			this.tabPageStatistics = new System.Windows.Forms.TabPage();
			this._radChartView = new Telerik.WinControls.UI.RadChartView();
			this.tabPageLog = new System.Windows.Forms.TabPage();
			this.tbConsoleLog = new System.Windows.Forms.TextBox();
			this.filetoSendDialog = new System.Windows.Forms.OpenFileDialog();
			this._telerikMetroTheme = new Telerik.WinControls.Themes.TelerikMetroTheme();
			this.object_e312bac3_151b_4c5a_91ba_fa2dfce57017 = new Telerik.WinControls.RootRadElement();
			this._dataGridViewSubscriber = new System.Windows.Forms.DataGridView();
			this.TimeStamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Topic = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._consumerDataSet = new KafkaHelpers.Model.ConsumerDataSet();
			this.messagesBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.tbTopicInfo = new System.Windows.Forms.TextBox();
			this.tabControl.SuspendLayout();
			this.tabPageSetting.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.radMessageSetting)).BeginInit();
			this.radMessageSetting.PanelContainer.SuspendLayout();
			this.radMessageSetting.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rbKey)).BeginInit();
			this.rbKey.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rbKeyIgnore)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rbKeyInt)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rbKeyString)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rbKeyLong)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.radModeSwitch)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBoxTime.SuspendLayout();
			this.tabPageSubsriber.SuspendLayout();
			this._panel.SuspendLayout();
			this._statusStrip.SuspendLayout();
			this.tabPageProducer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cntToSend)).BeginInit();
			this.tabPageStatistics.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._radChartView)).BeginInit();
			this.tabPageLog.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._dataGridViewSubscriber)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._consumerDataSet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.messagesBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabPageSetting);
			this.tabControl.Controls.Add(this.tabPageSubsriber);
			this.tabControl.Controls.Add(this.tabPageProducer);
			this.tabControl.Controls.Add(this.tabPageStatistics);
			this.tabControl.Controls.Add(this.tabPageLog);
			this.tabControl.Location = new System.Drawing.Point(0, 2);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(989, 643);
			this.tabControl.TabIndex = 0;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// tabPageSetting
			// 
			this.tabPageSetting.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageSetting.Controls.Add(this.tbTopicInfo);
			this.tabPageSetting.Controls.Add(this.btn_Setting);
			this.tabPageSetting.Controls.Add(this.radMessageSetting);
			this.tabPageSetting.Controls.Add(this.panel1);
			this.tabPageSetting.Controls.Add(this.label10);
			this.tabPageSetting.Controls.Add(this.tbFilterTopics);
			this.tabPageSetting.Controls.Add(this.ctbKafkaServer);
			this.tabPageSetting.Controls.Add(this.groupBox1);
			this.tabPageSetting.Controls.Add(this.btnReadTopics);
			this.tabPageSetting.Controls.Add(this.btnUnSubscribe);
			this.tabPageSetting.Controls.Add(this.btnUncheckAll);
			this.tabPageSetting.Controls.Add(this.btnCheckAll);
			this.tabPageSetting.Controls.Add(this.chklTopics);
			this.tabPageSetting.Controls.Add(this.label2);
			this.tabPageSetting.Controls.Add(this.label1);
			this.tabPageSetting.Controls.Add(this.btnSubscribe);
			this.tabPageSetting.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tabPageSetting.Location = new System.Drawing.Point(4, 22);
			this.tabPageSetting.Name = "tabPageSetting";
			this.tabPageSetting.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSetting.Size = new System.Drawing.Size(981, 617);
			this.tabPageSetting.TabIndex = 0;
			this.tabPageSetting.Text = "Setting";
			// 
			// btn_Setting
			// 
			this.btn_Setting.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btn_Setting.Image = global::KafkaHelpers.Properties.Resources.gear_16;
			this.btn_Setting.Location = new System.Drawing.Point(279, 6);
			this.btn_Setting.Name = "btn_Setting";
			this.btn_Setting.Size = new System.Drawing.Size(28, 26);
			this.btn_Setting.TabIndex = 2;
			this.btn_Setting.UseVisualStyleBackColor = true;
			this.btn_Setting.Click += new System.EventHandler(this.btn_Setting_Click);
			// 
			// radMessageSetting
			// 
			this.radMessageSetting.EnableAnimation = false;
			this.radMessageSetting.EnableTheming = false;
			this.radMessageSetting.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
			this.radMessageSetting.HeaderText = "Key type ";
			this.radMessageSetting.IsExpanded = false;
			this.radMessageSetting.Location = new System.Drawing.Point(443, 161);
			this.radMessageSetting.Name = "radMessageSetting";
			this.radMessageSetting.OwnerBoundsCache = new System.Drawing.Rectangle(443, 161, 149, 153);
			// 
			// radMessageSetting.PanelContainer
			// 
			this.radMessageSetting.PanelContainer.Controls.Add(this.rbKey);
			this.radMessageSetting.PanelContainer.Size = new System.Drawing.Size(0, 0);
			this.radMessageSetting.Size = new System.Drawing.Size(149, 21);
			this.radMessageSetting.TabIndex = 3;
			// 
			// rbKey
			// 
			this.rbKey.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this.rbKey.Controls.Add(this.rbKeyIgnore);
			this.rbKey.Controls.Add(this.rbKeyInt);
			this.rbKey.Controls.Add(this.rbKeyString);
			this.rbKey.Controls.Add(this.rbKeyLong);
			this.rbKey.HeaderText = "Key type";
			this.rbKey.Location = new System.Drawing.Point(3, 3);
			this.rbKey.Name = "rbKey";
			this.rbKey.Size = new System.Drawing.Size(139, 117);
			this.rbKey.TabIndex = 0;
			this.rbKey.Text = "Key type";
			// 
			// rbKeyIgnore
			// 
			this.rbKeyIgnore.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
			this.rbKeyIgnore.Location = new System.Drawing.Point(13, 93);
			this.rbKeyIgnore.Name = "rbKeyIgnore";
			this.rbKeyIgnore.Size = new System.Drawing.Size(55, 18);
			this.rbKeyIgnore.TabIndex = 3;
			this.rbKeyIgnore.TabStop = false;
			this.rbKeyIgnore.Text = "ignore";
			// 
			// rbKeyInt
			// 
			this.rbKeyInt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
			this.rbKeyInt.Location = new System.Drawing.Point(13, 69);
			this.rbKeyInt.Name = "rbKeyInt";
			this.rbKeyInt.Size = new System.Drawing.Size(35, 18);
			this.rbKeyInt.TabIndex = 2;
			this.rbKeyInt.TabStop = false;
			this.rbKeyInt.Text = "int";
			this.rbKeyInt.CheckStateChanged += new System.EventHandler(this.rbMessageType_CheckStateChanged);
			// 
			// rbKeyString
			// 
			this.rbKeyString.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
			this.rbKeyString.Location = new System.Drawing.Point(13, 21);
			this.rbKeyString.Name = "rbKeyString";
			this.rbKeyString.Size = new System.Drawing.Size(51, 18);
			this.rbKeyString.TabIndex = 0;
			this.rbKeyString.TabStop = false;
			this.rbKeyString.Text = "string";
			this.rbKeyString.CheckStateChanged += new System.EventHandler(this.rbMessageType_CheckStateChanged);
			// 
			// rbKeyLong
			// 
			this.rbKeyLong.CheckState = System.Windows.Forms.CheckState.Checked;
			this.rbKeyLong.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
			this.rbKeyLong.Location = new System.Drawing.Point(13, 45);
			this.rbKeyLong.Name = "rbKeyLong";
			this.rbKeyLong.Size = new System.Drawing.Size(44, 18);
			this.rbKeyLong.TabIndex = 1;
			this.rbKeyLong.Text = "long";
			this.rbKeyLong.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
			this.rbKeyLong.CheckStateChanged += new System.EventHandler(this.rbMessageType_CheckStateChanged);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Location = new System.Drawing.Point(607, -1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(378, 618);
			this.panel1.TabIndex = 12;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pictureBox1.Image = global::KafkaHelpers.Properties.Resources.kafka_hehper_1;
			this.pictureBox1.Location = new System.Drawing.Point(3, 186);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(373, 259);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label10.Image = global::KafkaHelpers.Properties.Resources.view_16;
			this.label10.Location = new System.Drawing.Point(65, 164);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(21, 17);
			this.label10.TabIndex = 11;
			// 
			// tbFilterTopics
			// 
			this.tbFilterTopics.Location = new System.Drawing.Point(90, 161);
			this.tbFilterTopics.Name = "tbFilterTopics";
			this.tbFilterTopics.Size = new System.Drawing.Size(347, 22);
			this.tbFilterTopics.TabIndex = 10;
			this.tbFilterTopics.TextChanged += new System.EventHandler(this.tbFilterTopics_TextChanged);
			// 
			// ctbKafkaServer
			// 
			this.ctbKafkaServer.FormattingEnabled = true;
			this.ctbKafkaServer.Location = new System.Drawing.Point(62, 9);
			this.ctbKafkaServer.Name = "ctbKafkaServer";
			this.ctbKafkaServer.Size = new System.Drawing.Size(215, 21);
			this.ctbKafkaServer.TabIndex = 0;
			this.ctbKafkaServer.SelectedValueChanged += new System.EventHandler(this.ctbKafkaServer_SelectedValueChanged);
			this.ctbKafkaServer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ctbKafkaServer_KeyPress);
			this.ctbKafkaServer.Validating += new System.ComponentModel.CancelEventHandler(this.ctbKafkaServer_Validating);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radModeSwitch);
			this.groupBox1.Controls.Add(this.groupBox3);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.tbValue);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.tbKey);
			this.groupBox1.Controls.Add(this.groupBoxTime);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.Location = new System.Drawing.Point(62, 34);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(538, 125);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Terms";
			// 
			// radModeSwitch
			// 
			this.radModeSwitch.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.radModeSwitch.Location = new System.Drawing.Point(408, 27);
			this.radModeSwitch.Name = "radModeSwitch";
			this.radModeSwitch.OffText = "Statistics";
			this.radModeSwitch.OnText = "Grid";
			this.radModeSwitch.Size = new System.Drawing.Size(122, 29);
			this.radModeSwitch.TabIndex = 12;
			this.radModeSwitch.Text = "radToggleSwitch1";
			this.radModeSwitch.ThemeName = "ControlDefault";
			this.radModeSwitch.ThumbTickness = 40;
			((Telerik.WinControls.UI.RadToggleSwitchElement)(this.radModeSwitch.GetChildAt(0))).ThumbTickness = 40;
			((Telerik.WinControls.UI.RadToggleSwitchElement)(this.radModeSwitch.GetChildAt(0))).ThumbOffset = 82;
			((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.radModeSwitch.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
			((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.radModeSwitch.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
			((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.radModeSwitch.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
			((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.radModeSwitch.GetChildAt(0).GetChildAt(0))).Text = "Grid";
			((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.radModeSwitch.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.tbMaxRows);
			this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox3.Location = new System.Drawing.Point(465, 70);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(65, 44);
			this.groupBox3.TabIndex = 22;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Rows";
			// 
			// tbMaxRows
			// 
			this.tbMaxRows.Location = new System.Drawing.Point(6, 15);
			this.tbMaxRows.Name = "tbMaxRows";
			this.tbMaxRows.Size = new System.Drawing.Size(53, 22);
			this.tbMaxRows.TabIndex = 11;
			this.tbMaxRows.TextChanged += new System.EventHandler(this.tbTop_TextChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.tbCounter);
			this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox2.Location = new System.Drawing.Point(399, 70);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(65, 44);
			this.groupBox2.TabIndex = 18;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Counter";
			// 
			// tbCounter
			// 
			this.tbCounter.Location = new System.Drawing.Point(6, 15);
			this.tbCounter.Name = "tbCounter";
			this.tbCounter.Size = new System.Drawing.Size(53, 22);
			this.tbCounter.TabIndex = 11;
			this.tbCounter.TextChanged += new System.EventHandler(this.tbCounter_TextChanged);
			// 
			// tbValue
			// 
			this.tbValue.Location = new System.Drawing.Point(60, 94);
			this.tbValue.Name = "tbValue";
			this.tbValue.Size = new System.Drawing.Size(333, 22);
			this.tbValue.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(16, 97);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 13);
			this.label3.TabIndex = 21;
			this.label3.Text = "Value:";
			// 
			// tbKey
			// 
			this.tbKey.Location = new System.Drawing.Point(60, 70);
			this.tbKey.Name = "tbKey";
			this.tbKey.Size = new System.Drawing.Size(333, 22);
			this.tbKey.TabIndex = 0;
			// 
			// groupBoxTime
			// 
			this.groupBoxTime.Controls.Add(this.btnMax);
			this.groupBoxTime.Controls.Add(this.label8);
			this.groupBoxTime.Controls.Add(this.label7);
			this.groupBoxTime.Controls.Add(this.chkTimestamp);
			this.groupBoxTime.Controls.Add(this.dateTimeEnd);
			this.groupBoxTime.Controls.Add(this.dateTimeStart);
			this.groupBoxTime.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBoxTime.Location = new System.Drawing.Point(6, 19);
			this.groupBoxTime.Name = "groupBoxTime";
			this.groupBoxTime.Size = new System.Drawing.Size(396, 44);
			this.groupBoxTime.TabIndex = 17;
			this.groupBoxTime.TabStop = false;
			this.groupBoxTime.Text = "TimeStamp";
			// 
			// btnMax
			// 
			this.btnMax.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnMax.Location = new System.Drawing.Point(354, 16);
			this.btnMax.Name = "btnMax";
			this.btnMax.Size = new System.Drawing.Size(36, 20);
			this.btnMax.TabIndex = 19;
			this.btnMax.Text = "max";
			this.btnMax.UseVisualStyleBackColor = true;
			this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label8.Location = new System.Drawing.Point(186, 20);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(30, 13);
			this.label8.TabIndex = 18;
			this.label8.Text = "End:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label7.Location = new System.Drawing.Point(15, 20);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(34, 13);
			this.label7.TabIndex = 17;
			this.label7.Text = "Start:";
			// 
			// chkTimestamp
			// 
			this.chkTimestamp.AutoSize = true;
			this.chkTimestamp.Location = new System.Drawing.Point(79, 0);
			this.chkTimestamp.Name = "chkTimestamp";
			this.chkTimestamp.Size = new System.Drawing.Size(15, 14);
			this.chkTimestamp.TabIndex = 0;
			this.chkTimestamp.UseVisualStyleBackColor = true;
			this.chkTimestamp.CheckedChanged += new System.EventHandler(this.chkTimestamp_CheckedChanged);
			// 
			// dateTimeEnd
			// 
			this.dateTimeEnd.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dateTimeEnd.CustomFormat = "dd-MM-yyyy HH:mm";
			this.dateTimeEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimeEnd.Location = new System.Drawing.Point(222, 16);
			this.dateTimeEnd.Name = "dateTimeEnd";
			this.dateTimeEnd.Size = new System.Drawing.Size(126, 20);
			this.dateTimeEnd.TabIndex = 2;
			// 
			// dateTimeStart
			// 
			this.dateTimeStart.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dateTimeStart.CustomFormat = "dd-MM-yyyy HH:mm";
			this.dateTimeStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimeStart.Location = new System.Drawing.Point(54, 16);
			this.dateTimeStart.Name = "dateTimeStart";
			this.dateTimeStart.Size = new System.Drawing.Size(126, 20);
			this.dateTimeStart.TabIndex = 1;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label9.Location = new System.Drawing.Point(27, 73);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(29, 13);
			this.label9.TabIndex = 19;
			this.label9.Text = "Key:";
			// 
			// btnReadTopics
			// 
			this.btnReadTopics.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnReadTopics.Image = global::KafkaHelpers.Properties.Resources.view_16;
			this.btnReadTopics.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnReadTopics.Location = new System.Drawing.Point(308, 6);
			this.btnReadTopics.Name = "btnReadTopics";
			this.btnReadTopics.Size = new System.Drawing.Size(103, 26);
			this.btnReadTopics.TabIndex = 1;
			this.btnReadTopics.Text = "Read topics";
			this.btnReadTopics.UseVisualStyleBackColor = true;
			this.btnReadTopics.Click += new System.EventHandler(this.btnReadTopics_Click);
			// 
			// btnUnSubscribe
			// 
			this.btnUnSubscribe.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnUnSubscribe.Image = global::KafkaHelpers.Properties.Resources.stop;
			this.btnUnSubscribe.Location = new System.Drawing.Point(502, 6);
			this.btnUnSubscribe.Name = "btnUnSubscribe";
			this.btnUnSubscribe.Size = new System.Drawing.Size(98, 26);
			this.btnUnSubscribe.TabIndex = 3;
			this.btnUnSubscribe.Text = "UnSubscribe";
			this.btnUnSubscribe.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnUnSubscribe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnUnSubscribe.UseVisualStyleBackColor = true;
			this.btnUnSubscribe.Click += new System.EventHandler(this.btnUnSubscribe_Click);
			// 
			// btnUncheckAll
			// 
			this.btnUncheckAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnUncheckAll.Location = new System.Drawing.Point(22, 221);
			this.btnUncheckAll.Name = "btnUncheckAll";
			this.btnUncheckAll.Size = new System.Drawing.Size(35, 23);
			this.btnUncheckAll.TabIndex = 9;
			this.btnUncheckAll.Text = "-";
			this.btnUncheckAll.UseVisualStyleBackColor = true;
			this.btnUncheckAll.Click += new System.EventHandler(this.btnUncheckAll_Click);
			// 
			// btnCheckAll
			// 
			this.btnCheckAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnCheckAll.Location = new System.Drawing.Point(22, 193);
			this.btnCheckAll.Name = "btnCheckAll";
			this.btnCheckAll.Size = new System.Drawing.Size(35, 23);
			this.btnCheckAll.TabIndex = 8;
			this.btnCheckAll.Text = "+";
			this.btnCheckAll.UseVisualStyleBackColor = true;
			this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
			// 
			// chklTopics
			// 
			this.chklTopics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.chklTopics.FormattingEnabled = true;
			this.chklTopics.HorizontalScrollbar = true;
			this.chklTopics.Location = new System.Drawing.Point(62, 185);
			this.chklTopics.Name = "chklTopics";
			this.chklTopics.ScrollAlwaysVisible = true;
			this.chklTopics.Size = new System.Drawing.Size(539, 293);
			this.chklTopics.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(15, 166);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Topics:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(15, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Server:";
			// 
			// btnSubscribe
			// 
			this.btnSubscribe.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnSubscribe.Image = global::KafkaHelpers.Properties.Resources.play4;
			this.btnSubscribe.Location = new System.Drawing.Point(412, 6);
			this.btnSubscribe.Name = "btnSubscribe";
			this.btnSubscribe.Size = new System.Drawing.Size(89, 26);
			this.btnSubscribe.TabIndex = 2;
			this.btnSubscribe.Text = "Subscribe";
			this.btnSubscribe.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSubscribe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSubscribe.UseVisualStyleBackColor = true;
			this.btnSubscribe.Click += new System.EventHandler(this.btnSubscribe_Click);
			// 
			// tabPageSubsriber
			// 
			this.tabPageSubsriber.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageSubsriber.Controls.Add(this._dataGridViewSubscriber);
			this.tabPageSubsriber.Controls.Add(this._panel);
			this.tabPageSubsriber.Controls.Add(this._statusStrip);
			this.tabPageSubsriber.Location = new System.Drawing.Point(4, 22);
			this.tabPageSubsriber.Name = "tabPageSubsriber";
			this.tabPageSubsriber.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSubsriber.Size = new System.Drawing.Size(981, 617);
			this.tabPageSubsriber.TabIndex = 1;
			this.tabPageSubsriber.Text = "Subscriber";
			// 
			// _panel
			// 
			this._panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._panel.Controls.Add(this.chbDefaultJsonParse);
			this._panel.Controls.Add(this.btnView);
			this._panel.Controls.Add(this.btnClear);
			this._panel.Controls.Add(this.btnSubscribe2);
			this._panel.Controls.Add(this.btnUnSubscribe2);
			this._panel.Location = new System.Drawing.Point(3, 6);
			this._panel.Name = "_panel";
			this._panel.Size = new System.Drawing.Size(979, 33);
			this._panel.TabIndex = 21;
			// 
			// chbDefaultJsonParse
			// 
			this.chbDefaultJsonParse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.chbDefaultJsonParse.AutoSize = true;
			this.chbDefaultJsonParse.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chbDefaultJsonParse.Checked = true;
			this.chbDefaultJsonParse.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbDefaultJsonParse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.chbDefaultJsonParse.Location = new System.Drawing.Point(779, 9);
			this.chbDefaultJsonParse.Name = "chbDefaultJsonParse";
			this.chbDefaultJsonParse.Size = new System.Drawing.Size(196, 17);
			this.chbDefaultJsonParse.TabIndex = 4;
			this.chbDefaultJsonParse.Text = "View with default JSON parse ON";
			this.chbDefaultJsonParse.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.chbDefaultJsonParse.UseVisualStyleBackColor = true;
			// 
			// btnView
			// 
			this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnView.Image = global::KafkaHelpers.Properties.Resources.view_16;
			this.btnView.Location = new System.Drawing.Point(98, 4);
			this.btnView.Name = "btnView";
			this.btnView.Size = new System.Drawing.Size(27, 25);
			this.btnView.TabIndex = 3;
			this.btnView.UseVisualStyleBackColor = true;
			this.btnView.Click += new System.EventHandler(this.btnView_Click);
			// 
			// btnClear
			// 
			this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnClear.Image = global::KafkaHelpers.Properties.Resources.garbage_empty;
			this.btnClear.Location = new System.Drawing.Point(67, 4);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(27, 25);
			this.btnClear.TabIndex = 2;
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnSubscribe2
			// 
			this.btnSubscribe2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnSubscribe2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnSubscribe2.Image = global::KafkaHelpers.Properties.Resources.play4;
			this.btnSubscribe2.Location = new System.Drawing.Point(5, 4);
			this.btnSubscribe2.Name = "btnSubscribe2";
			this.btnSubscribe2.Size = new System.Drawing.Size(27, 25);
			this.btnSubscribe2.TabIndex = 0;
			this.btnSubscribe2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSubscribe2.UseVisualStyleBackColor = true;
			this.btnSubscribe2.Click += new System.EventHandler(this.btnSubscribe_Click);
			// 
			// btnUnSubscribe2
			// 
			this.btnUnSubscribe2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnUnSubscribe2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnUnSubscribe2.Image = global::KafkaHelpers.Properties.Resources.stop;
			this.btnUnSubscribe2.Location = new System.Drawing.Point(36, 4);
			this.btnUnSubscribe2.Name = "btnUnSubscribe2";
			this.btnUnSubscribe2.Size = new System.Drawing.Size(27, 25);
			this.btnUnSubscribe2.TabIndex = 1;
			this.btnUnSubscribe2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnUnSubscribe2.UseVisualStyleBackColor = true;
			this.btnUnSubscribe2.Click += new System.EventHandler(this.btnUnSubscribe_Click);
			// 
			// _statusStrip
			// 
			this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripKafkaServer,
            this._toolStripKafkaServerValue,
            this._toolStripLabel,
            this._toolStripProgressBar,
            this._toolStripStatus,
            this._tsStatusConsumer,
            this.toolStripStatusLabel1,
            this._toolStatisticsLabel});
			this._statusStrip.Location = new System.Drawing.Point(3, 590);
			this._statusStrip.Name = "_statusStrip";
			this._statusStrip.Size = new System.Drawing.Size(975, 24);
			this._statusStrip.TabIndex = 20;
			this._statusStrip.Text = "statusStrip1";
			// 
			// toolStripKafkaServer
			// 
			this.toolStripKafkaServer.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this.toolStripKafkaServer.Name = "toolStripKafkaServer";
			this.toolStripKafkaServer.Size = new System.Drawing.Size(43, 19);
			this.toolStripKafkaServer.Text = "Server:";
			// 
			// _toolStripKafkaServerValue
			// 
			this._toolStripKafkaServerValue.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this._toolStripKafkaServerValue.Name = "_toolStripKafkaServerValue";
			this._toolStripKafkaServerValue.Size = new System.Drawing.Size(0, 19);
			// 
			// _toolStripLabel
			// 
			this._toolStripLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this._toolStripLabel.ForeColor = System.Drawing.Color.Navy;
			this._toolStripLabel.Name = "_toolStripLabel";
			this._toolStripLabel.Size = new System.Drawing.Size(56, 19);
			this._toolStripLabel.Text = "Message:";
			// 
			// _toolStripProgressBar
			// 
			this._toolStripProgressBar.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this._toolStripProgressBar.Name = "_toolStripProgressBar";
			this._toolStripProgressBar.Size = new System.Drawing.Size(100, 18);
			this._toolStripProgressBar.Step = 1;
			// 
			// _toolStripStatus
			// 
			this._toolStripStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this._toolStripStatus.Name = "_toolStripStatus";
			this._toolStripStatus.Size = new System.Drawing.Size(0, 19);
			// 
			// _tsStatusConsumer
			// 
			this._tsStatusConsumer.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this._tsStatusConsumer.ForeColor = System.Drawing.Color.Navy;
			this._tsStatusConsumer.Name = "_tsStatusConsumer";
			this._tsStatusConsumer.Size = new System.Drawing.Size(0, 19);
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Navy;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(51, 19);
			this.toolStripStatusLabel1.Text = "Statistis:";
			// 
			// _toolStatisticsLabel
			// 
			this._toolStatisticsLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this._toolStatisticsLabel.Name = "_toolStatisticsLabel";
			this._toolStatisticsLabel.Size = new System.Drawing.Size(14, 19);
			this._toolStatisticsLabel.Text = "0";
			// 
			// tabPageProducer
			// 
			this.tabPageProducer.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageProducer.Controls.Add(this.tbResult);
			this.tabPageProducer.Controls.Add(this.textBoxFileToSend);
			this.tabPageProducer.Controls.Add(this.cntToSend);
			this.tabPageProducer.Controls.Add(this.tbProducerValue);
			this.tabPageProducer.Controls.Add(this.tbProducerKey);
			this.tabPageProducer.Controls.Add(this.label6);
			this.tabPageProducer.Controls.Add(this.label5);
			this.tabPageProducer.Controls.Add(this.label4);
			this.tabPageProducer.Controls.Add(this.cmbProducerTopic);
			this.tabPageProducer.Controls.Add(this.buttonFile);
			this.tabPageProducer.Controls.Add(this.btnSendMessage);
			this.tabPageProducer.Location = new System.Drawing.Point(4, 22);
			this.tabPageProducer.Name = "tabPageProducer";
			this.tabPageProducer.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageProducer.Size = new System.Drawing.Size(981, 617);
			this.tabPageProducer.TabIndex = 2;
			this.tabPageProducer.Text = "Producer";
			// 
			// tbResult
			// 
			this.tbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbResult.BackColor = System.Drawing.SystemColors.Control;
			this.tbResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbResult.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
			this.tbResult.Location = new System.Drawing.Point(107, 530);
			this.tbResult.Multiline = true;
			this.tbResult.Name = "tbResult";
			this.tbResult.Size = new System.Drawing.Size(778, 78);
			this.tbResult.TabIndex = 9;
			// 
			// textBoxFileToSend
			// 
			this.textBoxFileToSend.Location = new System.Drawing.Point(504, 9);
			this.textBoxFileToSend.Name = "textBoxFileToSend";
			this.textBoxFileToSend.ReadOnly = true;
			this.textBoxFileToSend.Size = new System.Drawing.Size(264, 22);
			this.textBoxFileToSend.TabIndex = 7;
			// 
			// cntToSend
			// 
			this.cntToSend.Location = new System.Drawing.Point(648, 38);
			this.cntToSend.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.cntToSend.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.cntToSend.Name = "cntToSend";
			this.cntToSend.Size = new System.Drawing.Size(120, 22);
			this.cntToSend.TabIndex = 6;
			this.cntToSend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cntToSend.ThousandsSeparator = true;
			this.cntToSend.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// tbProducerValue
			// 
			this.tbProducerValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tbProducerValue.Location = new System.Drawing.Point(107, 73);
			this.tbProducerValue.MaxLength = 10000000;
			this.tbProducerValue.Multiline = true;
			this.tbProducerValue.Name = "tbProducerValue";
			this.tbProducerValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbProducerValue.Size = new System.Drawing.Size(778, 451);
			this.tbProducerValue.TabIndex = 2;
			// 
			// tbProducerKey
			// 
			this.tbProducerKey.Location = new System.Drawing.Point(107, 38);
			this.tbProducerKey.Name = "tbProducerKey";
			this.tbProducerKey.Size = new System.Drawing.Size(391, 22);
			this.tbProducerKey.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label6.Location = new System.Drawing.Point(5, 73);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(87, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "Message value:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(16, 42);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(77, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Message key:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(8, 14);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(85, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Message topic:";
			// 
			// cmbProducerTopic
			// 
			this.cmbProducerTopic.FormattingEnabled = true;
			this.cmbProducerTopic.Location = new System.Drawing.Point(107, 10);
			this.cmbProducerTopic.Name = "cmbProducerTopic";
			this.cmbProducerTopic.Size = new System.Drawing.Size(391, 21);
			this.cmbProducerTopic.TabIndex = 0;
			// 
			// buttonFile
			// 
			this.buttonFile.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonFile.Image = global::KafkaHelpers.Properties.Resources.paperclip;
			this.buttonFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFile.Location = new System.Drawing.Point(774, 7);
			this.buttonFile.Name = "buttonFile";
			this.buttonFile.Size = new System.Drawing.Size(111, 26);
			this.buttonFile.TabIndex = 8;
			this.buttonFile.Text = "File to send";
			this.buttonFile.UseVisualStyleBackColor = true;
			this.buttonFile.Click += new System.EventHandler(this.buttonFile_Click);
			// 
			// btnSendMessage
			// 
			this.btnSendMessage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnSendMessage.Image = global::KafkaHelpers.Properties.Resources.mail_16;
			this.btnSendMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSendMessage.Location = new System.Drawing.Point(774, 36);
			this.btnSendMessage.Name = "btnSendMessage";
			this.btnSendMessage.Size = new System.Drawing.Size(111, 26);
			this.btnSendMessage.TabIndex = 3;
			this.btnSendMessage.Text = "Send";
			this.btnSendMessage.UseVisualStyleBackColor = true;
			this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
			// 
			// tabPageStatistics
			// 
			this.tabPageStatistics.Controls.Add(this._radChartView);
			this.tabPageStatistics.Location = new System.Drawing.Point(4, 22);
			this.tabPageStatistics.Name = "tabPageStatistics";
			this.tabPageStatistics.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageStatistics.Size = new System.Drawing.Size(981, 617);
			this.tabPageStatistics.TabIndex = 3;
			this.tabPageStatistics.Text = "Statistics";
			this.tabPageStatistics.UseVisualStyleBackColor = true;
			// 
			// _radChartView
			// 
			cartesianArea1.ShowGrid = true;
			this._radChartView.AreaDesign = cartesianArea1;
			this._radChartView.AutoScroll = true;
			this._radChartView.BackColor = System.Drawing.Color.Cornsilk;
			this._radChartView.Dock = System.Windows.Forms.DockStyle.Fill;
			this._radChartView.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this._radChartView.Location = new System.Drawing.Point(3, 3);
			this._radChartView.Name = "_radChartView";
			this._radChartView.SelectionMode = Telerik.WinControls.UI.ChartSelectionMode.MultipleDataPoints;
			this._radChartView.ShowPanZoom = true;
			this._radChartView.ShowToolTip = true;
			this._radChartView.ShowTrackBall = true;
			this._radChartView.Size = new System.Drawing.Size(975, 611);
			this._radChartView.TabIndex = 0;
			this._radChartView.ThemeName = "TelerikMetro";
			// 
			// tabPageLog
			// 
			this.tabPageLog.Controls.Add(this.tbConsoleLog);
			this.tabPageLog.Location = new System.Drawing.Point(4, 22);
			this.tabPageLog.Name = "tabPageLog";
			this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageLog.Size = new System.Drawing.Size(981, 617);
			this.tabPageLog.TabIndex = 4;
			this.tabPageLog.Text = "Logs";
			this.tabPageLog.UseVisualStyleBackColor = true;
			// 
			// tbConsoleLog
			// 
			this.tbConsoleLog.BackColor = System.Drawing.SystemColors.Control;
			this.tbConsoleLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbConsoleLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbConsoleLog.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
			this.tbConsoleLog.Location = new System.Drawing.Point(3, 3);
			this.tbConsoleLog.Multiline = true;
			this.tbConsoleLog.Name = "tbConsoleLog";
			this.tbConsoleLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbConsoleLog.Size = new System.Drawing.Size(975, 611);
			this.tbConsoleLog.TabIndex = 10;
			// 
			// object_e312bac3_151b_4c5a_91ba_fa2dfce57017
			// 
			this.object_e312bac3_151b_4c5a_91ba_fa2dfce57017.Name = "object_e312bac3_151b_4c5a_91ba_fa2dfce57017";
			this.object_e312bac3_151b_4c5a_91ba_fa2dfce57017.StretchHorizontally = true;
			this.object_e312bac3_151b_4c5a_91ba_fa2dfce57017.StretchVertically = true;
			// 
			// _dataGridViewSubscriber
			// 
			this._dataGridViewSubscriber.AllowUserToAddRows = false;
			this._dataGridViewSubscriber.AllowUserToDeleteRows = false;
			this._dataGridViewSubscriber.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.OldLace;
			this._dataGridViewSubscriber.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this._dataGridViewSubscriber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._dataGridViewSubscriber.AutoGenerateColumns = false;
			this._dataGridViewSubscriber.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this._dataGridViewSubscriber.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this._dataGridViewSubscriber.ColumnHeadersHeight = 25;
			this._dataGridViewSubscriber.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this._dataGridViewSubscriber.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TimeStamp,
            this.Topic,
            this.Key,
            this.Size,
            this.Value});
			this._dataGridViewSubscriber.DataMember = "Messages";
			this._dataGridViewSubscriber.DataSource = this._consumerDataSet;
			this._dataGridViewSubscriber.Location = new System.Drawing.Point(5, 42);
			this._dataGridViewSubscriber.Name = "_dataGridViewSubscriber";
			this._dataGridViewSubscriber.ReadOnly = true;
			this._dataGridViewSubscriber.RowHeadersVisible = false;
			this._dataGridViewSubscriber.RowHeadersWidth = 30;
			this._dataGridViewSubscriber.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._dataGridViewSubscriber.ShowCellToolTips = false;
			this._dataGridViewSubscriber.Size = new System.Drawing.Size(980, 543);
			this._dataGridViewSubscriber.TabIndex = 22;
			this._dataGridViewSubscriber.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSubscriber_CellDoubleClick);
			this._dataGridViewSubscriber.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.dataGridViewSubscriber_CellToolTipTextNeeded);
			// 
			// TimeStamp
			// 
			this.TimeStamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.TimeStamp.DataPropertyName = "Recived";
			dataGridViewCellStyle2.Format = "dd.MM.yyyy HH:mm:ss.FFFF";
			dataGridViewCellStyle2.NullValue = null;
			this.TimeStamp.DefaultCellStyle = dataGridViewCellStyle2;
			this.TimeStamp.HeaderText = "TimeStamp";
			this.TimeStamp.Name = "TimeStamp";
			this.TimeStamp.ReadOnly = true;
			this.TimeStamp.Width = 88;
			// 
			// Topic
			// 
			this.Topic.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Topic.DataPropertyName = "Topic";
			this.Topic.HeaderText = "Topic";
			this.Topic.Name = "Topic";
			this.Topic.ReadOnly = true;
			this.Topic.Width = 59;
			// 
			// Key
			// 
			this.Key.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Key.DataPropertyName = "Key";
			this.Key.HeaderText = "Key";
			this.Key.Name = "Key";
			this.Key.ReadOnly = true;
			this.Key.Width = 49;
			// 
			// Size
			// 
			this.Size.DataPropertyName = "Size";
			this.Size.HeaderText = "Size";
			this.Size.Name = "Size";
			this.Size.ReadOnly = true;
			this.Size.Width = 52;
			// 
			// Value
			// 
			this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Value.DataPropertyName = "Value";
			this.Value.HeaderText = "Value";
			this.Value.Name = "Value";
			this.Value.ReadOnly = true;
			// 
			// _consumerDataSet
			// 
			this._consumerDataSet.DataSetName = "ConsumerDataSet";
			this._consumerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// messagesBindingSource
			// 
			this.messagesBindingSource.DataMember = "Messages";
			this.messagesBindingSource.DataSource = this._consumerDataSet;
			// 
			// tbTopicInfo
			// 
			this.tbTopicInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbTopicInfo.BackColor = System.Drawing.SystemColors.Control;
			this.tbTopicInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbTopicInfo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
			this.tbTopicInfo.Location = new System.Drawing.Point(62, 484);
			this.tbTopicInfo.Multiline = true;
			this.tbTopicInfo.Name = "tbTopicInfo";
			this.tbTopicInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbTopicInfo.Size = new System.Drawing.Size(538, 130);
			this.tbTopicInfo.TabIndex = 10;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(989, 644);
			this.Controls.Add(this.tabControl);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "KakaHelper by TaPaKaH";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.tabControl.ResumeLayout(false);
			this.tabPageSetting.ResumeLayout(false);
			this.tabPageSetting.PerformLayout();
			this.radMessageSetting.PanelContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.radMessageSetting)).EndInit();
			this.radMessageSetting.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.rbKey)).EndInit();
			this.rbKey.ResumeLayout(false);
			this.rbKey.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.rbKeyIgnore)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rbKeyInt)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rbKeyString)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rbKeyLong)).EndInit();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.radModeSwitch)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBoxTime.ResumeLayout(false);
			this.groupBoxTime.PerformLayout();
			this.tabPageSubsriber.ResumeLayout(false);
			this.tabPageSubsriber.PerformLayout();
			this._panel.ResumeLayout(false);
			this._panel.PerformLayout();
			this._statusStrip.ResumeLayout(false);
			this._statusStrip.PerformLayout();
			this.tabPageProducer.ResumeLayout(false);
			this.tabPageProducer.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cntToSend)).EndInit();
			this.tabPageStatistics.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._radChartView)).EndInit();
			this.tabPageLog.ResumeLayout(false);
			this.tabPageLog.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._dataGridViewSubscriber)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._consumerDataSet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.messagesBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnSubscribe;
		private System.Windows.Forms.CheckedListBox chklTopics;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageSetting;
		private System.Windows.Forms.TabPage tabPageSubsriber;
		private System.Windows.Forms.Button btnUncheckAll;
		private System.Windows.Forms.Button btnCheckAll;
		private System.Windows.Forms.TabPage tabPageProducer;
		private System.Windows.Forms.Button btnUnSubscribe;
		private System.Windows.Forms.BindingSource messagesBindingSource;
		private KafkaHelpers.Model.ConsumerDataSet _consumerDataSet;
		private System.Windows.Forms.Button btnReadTopics;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cmbProducerTopic;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnSendMessage;
		private System.Windows.Forms.TextBox tbProducerValue;
		private System.Windows.Forms.TextBox tbProducerKey;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.DateTimePicker dateTimeEnd;
		private System.Windows.Forms.DateTimePicker dateTimeStart;
		private System.Windows.Forms.GroupBox groupBoxTime;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox chkTimestamp;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox tbKey;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btnUnSubscribe2;
		private System.Windows.Forms.Button btnSubscribe2;
		private System.Windows.Forms.StatusStrip _statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel _toolStripLabel;
		private System.Windows.Forms.ToolStripStatusLabel _toolStripStatus;
		private System.Windows.Forms.ToolStripProgressBar _toolStripProgressBar;
		private System.Windows.Forms.TextBox tbValue;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox ctbKafkaServer;
		private System.Windows.Forms.Button btnMax;
		private System.Windows.Forms.ToolStripStatusLabel toolStripKafkaServer;
		private System.Windows.Forms.ToolStripStatusLabel _toolStripKafkaServerValue;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox tbCounter;
		private System.Windows.Forms.ToolStripStatusLabel _tsStatusConsumer;
		private System.Windows.Forms.NumericUpDown cntToSend;
		private System.Windows.Forms.Button btnView;
		private System.Windows.Forms.Panel _panel;
		private System.Windows.Forms.DataGridView _dataGridViewSubscriber;
		private System.Windows.Forms.Button buttonFile;
		private System.Windows.Forms.TextBox textBoxFileToSend;
		private System.Windows.Forms.OpenFileDialog filetoSendDialog;
		private System.Windows.Forms.CheckBox chbDefaultJsonParse;
		private System.Windows.Forms.TextBox tbFilterTopics;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox tbMaxRows;
		private System.Windows.Forms.TabPage tabPageStatistics;
		private Telerik.WinControls.UI.RadChartView _radChartView;
		private Telerik.WinControls.Themes.TelerikMetroTheme _telerikMetroTheme;
        private Telerik.WinControls.UI.RadToggleSwitch radModeSwitch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel _toolStatisticsLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Telerik.WinControls.UI.RadCollapsiblePanel radMessageSetting;
        private Telerik.WinControls.UI.RadRadioButton rbKeyInt;
        private Telerik.WinControls.UI.RadRadioButton rbKeyLong;
        private Telerik.WinControls.UI.RadRadioButton rbKeyString;
        private Telerik.WinControls.UI.RadGroupBox rbKey;
        private Telerik.WinControls.RootRadElement object_e312bac3_151b_4c5a_91ba_fa2dfce57017;
        private Telerik.WinControls.UI.RadRadioButton rbKeyIgnore;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Button btn_Setting;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeStamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Topic;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
		private System.Windows.Forms.TabPage tabPageLog;
		private System.Windows.Forms.TextBox tbConsoleLog;
		private System.Windows.Forms.TextBox tbTopicInfo;
	}
}

