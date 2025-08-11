using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Brnach___Bound_algorithm
{
    public partial class Form1 : Form
    {
        private OpenFileDialog ofd;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;

                try
                {
                    //Read file as UTF-8
                    string text = System.IO.File.ReadAllText(ofd.FileName, System.Text.Encoding.UTF8);
                    if (PrettyPrint.FormatLP(text, out string formatted))
                    {
                        rtbPreview.Clear();
                        rtbPreview.Text = formatted;
                    }
                    else
                    {
                        // If it can't pretty format, just show raw text
                        rtbPreview.Clear();
                        rtbPreview.Text = text;
                    }

                    rtbPreview.SelectionStart = 0;
                    rtbPreview.ScrollToCaret();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not read file:\n{ex.Message}", "Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
