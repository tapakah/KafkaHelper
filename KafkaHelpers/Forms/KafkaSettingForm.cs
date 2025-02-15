﻿using KafkaHelpers.Model;
using KafkaHelpers.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafkaHelpers.Forms
{
	public partial class KafkaSettingForm : Form
	{
		private KafkaSettingEntity _setting { get; set; }
		public KafkaSettingEntity Setting { get { return _setting; } set { _setting = value; } }
		private static bool isSaved = false;
		public KafkaSettingForm()
		{
			InitializeComponent();
		}

		private void KafkaSettingForm_Load(object sender, EventArgs e)
		{
			isSaved = false;
			lstSecurityProtocol.SelectedValue = Setting.SecurityProtocol ?? KafkaProtocol.PLAINTEXT;
			tbSslCaLocation.Text = Setting.SslCaLocation;
			tbSslCertificateLocation.Text = Setting.SslCertificateLocation;
			tbSslKeyLocation.Text = Setting.SslKeyLocation;
			tbSslKeyPassword.Text = Setting.SslKeyPassword;

			lstSaslMechanism.SelectedValue = Setting.SaslMechanism ?? KafkaSaslMechanism.PLAIN;
			tbSaslUsername.Text = Setting.SaslUsername;
			tbSaslPassword.Text = Setting.SaslPassword;
			tbDebug.Text = Setting.Debug;
		}

		private void KafkaSettingForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (isSaved) return;

			DialogResult result = MessageBox.Show("Do you want save settings?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				StoreSettings();
			}
		}

		private void StoreSettings()
		{
			Setting.SecurityProtocol = lstSecurityProtocol.SelectedItem != null ? lstSecurityProtocol.SelectedItem.Text : null;
			Setting.SslCaLocation = string.IsNullOrEmpty(tbSslCaLocation.Text) ? null : tbSslCaLocation.Text;
			Setting.SslCertificateLocation = string.IsNullOrEmpty(tbSslCertificateLocation.Text) ? null : tbSslCertificateLocation.Text;
			Setting.SslKeyLocation = string.IsNullOrEmpty(tbSslKeyLocation.Text) ? null : tbSslKeyLocation.Text;
			Setting.SslKeyPassword = string.IsNullOrEmpty(tbSslKeyPassword.Text) ? null : tbSslKeyPassword.Text;

			Setting.SaslMechanism = lstSaslMechanism.SelectedItem != null ? lstSaslMechanism.SelectedItem.Text : null;
			Setting.SaslUsername = string.IsNullOrEmpty(lbSaslUsername.Text) ? null : tbSaslUsername.Text;
			Setting.SaslPassword = string.IsNullOrEmpty(tbSaslPassword.Text) ? null : tbSaslPassword.Text;
			Setting.Debug = string.IsNullOrEmpty(tbDebug.Text) ? null : tbDebug.Text;
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			_setting = new KafkaSettingEntity()
			{
				ServerName = Setting.ServerName,
				SecurityProtocol = KafkaSettingEntity.SettingToSecurityProtocol(KafkaProtocol.PLAINTEXT).ToString().ToLower(),
				SaslMechanism = KafkaSettingEntity.SettingToSaslMechanism(KafkaSaslMechanism.PLAIN).ToString()
			};

			KafkaSettingForm_Load(this, null);
		}

		private void btn_Save_Click(object sender, EventArgs e)
		{
			StoreSettings();
			isSaved = true;
			this.Close();
		}
	}
}
