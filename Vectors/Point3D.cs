using System;
using System.Collections.Generic;
using System.Text;

namespace Vectors
{
    public class Point3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public static Point3D operator +(Point3D point, Vector3D v)
        {
            return new Point3D(v.X + point.X, v.Y + point.Y, v.Z + point.Z);
        }
        public static Point3D operator -(Point3D point, Vector3D v)
        {
            return new Point3D(point.X - v.X, point.Y - v.Y, point.Z - v.Z);
        }

        public static Point3D operator -( Vector3D v, Point3D point)
        {
            return new Point3D(v.X - point.X, v.Y - point.Y, v.Z - point.Z);
        }

        public static Point3D operator +(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        public static Point3D operator -(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        public Vector3D AsVector()
        {
            return new Vector3D(X, Y, Z);
        }

        public override bool Equals(object obj)
        {
            if(obj is Point3D)
            {
                Point3D other = obj as Point3D;
                return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
            }

            return false;
        }
    }
}
