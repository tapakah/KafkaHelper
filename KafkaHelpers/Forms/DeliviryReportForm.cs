﻿using KafkaHelpers.Model;
using System;
using System.Text;
using System.Windows.Forms;

namespace KafkaHelpers
{
    public partial class DeliviryReportForm : Form
    {
        public DeliviryStatus Report = new DeliviryStatus();

        public DeliviryReportForm()
        {
            InitializeComponent();
        }

        private void DeliviryReportForm_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Message sended: {0}", Report.Count.ToString()));
            sb.AppendLine(string.Format("Message size: {0}", Report.MessageSize));
            sb.AppendLine(string.Format("Total time: {0}ms", (Report.Finish - Report.Start).TotalMilliseconds));
            sb.AppendLine(string.Format("AVG time: {0}ms", ((Report.Finish - Report.Start).TotalMilliseconds) / Report.Count).ToString());

            tbResult.Text = sb.ToString();
        }
    }
}