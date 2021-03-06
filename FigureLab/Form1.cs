using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FigureLab
{
    public partial class Form1 : Form
    {
        Figure figure;
        Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {          
            Point point1 = new Point((float)numericUpDown1.Value, (float)numericUpDown2.Value);
            Point point2 = new Point((float)numericUpDown3.Value, (float)numericUpDown4.Value);
            Point point3 = new Point((float)numericUpDown5.Value, (float)numericUpDown6.Value);
            Point point4 = new Point((float)numericUpDown7.Value, (float)numericUpDown8.Value);
            try
            {
                figure = new Figure(point1, point2, point3,point4);
                textBox1.Text = figure.GetTypeFigure().ToString();

            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }

            using (Graphics g = Graphics.FromImage(bmp))
            {
                PointF[] pointsf = new PointF[4];
                pointsf[0] = new PointF((float)figure.Points[0].X, (float)figure.Points[0].Y);
                pointsf[1] = new PointF((float)figure.Points[1].X, (float)figure.Points[1].Y);
                pointsf[2] = new PointF((float)figure.Points[2].X, (float)figure.Points[2].Y);
                pointsf[3] = new PointF((float)figure.Points[3].X, (float)figure.Points[3].Y);


                float minX = Math.Min(pointsf[0].X, pointsf[1].X);
                minX = Math.Min(minX, pointsf[2].X);
                minX = Math.Min(minX, pointsf[3].X);

                float minY = Math.Min(pointsf[0].Y, pointsf[1].Y);
                minY = Math.Min(minX, pointsf[2].Y);
                minY = Math.Min(minX, pointsf[3].Y);

                if (minX<0)
                {
                    for (int i = 0; i < pointsf.Length; i++)
                        pointsf[i].X -= minX;
                }
                if (minY < 0)
                {
                    for (int i = 0; i < pointsf.Length; i++)
                        pointsf[i].Y -= minY;
                }

                float maxX = Math.Max(pointsf[0].X, pointsf[1].X);
                maxX = Math.Max(maxX, pointsf[2].X);
                maxX = Math.Max(maxX, pointsf[3].X);

                float maxY = Math.Max(pointsf[0].Y, pointsf[1].Y);
                maxY = Math.Max(maxY, pointsf[2].Y);
                maxY = Math.Max(maxY, pointsf[3].Y);

                float k1 = pictureBox1.ClientSize.Width / maxX;
                float k2 = pictureBox1.ClientSize.Height / maxY;

                float k = Math.Min(k1, k2);

                for (int i = 0; i < pointsf.Length; i++)
                {
                    pointsf[i].X *= k;
                    pointsf[i].Y *= k;
                }

                g.Clear(Color.Transparent);
                g.FillPolygon(Brushes.Red, pointsf);
            }
            /// Назначаем наш Bitmap свойству Image
            pictureBox1.Image = bmp;
        }
    }
}
