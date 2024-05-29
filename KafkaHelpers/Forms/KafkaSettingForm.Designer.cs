namespace KafkaHelpers.Forms
{
    partial class KafkaSettingForm
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem5 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem6 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem7 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem8 = new Telerik.WinControls.UI.RadListDataItem();
            this.lstSecurityProtocol = new Telerik.WinControls.UI.RadDropDownList();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSslCertificateLocation = new System.Windows.Forms.Label();
            this.lbSslKeyLocation = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lstSaslMechanism = new Telerik.WinControls.UI.RadDropDownList();
            this.lbSaslUsername = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbSslCaLocation = new Telerik.WinControls.UI.RadTextBox();
            this.tbSslCertificateLocation = new Telerik.WinControls.UI.RadTextBox();
            this.tbSslKeyLocation = new Telerik.WinControls.UI.RadTextBox();
            this.tbSslKeyPassword = new Telerik.WinControls.UI.RadTextBox();
            this.tbSaslUsername = new Telerik.WinControls.UI.RadTextBox();
            this.tbSaslPassword = new Telerik.WinControls.UI.RadTextBox();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lstSecurityProtocol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstSaslMechanism)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSslCaLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSslCertificateLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSslKeyLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSslKeyPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSaslUsername)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSaslPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // lstSecurityProtocol
            // 
            this.lstSecurityProtocol.DropDownAnimationEnabled = false;
            this.lstSecurityProtocol.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            radListDataItem1.Text = "plaintext";
            radListDataItem2.Text = "ssl";
            radListDataItem3.Text = "sasl_plaintext";
            radListDataItem4.Text = "sasl_ssl";
            this.lstSecurityProtocol.Items.Add(radListDataItem1);
            this.lstSecurityProtocol.Items.Add(radListDataItem2);
            this.lstSecurityProtocol.Items.Add(radListDataItem3);
            this.lstSecurityProtocol.Items.Add(radListDataItem4);
            this.lstSecurityProtocol.Location = new System.Drawing.Point(148, 3);
            this.lstSecurityProtocol.Name = "lstSecurityProtocol";
            this.lstSecurityProtocol.Size = new System.Drawing.Size(125, 20);
            this.lstSecurityProtocol.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(12, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "security.protocol:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "ssl.ca.location:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbSslCertificateLocation
            // 
            this.lbSslCertificateLocation.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbSslCertificateLocation.Location = new System.Drawing.Point(12, 56);
            this.lbSslCertificateLocation.Name = "lbSslCertificateLocation";
            this.lbSslCertificateLocation.Size = new System.Drawing.Size(130, 13);
            this.lbSslCertificateLocation.TabIndex = 20;
            this.lbSslCertificateLocation.Text = "ssl.certificate.location:";
            this.lbSslCertificateLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbSslKeyLocation
            // 
            this.lbSslKeyLocation.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbSslKeyLocation.Location = new System.Drawing.Point(12, 80);
            this.lbSslKeyLocation.Name = "lbSslKeyLocation";
            this.lbSslKeyLocation.Size = new System.Drawing.Size(130, 13);
            this.lbSslKeyLocation.TabIndex = 21;
            this.lbSslKeyLocation.Text = "ssl.key.location:";
            this.lbSslKeyLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "ssl.key.password:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(12, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "sasl.mechanism:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstSaslMechanism
            // 
            this.lstSaslMechanism.DropDownAnimationEnabled = false;
            this.lstSaslMechanism.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            radListDataItem5.Text = "GSSAPI";
            radListDataItem6.Text = "PLAIN";
            radListDataItem7.Text = "SCRAM-SHA-256";
            radListDataItem8.Text = "SCRAM-SHA-512";
            this.lstSaslMechanism.Items.Add(radListDataItem5);
            this.lstSaslMechanism.Items.Add(radListDataItem6);
            this.lstSaslMechanism.Items.Add(radListDataItem7);
            this.lstSaslMechanism.Items.Add(radListDataItem8);
            this.lstSaslMechanism.Location = new System.Drawing.Point(148, 126);
            this.lstSaslMechanism.Name = "lstSaslMechanism";
            this.lstSaslMechanism.Size = new System.Drawing.Size(125, 20);
            this.lstSaslMechanism.TabIndex = 24;
            // 
            // lbSaslUsername
            // 
            this.lbSaslUsername.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbSaslUsername.Location = new System.Drawing.Point(12, 155);
            this.lbSaslUsername.Name = "lbSaslUsername";
            this.lbSaslUsername.Size = new System.Drawing.Size(130, 13);
            this.lbSaslUsername.TabIndex = 25;
            this.lbSaslUsername.Text = "sasl.username:";
            this.lbSaslUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(12, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "sasl.password:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbSslCaLocation
            // 
            this.tbSslCaLocation.Location = new System.Drawing.Point(148, 28);
            this.tbSslCaLocation.Name = "tbSslCaLocation";
            this.tbSslCaLocation.Size = new System.Drawing.Size(356, 20);
            this.tbSslCaLocation.TabIndex = 27;
            // 
            // tbSslCertificateLocation
            // 
            this.tbSslCertificateLocation.Location = new System.Drawing.Point(148, 52);
            this.tbSslCertificateLocation.Name = "tbSslCertificateLocation";
            this.tbSslCertificateLocation.Size = new System.Drawing.Size(356, 20);
            this.tbSslCertificateLocation.TabIndex = 28;
            // 
            // tbSslKeyLocation
            // 
            this.tbSslKeyLocation.Location = new System.Drawing.Point(148, 76);
            this.tbSslKeyLocation.Name = "tbSslKeyLocation";
            this.tbSslKeyLocation.Size = new System.Drawing.Size(356, 20);
            this.tbSslKeyLocation.TabIndex = 28;
            // 
            // tbSslKeyPassword
            // 
            this.tbSslKeyPassword.Location = new System.Drawing.Point(148, 102);
            this.tbSslKeyPassword.Name = "tbSslKeyPassword";
            this.tbSslKeyPassword.Size = new System.Drawing.Size(356, 20);
            this.tbSslKeyPassword.TabIndex = 28;
            // 
            // tbSaslUsername
            // 
            this.tbSaslUsername.Location = new System.Drawing.Point(148, 151);
            this.tbSaslUsername.Name = "tbSaslUsername";
            this.tbSaslUsername.Size = new System.Drawing.Size(356, 20);
            this.tbSaslUsername.TabIndex = 28;
            // 
            // tbSaslPassword
            // 
            this.tbSaslPassword.Location = new System.Drawing.Point(148, 178);
            this.tbSaslPassword.Name = "tbSaslPassword";
            this.tbSaslPassword.Size = new System.Drawing.Size(356, 20);
            this.tbSaslPassword.TabIndex = 28;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClear.Location = new System.Drawing.Point(447, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(57, 22);
            this.btnClear.TabIndex = 29;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // KafkaSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 202);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.tbSaslPassword);
            this.Controls.Add(this.tbSaslUsername);
            this.Controls.Add(this.tbSslKeyPassword);
            this.Controls.Add(this.tbSslKeyLocation);
            this.Controls.Add(this.tbSslCertificateLocation);
            this.Controls.Add(this.tbSslCaLocation);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lbSaslUsername);
            this.Controls.Add(this.lstSaslMechanism);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbSslKeyLocation);
            this.Controls.Add(this.lbSslCertificateLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lstSecurityProtocol);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "KafkaSettingForm";
            this.Text = "Settings";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KafkaSettingForm_FormClosing);
            this.Load += new System.EventHandler(this.KafkaSettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lstSecurityProtocol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstSaslMechanism)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSslCaLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSslCertificateLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSslKeyLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSslKeyPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSaslUsername)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSaslPassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDropDownList lstSecurityProtocol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbSslCertificateLocation;
        private System.Windows.Forms.Label lbSslKeyLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Telerik.WinControls.UI.RadDropDownList lstSaslMechanism;
        private System.Windows.Forms.Label lbSaslUsername;
        private System.Windows.Forms.Label label8;
        private Telerik.WinControls.UI.RadTextBox tbSslCaLocation;
        private Telerik.WinControls.UI.RadTextBox tbSslCertificateLocation;
        private Telerik.WinControls.UI.RadTextBox tbSslKeyLocation;
        private Telerik.WinControls.UI.RadTextBox tbSslKeyPassword;
        private Telerik.WinControls.UI.RadTextBox tbSaslUsername;
        private Telerik.WinControls.UI.RadTextBox tbSaslPassword;
        private System.Windows.Forms.Button btnClear;
    }
}