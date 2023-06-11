using MathLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics.Objects
{
    public class Line : IDrawable
    {
        Vector2D A { get; set; }
        Vector2D B { get; set; }
        Color Color { get; set; }
        public Line(Vector2D a, Vector2D b, Color color)
        {
            this.A = a;
            this.B = b;
            Color = color;
        }
        public void Update(Matrix Transform = null)
        {
            if(Transform != null)
            {
                A = Transform * A;
                B = Transform * B;
            }
        }

        public void Display(System.Drawing.Graphics e)
        {
            Pen p = new Pen(Color);
            p.Width = 20;
            e.DrawLine(p, new Point(Convert.ToInt32(A.X), Convert.ToInt32(A.Y)), new Point(Convert.ToInt32(B.X), Convert.ToInt32(B.Y)));
        }
        public Vector2D Ray()
        {
            return B - A;
        }
    }
}
