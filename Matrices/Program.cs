using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;

namespace Matrices
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //Matrix<int> a = new Matrix<int>(2, 2);
            //Matrix<int> b = new Matrix<int>(2, 2);
            //Matrix<int> initialized = new Matrix<int>(1,2);
            //for (int i = 0; i < a.m; i++)
            //    for (int j = 0; j < 2; j++) {
            //        a.Set(i, j, i + j);
            //        b.Set(i, j, i + j);
            //    }

            //Matrix<int> I = Matrix<int>.IdentityMatrix(2);
            //var aResult = a * I;
            //var aTa = a * a;
            //Console.WriteLine("A ");
            //Console.WriteLine(a.ToString());
            //Console.WriteLine("A * Identity");
            //Console.WriteLine(aResult.ToString());
            //Console.WriteLine("A * A");
            //Console.WriteLine(aTa.ToString());
            //Vector2 v = new Vector2();
            //Matrix m = new Matrix();
            //Point p = new Point(5, 5);
            //Matrix<int> c = a - b;
            //Console.WriteLine(c.ToString());
            //Console.ReadKey();
        }

        static void Test()
        {
            int m = 2, n = 3, p = 3, q = 3, i, j;
            int[,] a = { { 1, 4, 2 }, { 2, 5, 1 } };
            int[,] b = { { 3, 4, 2 }, { 3, 5, 7 }, { 1, 2, 1 } };
            Console.WriteLine("Matrix a:");
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < n; j++)
                {
                    Console.Write(a[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Matrix b:");
            for (i = 0; i < p; i++)
            {
                for (j = 0; j < q; j++)
                {
                    Console.Write(b[i, j] + " ");
                }
                Console.WriteLine();
            }
            if (n != p)
            {
                Console.WriteLine("Matrix multiplication not possible");
            }
            else
            {
                int[,] c = new int[m, q];
                for (i = 0; i < m; i++)
                {
                    for (j = 0; j < q; j++)
                    {
                        c[i, j] = 0;
                        for (int k = 0; k < n; k++)
                        {
                            c[i, j] += a[i, k] * b[k, j];
                        }
                    }
                }
                Console.WriteLine("The product of the two matrices is :");
                for (i = 0; i < m; i++)
                {
                    for (j = 0; j < n; j++)
                    {
                        Console.Write(c[i, j] + "\t");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
