using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace PO
{
    class Chart
    {
        int x, y;
        PictureBox pic_Box;
        Pen pen = new Pen(Color.Green);
        Pen peb_b = new Pen(Color.Black, 2);
        Pen pen_c = new Pen(Color.Black, 2);
        double[,] mas;

        public Chart(int x, int y, PictureBox PB, double[,] mas)
        {
            this.x = x;
            this.y = y;
            pic_Box = PB;
            this.mas = mas;
        }

        public Chart(PictureBox p)
        {
            pic_Box = p;
            x = pic_Box.Width / 2;
            y = pic_Box.Height / 2;
        }

        public void Draw_Osi()//рисование осей, переделанное
        {
            pen_c.StartCap = LineCap.ArrowAnchor;
            SolidBrush b = new SolidBrush(Color.Black);
            Graphics cgraph = Graphics.FromImage(pic_Box.Image);
            cgraph.DrawRectangle(pen, x, y, 2, 2);

            cgraph.DrawLine(peb_b, 0, pic_Box.Height / 2, pic_Box.Width, pic_Box.Height / 2);//горизонтальная ось * в норме)
            cgraph.DrawLine(peb_b, pic_Box.Width / 2, 0, pic_Box.Width / 2, pic_Box.Height);//Вертикальная ось * в норме)

            cgraph.DrawLine(peb_b, pic_Box.Width, pic_Box.Height / 2, pic_Box.Width - 17, pic_Box.Height / 2 + 4);// низ горизонтальной стрелки * в норме
            cgraph.DrawLine(peb_b, pic_Box.Width, pic_Box.Height / 2, pic_Box.Width - 17, pic_Box.Height / 2 - 5);// верх горизонтальной стрелки * в норме

            cgraph.DrawLine(pen_c, pic_Box.Width / 2 - 4, 17, pic_Box.Width / 2 - 1, 0); //левая часть верхней стрелки * в норме
            cgraph.DrawLine(pen_c, pic_Box.Width / 2 + 4, 17, pic_Box.Width / 2 - 1, 0);//правая * в номе

            cgraph.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Font font = new Font("Times New Roman", 12, FontStyle.Bold);
            Font font1 = new Font("Times New Roman", 7, FontStyle.Bold);
            cgraph.DrawString("y", font, b, pic_Box.Width / 2 + 23, 0);//буква
            cgraph.DrawString("x", font, b, pic_Box.Width - 10, pic_Box.Height / 2 + 10);//буква

            for (int i = 2; i < pic_Box.Width - 10; i += 18)//чёрточки на горизонтальной * норм //GT7B8-54LHP-VZ053 игры//
            {
                cgraph.DrawLine(peb_b, -1 + i, (pic_Box.Height / 2 - 3), -1 + i, (pic_Box.Height / 2 + 3));// 

            }
            for (int i = 2; i < pic_Box.Height - 10; i += 18)//чёрточки на вертикальной * норм
            {
                //if(i<)
                cgraph.DrawLine(peb_b, pic_Box.Width / 2 - 3, pic_Box.Height - i, pic_Box.Width / 2 + 3, pic_Box.Height - i);//
            }

            int i1 = -100;
            for (int j = 2; j < pic_Box.Height; j += 19)//числа вертикальной оси
            {
                cgraph.DrawString(Convert.ToString(i1), font1, b, pic_Box.Width / 2 + 8, pic_Box.Height - 9 - j);
                i1 += 10;
                if (i1 == 0)
                {
                    i1 += 10;
                    j += 20;
                }
            }

            int i2 = -100;
            for (int j = 2; j < pic_Box.Height; j += 19)//числа горизонтальной оси
            {
                if (i2 != 0)
                {
                    cgraph.DrawString(Convert.ToString(i2), font1, b, 0 + j, pic_Box.Height / 2 + 5);
                }
                if (i2 == -100)
                {
                    j += 4;
                }
                if (i2 == 10)
                {
                    j -= 4; //dis--;
                }
                if (i2 == 50)
                {
                    j -= 2;
                }
                if (i2 == 70)
                {
                    j -= 2;
                }
                i2 += 10;
                if (i2 == 0)
                {
                    i2 += 10;
                    j += 20;
                }
                if (i2 > 100)
                {
                    break;
                }
            }
        }

        public void Draw_Chart(Storage stor)// Прорисовка графика
        {
            Graphics graph = Graphics.FromImage(pic_Box.Image);
            Brush brush = new SolidBrush(Color.Green);
            Pen pen = new Pen(Color.OrangeRed, 1);
            double[] mas = new double[3];
            int pos_x = 0, pos_y = 0;
            int d_pos_x = 0, d_pos_y = 0;
            for (int i = 0; i < stor.Count; i++)
            {
                if (i != stor.Count - 1)
                {
                    i++;
                    mas = stor[i];
                    if (mas[2] != 0)
                    {
                        d_pos_x = pic_Box.Width / 2 + Convert.ToInt32(mas[0] * 100);
                        d_pos_y = pic_Box.Height / 2 - Convert.ToInt32(mas[1] * 100);
                        graph.DrawLine(pen, pos_x, pos_y, d_pos_x, d_pos_y);//
                    }
                    i--;
                }
                mas = stor[i];
                pos_x = pic_Box.Width / 2 + Convert.ToInt32(mas[0] * 100);
                pos_y = pic_Box.Height / 2 - Convert.ToInt32(mas[1] * 100);
                graph.FillEllipse(brush, pos_x - 3, pos_y - 4, 7, 7);//
            }
        }
    }
}
