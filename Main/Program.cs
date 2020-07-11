using System;
using Matrices;
using Vectors;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            VectorTests();
            //Tests();
        }

        public static void VectorTests()
        {
            Vector2D v1 = new Vector2D(1,1);
            Console.WriteLine("Scale test: " + (2 * v1 == new Vector2D(2, 2) ? "passed" : "failed"));
            Console.WriteLine("Addition test: " + (v1 + v1 == new Vector2D(2, 2) ? "passed" : "failed"));
            Console.WriteLine("Identity mult: " + (Matrix.IdentityMatrix(2) * v1 == v1 ? "passed" : "failed"));
        }

        public static void Tests()
        {
            float[,] A = { { 0, 1 }, { 1, 2 } };
            float[,] C = { { 0, 1, 2, 2 }, { 1, 2, 2, 2 } };
            float[,] Scale = { { 0, 2 }, {2, 4 } };
            Matrix a = new Matrix(A);
            Matrix b = new Matrix(A);
            Matrix c = new Matrix(C);

            Matrix s = new Matrix(Scale);
            Console.WriteLine("Equality Test: " +(a == b ? "passed" : "fail"));
            Console.WriteLine("Inequality Test: " + (a != b ? "passed" : "fail"));
            Console.WriteLine("Identity Test: " + (a * Matrix.IdentityMatrix(2) == a ? "passed" : "fail"));
            Console.WriteLine("Scale Test: " + ((2 * a) == s  ? "passed" : "fail"));
            Console.WriteLine(a * c);
        }
    }
}
