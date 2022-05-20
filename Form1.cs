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

namespace kurs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            double a11 = Convert.ToDouble(textBox1.Text), a12 = Convert.ToDouble(textBox4.Text), a13 = Convert.ToDouble(textBox7.Text);  //ввод коэффициентов
            double a21 = Convert.ToDouble(textBox2.Text), a22 = Convert.ToDouble(textBox5.Text), a23 = Convert.ToDouble(textBox8.Text);
            double a31 = Convert.ToDouble(textBox3.Text), a32 = Convert.ToDouble(textBox6.Text), a33 = Convert.ToDouble(textBox9.Text);

            double b1 = Convert.ToDouble(textBox14.Text), b2 = Convert.ToDouble(textBox15.Text), b3 = Convert.ToDouble(textBox16.Text);   // столбец свободных членов

            double x1 = 0, x2 = 0, x3 = 0;       // начальное приближение
            double x1n, x2n, x3n;
            double eps = 0.000000001;
            int count;

            if ((Math.Abs(a11) > Math.Abs(a12) + Math.Abs(a13)) && (Math.Abs(a22) > Math.Abs(a21) + Math.Abs(a23)) && (Math.Abs(a33) > Math.Abs(a31) + Math.Abs(a32)))
                textBox13.Text = "диаганальное преобладание не нарушается \r\n";
            else
            {
                textBox13.Text = "диаганальное преобладание нарушается \r\n" ;
            }
            count = 0;
            do
            {
                x1n = (b1 - (a12 * x2 + a13 * x3 )) / a11;
                x2n = (b2 - (a21 * x1 + a23 * x3 )) / a22;
                x3n = (b3 - (a31 * x1 + a32 * x2 )) / a33;

                count = count + 1;
                if (Math.Abs(x1n - x1) < eps &&
                    Math.Abs(x2n - x2) < eps &&
                    Math.Abs(x3n - x3) < eps
                   ) break;
                x1 = x1n;
                x2 = x2n;
                x3 = x3n;
                

               
            } while (1>0);
            x1 = x1n;
            x2 = x2n;
            x3 = x3n;

            textBox13.Text =textBox13.Text + "Колличество итераций = " +  count + "\r\n x1 = " + x1 + "\r\n x2 = " + x2 + "\r\n x3 = " + x3 + "\n\t";

            

        }

        private void save(object sender, EventArgs e)
        {

            if (textBox13.Text == "")
            { 
                MessageBox.Show("нет данных\r\n выполните вычисления и нажмите 'пуск'", "Решение СЛАУ методом простых итераций", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return; 
            }

                SaveFileDialog sf;
                sf = new SaveFileDialog();
                sf.Filter = "Text files(*.txt)|*.txt| All files(*.*)|*.*";
                string filename;
                sf.ShowDialog();
                filename = sf.FileName;
           
            
                try
                {
                    File.WriteAllText(filename,textBox13.Text);
                }
                catch (Exception ex)
            { 
                MessageBox.Show("wrong name/path", "Решение СЛАУ методом простых итераций", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            AboutBox1 about = new AboutBox1();
            about.Show();
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of;
            of = new OpenFileDialog();      
            of.InitialDirectory = "c:\\";
            of.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";       
            if (of.ShowDialog() == DialogResult.OK)
            {
                    var fileStream = of.OpenFile();       
                    StreamReader reader = new StreamReader(fileStream);
                    textBox13.Text = reader.ReadToEnd();
            }
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("вы уверены ?", "Решение СЛАУ методом простых итераций", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes) 
            {
                Application.Exit();
            }
            
            
        }
    }
    }


