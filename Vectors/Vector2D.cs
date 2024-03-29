﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Vectors
{
    public class Vector2D
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
            return new Vector2D(v.X+other.X, v.Y +other.Y);
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

        public double DotProduct(Vector2D v1, Vector2D v2)
        {

            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public double Dot(IVector otherVector)
        {
            Vector2D v = otherVector as Vector2D;
            return this.X * v.X + this.Y * v.Y;
        }


        public Vector2D Normalized()
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

        /*public static double AngleBetweenVectors(Vector2D v1, Vector2D v2)
        {
            double dotProduct = DotProduct(v1, v2);
            double combinedLengths = v1.Length() * v2.Length();
            return Math.Acos(dotProduct / combinedLengths);
        }*/

        //public void Translate(float x, float y)
        //{
        //    X += x;
        //    Y += y;
        //}
        //public void TranslateX(float x)
        //{
        //    X += x;
        //}
        //public void TranslateY(float y)
        //{
        //    Y += y;
        //}
    }
}
