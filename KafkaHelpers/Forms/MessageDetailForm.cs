using ScintillaNET;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace KafkaHelpers
{
    public partial class MessageDetailForm : Form
    {
        private MessageEntity<string> _entiry = new MessageEntity<string>();

        public MessageEntity<string> Entity
        {
            get { return _entiry; }
        }

        private string TextValue;

        private ScintillaNET.Scintilla TextArea;
        private const int BACK_COLOR = 0xe3e2dd;
        private const int FORE_COLOR = 0xB7B7B7;
        private const int NUMBER_MARGIN = 1;
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;
        private const int FOLDING_MARGIN = 3;
        private const bool CODEFOLDING_CIRCULAR = true;

        public MessageDetailForm()
        {
            InitializeComponent();
        }

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        private void InitNumberMargin()
        {
            TextArea.Styles[Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            TextArea.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = TextArea.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            TextArea.MarginClick += TextArea_MarginClick;
        }

        private void TextArea_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = TextArea.Lines[TextArea.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }
        }


        private void MessageDetailForm_Load(object sender, EventArgs e)
        {
            // CREATE CONTROL
            TextArea = new ScintillaNET.Scintilla();
            messagePanel.Controls.Add(TextArea);
            // BASIC CONFIG
            TextArea.Dock = System.Windows.Forms.DockStyle.Fill;

            // INITIAL VIEW CONFIG
            TextArea.WrapMode = WrapMode.Word;
            TextArea.IndentationGuides = IndentView.LookBoth;


            // NUMBER MARGIN
            InitNumberMargin();

            tbId.Text = _entiry.Id.ToString();
            tbTopic.Text = _entiry.Topic.ToString();
            tbKey.Text = _entiry.KeyString;
            lbSize.Text = RowFormatter.FormatSize(System.Text.ASCIIEncoding.Unicode.GetByteCount(_entiry.Message));
            TextValue = _entiry.Message;

            try
            {
                if (_entiry.DefaultJsonParse)
                {
                    TextValue = (string)JsonHelper.FormatJson(_entiry.Message);
                }
            }
            catch { }

            TextArea.Text = TextValue;
            TextArea.ReadOnly = true;
        }

        private void btnJson_Click(object sender, EventArgs e)
        {
            TextArea.ReadOnly = false;
            try
            {
                TextArea.Text = (string)JsonHelper.FormatJson(_entiry.Message);
            }
            catch (Exception)
            {
                TextArea.Text = TextValue;
                throw;
            }
            TextArea.ReadOnly = true;
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
            TextArea.ReadOnly = false;
            TextArea.Text = _entiry.Message;
            TextArea.ReadOnly = true;
        }
    }
}