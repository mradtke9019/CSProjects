using MathLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace Graphics.Objects
{
    public class Circle: IDrawable
    {
        public Vector2D Position { get; set; }
        public int Radius { get; set; }
        public Vector2D Velocity { get; set; }
        public Circle(int radius, Vector2D velocity, Vector2D position)
        {
            Radius = radius;
            Velocity = velocity;
            Position = position;
        }
        public void Update(Matrix Transform = null)
        {
            Velocity.Y += 1;
            if(Position.Y + Radius >= 1200)
            {
                Velocity.Y = Velocity.Y * 0.80 * -1.0;
            }
            Position += Velocity;
        }

        public void Display(System.Drawing.Graphics e)
        {
            e.FillEllipse(Brushes.Red, Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Radius, Radius);
        }
    }
}
