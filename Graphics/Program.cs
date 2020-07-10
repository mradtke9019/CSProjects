using System;
using System.Drawing;
using Matrices;

namespace Drawings
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Pen blackPen = new Pen(Color.Black,3 );
            // Create points that define line.
            PointF point1 = new PointF(100.0F, 100.0F);
            PointF point2 = new PointF(500.0F, 100.0F);
            //Control.CreateGraphics
            //g.DrawLine(blackPen, point1, point2);
            Console.ReadKey();
        }
    }
}
