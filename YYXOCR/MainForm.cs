using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YYXOCR
{
    public partial class MainForm : Form
    {
        private readonly OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            Multiselect = false
        };

        public MainForm()
        {
            InitializeComponent();
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            splitContainer.Panel1.Controls.Clear();
            splitContainer.Panel2.Controls.Clear();

            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var fileName = openFileDialog.FileName;

                Convert(fileName);
            }
        }

        private void Convert(string fileName)
        {
            Task.Factory.StartNew(() =>
            {
                SetStatusLabel(@"解析中...");

                try
                {
                    var bitmap = new Bitmap(fileName);

                    AddPictureBox(bitmap);

                    var text = TesseractConvert.ToText(bitmap);

                    AddRichTextBox(text);

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
                finally
                {
                    SetStatusLabel(string.Empty);
                }
            });
        }

        private void SetStatusLabel(string text)
        {
            BeginInvoke(new Action(() =>
            {
                toolStripStatusLabel.Text = text;
            }));
        }

        private void AddPictureBox(Bitmap bitmap)
        {
            BeginInvoke(new Action(() =>
            {
                var pictureBox = new PictureBox
                {
                    Image = bitmap,
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.Zoom
                };
                splitContainer.Panel1.Controls.Add(pictureBox);
            }));
        }

        private void AddRichTextBox(string text)
        {
            BeginInvoke(new Action(() =>
            {
                var richTextBox = new RichTextBox
                {
                    BorderStyle = BorderStyle.None,
                    Dock = DockStyle.Fill,
                    Text = text
                };
                splitContainer.Panel2.Controls.Add(richTextBox);
            }));
        }
    }
}
