using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PO;

namespace chart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Method meth = new Method(2.5, 2.5, 3, 2, 0.1, 5, 0.5, 0.3);

        private void Build_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Height, pictureBox1.Width);
            Chart ch = new Chart(pictureBox1);
            ch.Draw_Osi();
            Storage stor = meth.Method_Start();
            ch.Draw_Chart(stor);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Build.PerformClick();

        }
    }
}
