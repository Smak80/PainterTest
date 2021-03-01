using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndCapTest
{
    public partial class Form1 : Form
    {
        private Painter p;
        private bool pressed = false;

        public Form1()
        {
            InitializeComponent();
            p = new Painter();
            p.Thickness = 10;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pressed = true;
            if (e.Button == MouseButtons.Right) 
                p.ObjectType = Painter.DrawType.Arrow;
            if (e.Button == MouseButtons.Left) p.ObjectType = Painter.DrawType.Line;
            p.SetStartPoint(e.Location);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pressed = false;
            p.Paint(panel1.CreateGraphics());
            panel1.Refresh();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pressed)
            {
                p.SetPoint(e.Location, 2);
                p.Preview(panel1.CreateGraphics());
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            p.Show(panel1.CreateGraphics());
        }
    }
}
