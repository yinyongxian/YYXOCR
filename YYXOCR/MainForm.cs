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
        private OpenFileDialog openFileDialog = new OpenFileDialog()
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
                    var pictureBox = new PictureBox
                    {
                        Image = bitmap,
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.CenterImage
                    };
                    splitContainer.Panel1.Controls.Add(pictureBox);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }
    }
}
