using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Form1 : Form
    {
        Figure figure;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {          
            Point point1 = new Point((float)numericUpDown1.Value, (float)numericUpDown2.Value);
            Point point2 = new Point((float)numericUpDown3.Value, (float)numericUpDown4.Value);
            Point point3 = new Point((float)numericUpDown5.Value, (float)numericUpDown6.Value);
            Point point4 = new Point((float)numericUpDown7.Value, (float)numericUpDown8.Value);
            try
            {
                figure = new Figure(ref point1, ref point2, ref point3, ref point4);
                numericUpDown1.Value = (decimal)point1.X;
                numericUpDown2.Value = (decimal)point1.Y;
                numericUpDown3.Value = (decimal)point2.X;
                numericUpDown4.Value = (decimal)point2.Y;
                numericUpDown5.Value = (decimal)point3.X;
                numericUpDown6.Value = (decimal)point3.Y;
                numericUpDown7.Value = (decimal)point4.X;
                numericUpDown8.Value = (decimal)point4.Y;
                textBox1.Text = figure.GetTypeFigure().ToString();
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }
        }
    }
}
