using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triangle
{
    public partial class Main : Form
    {
        private int pictureBoxSize = 10;
        private int counter = 0;
        public int count = 0;
        //Anzahl der Ebenen
        public int Stages = 2;
        //Anzahl der Ebenen
        public int inherit;
        private Point[] points = new Point[3];
        public List<triangle> tris = new List<triangle>();
        public List<triangle> newtris = new List<triangle>();
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Size = new Size(500, 500);
        }

        private void Main_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = PointToClient(Cursor.Position);
            points[counter] = p;
            PictureBox pb = new PictureBox();
            pb.Size = new Size(pictureBoxSize, pictureBoxSize);
            pb.Location = new Point(p.X - pictureBoxSize/2, p.Y - pictureBoxSize/2);
            pb.BackColor = Color.Black;
            this.Controls.Add(pb);
            counter++;
            if(counter == 3)
            {
                createTriangle(Stages,points[0], points[1], points[2]);
                counter = 0;
            }
        }
        public void createTriangle(int inheritances, Point a, Point b, Point c)
        {
            triangle t = new triangle(a,b,c);
            tris.Add(t);
            drawLine(t.a, t.b, t.c);
            inherit = inheritances;
            main2();
        }
        public void split(triangle t)
        {
            triangle t1 = new triangle(midLine(t.a, t.b), midLine(t.a, t.c), midLine(t.c, t.b));
            triangle t2 = new triangle(t.a, midLine(t.a, t.c), midLine(t.a, t.b));
            triangle t3 = new triangle(t.b, midLine(t.b, t.c), midLine(t.a, t.b));
            triangle t4 = new triangle(t.c, midLine(t.a, t.c), midLine(t.c, t.b));

            // triangle t2 = new triangle(midLine(midLine(t.a, t.b), t.a), midLine(midLine(t.a, t.c), t.a), midLine(midLine(t.a, t.b), midLine(t.a, t.c)));
            //triangle t3 = new triangle(midLine(midLine(t.a, t.b), t.b), midLine(midLine(t.b, t.c), t.b), midLine(midLine(t.a, t.b), midLine(t.b, t.c)));
            //triangle t4 = new triangle(midLine(midLine(t.c, t.b), t.c), midLine(midLine(t.a, t.c), t.c), midLine(midLine(t.c, t.b), midLine(t.a, t.c)));
            newtris.Add(t1);
            newtris.Add(t2);
            newtris.Add(t3);
            newtris.Add(t4);
            
            drawLine(t1.a, t1.b, t1.c);
          //  drawLine(t2.a, t2.b, t2.c);
            //drawLine(t3.a, t3.b, t3.c);
            //drawLine(t4.a, t4.b, t4.c);
            

            if (count < inherit)
            { 
                
            }
        }

        
        public void main()
        {
            foreach(triangle tri in tris)
            {
                split(tri);
            }
            tris.Clear();
            foreach(triangle tri in newtris)
            {
                tris.Add(tri);
                drawLine(midLine(tri.a, tri.b), midLine(tri.a, tri.c), midLine(tri.c, tri.b));
            }
            newtris.Clear();
        }
        public void main2()
        {
            if(count < inherit)
            {
                foreach (triangle tri in tris)
                {
                    split(tri);
                }
                tris.Clear();
                foreach(triangle tri in newtris)
                {
                    tris.Add(tri);
                }
                count++;
                main2();
            }


        }
        public void splitTriangle(int inheritances, Point a, Point b, Point c)
        {


            createTriangle(inheritances, midLine(a, b), midLine(a, c), midLine(c, b));
            createTriangle(inheritances, midLine(midLine(a, b), a), midLine(midLine(a, c), a), midLine(midLine(a, b), midLine(a, c)));
            createTriangle(inheritances, midLine(midLine(a, b), b), midLine(midLine(b, c), b), midLine(midLine(a, b), midLine(b, c)));
            createTriangle(inheritances, midLine(midLine(c, b), c), midLine(midLine(a, c), c), midLine(midLine(c, b), midLine(a, c)));


        }

        public Point midLine(Point a, Point b)
        {
            Point mid;
            mid = new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
            return mid;
        }
        public void drawLine(Point a, Point b, Point c)
        {
            Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.Black);
            g.DrawLine(p, a, b);
            g.DrawLine(p, c, b);
            g.DrawLine(p, a, c);
            
        }

        
    }
    public class triangle
    {
        public Point a,b,c;

        public triangle(Point p1, Point p2, Point p3)
        {
            a = p1;
            b = p2;
            c = p3;
        }


    }
}
