using System;
using System.Collections.Generic;
using System.Text;

namespace Vectors
{
    public class Vector3D
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
            return new Vector3D( v.X / scale, v.Y / scale, v.Z /scale);
        }

        public override bool Equals(object otherObj)
        {
            if(otherObj is Vector3D)
            {
                Vector3D other = otherObj as Vector3D;
                return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
            }
            return false;
        }



        public static double DotProduct(Vector3D v1, Vector3D v2)
        {
            return (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z);
        }

        public Vector3D Normalized()
        {
            double length = Length();// = Convert.ToSingle(Math.Sqrt((this.X * this.X) + (this.Y * this.Y) + (this.Z * this.Z)));
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

        public static double AngleBetweenVectors(Vector3D v1, Vector3D v2)
        {
            double dotProduct = DotProduct(v1, v2);
            double combinedLengths = v1.Length() * v2.Length();
            return Math.Acos(dotProduct / combinedLengths);
        }

        public static Vector3D CrossProduct(Vector3D u, Vector3D v)
        {
            double x = u.Y * v.Z - u.Z * v.Y;
            double y = u.Z * v.X - u.X * v.Z;
            double z = u.X * v.Y - u.Y * v.X;
            return new Vector3D(x, y, z);
        }
    }
}
