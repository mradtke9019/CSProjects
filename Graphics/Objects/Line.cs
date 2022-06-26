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
        public void Update()
        {
            double theta = 1;
            A = Matrix.RotationMatrix<Vector2D>(theta) * (Matrix.TranslationMatrix<Vector2D>(500, 800) * A.AsVector3D()).AsVector2D();
            B = Matrix.RotationMatrix<Vector2D>(theta) * (Matrix.TranslationMatrix<Vector2D>(500, 800) * B.AsVector3D()).AsVector2D();
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
