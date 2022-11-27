using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace lab1
{
    public partial class Form1 : Form
    {
        public Graphics graph;
        StreamWriter write = new StreamWriter("punctuletze.txt");

        public Form1()
        {
            InitializeComponent();
        }
        double Gauss(double x, double m, double dispersia)
        {
            double var1 = Math.Pow((m - x), 2.0);
            double var2 = var1 / (2.0 * Math.Pow(dispersia, 2));
            double var3 = Math.Exp(-var2);

            return var3;
        }

        public void DrawAxis(Point origin)
        {
            graph = panel1.CreateGraphics();
            Pen myPen = new Pen(Color.Black);

            Point p1 = new Point(origin.X - 300, origin.Y);
            Point p2 = new Point(origin.X + 300, origin.Y);
            Point p3 = new Point(origin.X, origin.Y-300);
            Point p4 = new Point(origin.X, origin.Y+300);

            graph.TranslateTransform(300, 300);
            graph.DrawLine(myPen, p1, p2);
            graph.DrawLine(myPen, p3, p4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string file = @"D:\Laptop Amalia\Facultate\anul IV\invatare automata\lab1\lab1\bin\Debug\punctuletze.txt";
            Process.Start(file);
        }

        public void DesenareZona(Pen myPen,int mx, int my, int sigmax, int sigmay)
        {
            graph = panel1.CreateGraphics();

            Point stangaSus = new Point(300 + (mx - sigmax), 300-(my + sigmay));
            Point stangaJos = new Point(300+(mx - sigmax), 300-(my - sigmay));
            Point dreaptaSus = new Point(300+(mx + sigmax), 300-(my + sigmay));
            Point dreaptaJos = new Point(300+(mx + sigmax), 300-(my - sigmay));

            graph.DrawLine(myPen, stangaSus, stangaJos);
            graph.DrawLine(myPen, stangaJos, dreaptaJos);
            graph.DrawLine(myPen,dreaptaJos, dreaptaSus);
            graph.DrawLine(myPen, dreaptaSus, stangaSus);
        }

        public void GenerarePuncte(Pen pen,int nrPuncte, double mx, double sigmax, double my, double sigmay) 
        {
            Random rand = new Random();
            int coordX, coordY;
            int contor = 0;

            while (contor < nrPuncte)
            {
            Start1:
                int x = rand.Next(-300, 300);
                double probabilitatex = Gauss(x, mx, sigmax);
                double pax = rand.Next(0, 1001) / 1000.0;

                if (probabilitatex >= pax)
                {
                    coordX = x;
                }
                else
                {
                    goto Start1;
                }
            Start2:
                int y = rand.Next(-300, 300);
                double probabilitatey = Gauss(y, my, sigmay);
                double pay = rand.Next(0, 1001) / 1000.0;

                if (probabilitatey >= pay)
                {
                    coordY = y;
                }
                else
                {
                    goto Start2;
                }
                contor++;

                graph.DrawRectangle(pen, 300 + coordX, 300 - coordY, 1, 1);
                write.WriteLine("coord_X: " + coordX + ", coord_Y: " + coordY );

            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen red = new Pen(Color.Red);
            Pen yellow = new Pen(Color.Yellow);
            Pen blue = new Pen(Color.Blue);

            Point origin = new Point(0, 0);
            DrawAxis(origin);


            DesenareZona(red, 180, 220, 20, 20);
            GenerarePuncte(red, 1500, 180, 10, 220, 10);

            DesenareZona(blue,-100, 110, 30, 20);
            GenerarePuncte(blue, 1500, -100, 15, 110, 10);

            DesenareZona(yellow,210, -150, 10, 40);
            GenerarePuncte(yellow,500, 210, 5, -150, 20);

        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = String.Format("{0},{1}", e.Location.X, e.Location.Y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pen red = new Pen(Color.Red);

            textBox1.Location = new Point(747, 307);
            textBox1.Visible = true;

            //int x = int.Parse(textBox1.Text);
        }
    }
}
