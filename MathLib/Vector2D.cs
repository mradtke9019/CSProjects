using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib
{
    public class Vector2D : IVector
    {
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double X { get; set; }
        public double Y { get; set; }

        public static Vector2D operator +(Vector2D v, Vector2D other)
        {
            return new Vector2D(v.X + other.X, v.Y + other.Y);
        }
        public static Vector2D operator -(Vector2D v, Vector2D other)
        {
            return new Vector2D(v.X - other.X, v.Y - other.Y);
        }
        public static bool operator ==(Vector2D v, Vector2D other)
        {
            return v.X == other.X && v.Y == other.Y;
        }
        public static bool operator !=(Vector2D v, Vector2D other)
        {
            return v.X != other.X || v.Y != other.Y;
        }

        public static Vector2D operator *(double scale, Vector2D v)
        {
            return new Vector2D(scale * v.X, scale * v.Y);
        }

        public static Vector2D operator /(double scale, Vector2D v)
        {
            return new Vector2D(v.X / scale, v.Y / scale);
        }

        public Vector3D AsVector3D()
        {
            return new Vector3D(this.X, this.Y, 1);
        }

        public double Dot(IVector otherVector)
        {
            Vector2D v = otherVector as Vector2D;
            return this.X * v.X + this.Y * v.Y;
        }


        public IVector Normalized()
        {
            double length = Length();// = Convert.ToSingle(Math.Sqrt((this.X * this.X) + (this.Y * this.Y) + (this.Z * this.Z)));
            return new Vector2D(this.X / length, this.Y / length);
        }

        public double Length()
        {
            return Math.Sqrt(LengthSquared());
        }
        public double LengthSquared()
        {
            return (this.X * this.X) + (this.Y * this.Y);
        }

    }
}
