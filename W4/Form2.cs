using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace W4
{
    public partial class Form2 : Form
    {
        Form1 fmain;
        public Form2()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            fmain.MySpeed = trackBar1.Value;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            fmain = (Form1)this.Owner;
            trackBar1.Value = fmain.MySpeed;
            button1.ForeColor = fmain.MyColorForward;
            button2.ForeColor = fmain.MyColorBack;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            fmain.form2Closed = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                fmain.MyColorForward = button1.ForeColor = colorDialog1.Color;
            //else
            //    fmain.MyColorForward = button1.ForeColor = Color.Brown;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                fmain.MyColorBack = button2.ForeColor = colorDialog1.Color;
            //else
            //    fmain.MyColorBack = button2.ForeColor = Color.Blue;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (fmain.flagMove == Form1.MOVEMENT.Forward)
            {
                //fmain.timer1.Stop();
                fmain.flag = Form1.STATUS.Right;
                fmain.flagMove = Form1.MOVEMENT.Back;
                //fmain.timer1.Start();
            }
            else
            {
                //fmain.timer1.Stop();
                fmain.flag = Form1.STATUS.Down;
                fmain.flagMove = Form1.MOVEMENT.Forward;
                //fmain.timer1.Start();
            }
        }

        
    }
}
