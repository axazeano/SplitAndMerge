using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplitAndMerge
{
    public partial class Form1 : Form
    {
        private Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color testColor = Color.FromArgb(10, 10, 10);
            HSB hsb = new HSB(testColor);
            Color testColor2 = hsb.getRGB();
            Console.Out.WriteLine(testColor2.R + " " + testColor2.G + " " + testColor2.B);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(openFileDialog1.FileName);
                pictureBox.Image = image;
            }
        }

        private void splitToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void setImage(Bitmap image)
        {
            pictureBox.Image = image;
        }

        private void graySplitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraySplit split = new GraySplit((Bitmap)pictureBox.Image, 1);
            Rectangle region = new Rectangle(0, 0, pictureBox.Image.Width, pictureBox.Image.Height);
            split.split(setImage);
        }
    }
}
