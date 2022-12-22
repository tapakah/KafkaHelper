using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafkaHelpers
{
        public partial class MessageDetailForm : Form
        {
                private MessageDetailEntity _entiry = new MessageDetailEntity();
                public MessageDetailEntity Entity { get { return _entiry; } }

                public MessageDetailForm()
                {
                        InitializeComponent();
                }


                private void MessageDetailForm_Load(object sender, EventArgs e)
                {
                        tbId.Text = _entiry.Id.ToString();
                        tbTopic.Text = _entiry.Topic.ToString();
                        tbKey.Text = _entiry.KeyString;
                        lbSize.Text = RowFormatter.FormatSize(System.Text.ASCIIEncoding.Unicode.GetByteCount(_entiry.Message));

                        try
                        {
                                if (_entiry.DefaultJsonParse)
                                {
                                        tbValue.Text = (string)JsonHelper.FormatJson(_entiry.Message);
                                }
                                else
                                {
                                        tbValue.Text = _entiry.Message;
                                }
                        }
                        catch (Exception)
                        {
                                tbValue.Text = _entiry.Message;
                        }

                }

                private void btnJson_Click(object sender, EventArgs e)
                {
                        try
                        {
                                tbValue.Text = (string)JsonHelper.FormatJson(_entiry.Message);
                        }
                        catch (Exception)
                        {
                                tbValue.Text = _entiry.Message;
                                throw;
                        }
                }

                protected override bool ProcessDialogKey(Keys keyData)
                {
                        if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
                        {
                                this.Close();
                                return true;
                        }
                        return base.ProcessDialogKey(keyData);
                }

                private void btnTextView_Click(object sender, EventArgs e)
                {
                        tbValue.Text = _entiry.Message;
                }
        }
}
