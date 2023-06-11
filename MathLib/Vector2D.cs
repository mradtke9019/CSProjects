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

        #region Operators
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

        public override bool Equals(object? otherObj)
        {
            if (otherObj == null)
                return false;
            if (otherObj is Vector2D)
            {
                Vector2D? other = (Vector2D)otherObj;
                if (other == null)
                    return false;
                return this.X == other.X && this.Y == other.Y;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        public Vector3D AsVector3D()
        {
            return new Vector3D(this.X, this.Y, 1);
        }

        public double Dot(IVector? otherVector)
        {
            if(otherVector == null)
                throw new NullReferenceException();
            Vector2D v = (Vector2D)otherVector;
            if (v == null)
                throw new InvalidOperationException();

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

        public IVector Rotate(double theta, bool inDegrees = true)
        {
            return Matrix.RotationMatrix<Vector2D>(theta, inDegrees) * this;
        }

    }
}
