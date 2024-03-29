﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib
{

    public class Vector3D : IVector
    {
        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        #region Operators
        public static Vector3D operator +(Vector3D v, Vector3D other)
        {
            return new Vector3D(v.X + other.X, v.Y + other.Y, v.Z + other.Z);
        }
        public static Vector3D operator -(Vector3D v, Vector3D other)
        {
            return new Vector3D(v.X - other.X, v.Y - other.Y, v.Z - other.Z);
        }
        public static bool operator ==(Vector3D v, Vector3D other)
        {
            return v.X == other.X && v.Y == other.Y && v.Z == other.Z;
        }
        public static bool operator !=(Vector3D v, Vector3D other)
        {
            return v.X != other.X || v.Y != other.Y || v.Z != other.Z;
        }

        public static Vector3D operator *(double scale, Vector3D v)
        {
            return new Vector3D(scale * v.X, scale * v.Y, scale * v.Z);
        }

        public static Vector3D operator /(Vector3D v, double scale)
        {
            return new Vector3D(v.X / scale, v.Y / scale, v.Z / scale);
        }

        public override bool Equals(object? otherObj)
        {
            if (otherObj == null)
                return false;
            if (otherObj is Vector3D)
            {
                Vector3D other = otherObj as Vector3D;
                return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        public Vector2D AsVector2D()
        {
            return new Vector2D(this.X, this.Y);
        }

        public double Dot(IVector? otherVector)
        {
            Vector3D v = otherVector as Vector3D;
            return (this.X * v.X) + (this.Y * v.Y) + (this.Z * v.Z);
        }

        public Vector3D Normalized()
        {
            double length = Length();
            return new Vector3D(this.X / length, this.Y / length, this.Z / length);
        }

        public double Length()
        {
            return Math.Sqrt(LengthSquared());
        }
        public double LengthSquared()
        {
            return (this.X * this.X) + (this.Y * this.Y) + (this.Z * this.Z);
        }

        public IVector Rotate(double theta, bool inDegrees)
        {
            return Matrix.RotationMatrix<Vector3D>(theta, inDegrees) * this;
        }

        /// <summary>
        /// Returns a vector who is perpendicular to both u and v.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3D CrossProduct(Vector3D u, Vector3D v)
        {
            double x = u.Y * v.Z - u.Z * v.Y;
            double y = u.Z * v.X - u.X * v.Z;
            double z = u.X * v.Y - u.Y * v.X;
            return new Vector3D(x, y, z);
        }
    }
}
