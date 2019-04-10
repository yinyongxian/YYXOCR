using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var fileName = openFileDialog.FileName;

                try
                {
                    var bitmap = new Bitmap(fileName);

                    AddPictureBox(bitmap);

                    var blackAndWhiteBitmap = bitmap.ToBlackAndWhite();
                    var text = TesseractConvert.ToText(blackAndWhiteBitmap);

                    AddRichTextBox(text);

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }

        private void AddPictureBox(Bitmap bitmap)
        {
            var pictureBox = new PictureBox
            {
                Image = bitmap,
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            splitContainer.Panel1.Controls.Add(pictureBox);
        }

        private void AddRichTextBox(string text)
        {
            var richTextBox = new RichTextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
                Text = text
            };
            splitContainer.Panel2.Controls.Add(richTextBox);
        }
    }
}
