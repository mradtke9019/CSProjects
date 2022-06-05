using MathLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void MatrixTransposeTest1()
        {
            int m = 3; int n = 4;
            Matrix A = new Matrix(m, n);
            
            for (int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    A.Set(i, j, i * j);
                }
            }

            Matrix B = new Matrix(n, m);
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    B.Set(i, j, i * j);
                }
            }

            Assert.IsTrue(A.Transpose() == B);
            Assert.IsTrue(B.Transpose() == A);
            Assert.IsTrue(A.Transpose().Transpose() == A);
        }

        public void MatrixTraceTest1()
        {
            int n = 4;
            Matrix A = new Matrix(n, n);
            double sum = 0;

            for (int i = 0; i < n; i++)
            {
                for(int j = 0; j <n; j++)
                {
                    double val = i * j;
                    if (i == j)
                        sum += val;
                    A.Set(i, j,val);
                }
            }

            Assert.IsTrue(A.Trace() == sum);
            Matrix B = new Matrix(n, n + 1);
            Assert.ThrowsException<InvalidOperationException>(new Action(() => B.Trace()));
        }
    }
}