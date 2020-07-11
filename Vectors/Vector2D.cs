using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Vectors
{
    public class Vector2D
    {
        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }
        public float X { get; set; }
        public float Y { get; set; }

        public static Vector2D operator +(Vector2D v, Vector2D other)
        {
            return new Vector2D(v.X+other.X, v.Y +other.Y);
        }
        public static bool operator ==(Vector2D v, Vector2D other)
        {
            return v.X == other.X && v.Y == other.Y;
        }
        public static bool operator !=(Vector2D v, Vector2D other)
        {
            return v.X != other.X || v.Y != other.Y;
        }

        public static Vector2D operator *(float scale, Vector2D v)
        {
            return new Vector2D(scale * v.X, scale * v.Y);
        }
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
