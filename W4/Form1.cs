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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int sec = 0; // Милисекунды
        int w = 80, h = 40; // Размеры прямоугольной области для прорисовки
        int x = 1, y = 1; // Координаты фигуры (Верхний левый угол)
        int dx = 5; // Шаг при движении фигуры
        int dy = 5;

        //переменные для увеличения и уменьшения эллипса
        float sin = 0;
        float sinScale = 5;
        float sinTime = 0.3f;

        Form2 form2;
        
        public enum STATUS { Left, Right, Up, Down }; // Режимы движения
        public STATUS flag;

        public enum MOVEMENT { Forward, Back };
        public MOVEMENT flagMove;

        //кисти
        SolidBrush brush = new SolidBrush(Color.White);
        SolidBrush brush2 = new SolidBrush(Color.Blue);
        SolidBrush brush3 = new SolidBrush(Color.Green);
        Rectangle rc; // Прямоугольная область

        

        public int MySpeed 
        {
            get
            {
                return 100 - timer1.Interval;
            }
            set
            {
                timer1.Interval = 100 - value;
            }
        }


        public Color MyColorForward
        {
            get { return brush3.Color; }
            set { brush3.Color = value; }
        }


        public Color MyColorBack
        {
            get { return brush2.Color; }
            set { brush2.Color = value; }
        }

        

        public bool form2Closed
        {
            get;
            set;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sec++;  // секунды
            rc = new Rectangle(x, y, w, h); // размер прямоугольной области
            this.Invalidate(rc, true);      // вызываем прорисовку области            

            w += (int)(Math.Sin(sin) * sinScale);
            h += (int)(Math.Cos(-sin) * sinScale);
            sin += sinTime;


            if (flag == STATUS.Left) // движение влево
                x -= dx;
            if (flag == STATUS.Right) // движение вправо
                x += dx;
            if (flag == STATUS.Up) // движение вверх
                y -= dy;
            if (flag == STATUS.Down) // движение вниз
                y += dy;

            if (flagMove == MOVEMENT.Forward) //при движении вверх-вниз
            {
                
                if (y >= (this.ClientSize.Height - h)) // если достигли нижнего края формы
                {
                    flag = STATUS.Up; // меняем статус движения на верхний
                    brush.Color = brush2.Color;
                   
                }
                else
                if (y <= 1) // если достигли верхнего края формы
                {
                    flag = STATUS.Down;    // меняем статус движения на нижний
                    brush.Color = brush3.Color;
                }
            }
            else if (flagMove == MOVEMENT.Back) //при движении влево-вправо
            {
                if (x <= 1) // если достигли правого края формы
                {
                    flag = STATUS.Right; // меняем статус движения на правый
                    brush.Color = brush3.Color;
                }
                else
                if (x >= (this.ClientSize.Width - w)) // если достигли левого края формы
                {
                    flag = STATUS.Left;    // меняем статус движения на левый
                    brush.Color = brush2.Color;
                }
            }
            rc = new Rectangle(x, y, w, h); // новая прямоугольная область
            this.Invalidate(rc, true);  // вызываем прорисовку этой области

        }

        private void button1_Click(object sender, EventArgs e) //при нажатии на кнопку старта/остановки
        {
            if (timer1.Enabled == false)
            {
                timer1.Start();
                button1.Text = "Stop";
            }
            else
            {
                timer1.Stop();
                button1.Text = "Start";
            }
        }

        

        private void button2_Click(object sender, EventArgs e) //при нажатии на кнопку настроек открывается форма настроек
        {
            if (form2Closed == true)
            {
                timer1.Start();
                button1.Text = "Stop";
                form2 = new Form2();
                form2.Owner = this;
                form2.Show();
                form2Closed = false;
                
            }
        }

        private void Form1_Paint_1(object sender, PaintEventArgs e) 
        {
            if (flagMove == MOVEMENT.Forward)
                e.Graphics.FillEllipse(brush, rc);
            else if (flagMove == MOVEMENT.Back)
                e.Graphics.FillEllipse(brush, rc);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rc = new Rectangle(x, y, w, h);
            this.Invalidate(rc, true);
            flag = STATUS.Right;
            flagMove = MOVEMENT.Forward;
            form2Closed = true;

            MyColorForward = Properties.Settings.Default.My_ColorForward;
            MyColorBack = Properties.Settings.Default.My_ColorBack;
            MySpeed = Properties.Settings.Default.My_Speed;
        }

        

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.My_ColorForward = MyColorForward;
            Properties.Settings.Default.My_ColorBack = MyColorBack;
            Properties.Settings.Default.My_Speed = MySpeed;
            Properties.Settings.Default.Save();
        }
    }

}
