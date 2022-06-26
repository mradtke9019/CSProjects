using MathLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void MatrixBasicTest1()
        {
            double[,] A = { { 0, 1 }, { 1, 2 } };
            double[,] C = { { 0, 1, 2, 2 }, { 1, 2, 2, 2 } };
            double[,] Scale = { { 0, 2 }, { 2, 4 } };
            double[,] AD = { { 1, 2, 2, 2 }, { 2, 5, 6, 6 } };

            Matrix a = new Matrix(A);
            Matrix b = new Matrix(A);
            Matrix c = new Matrix(C);
            Matrix ad = new Matrix(AD);

            Matrix s = new Matrix(Scale);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsTrue(a * Matrix.IdentityMatrix(2) == a);
            Assert.IsTrue((2 * a) == s);
            Assert.IsTrue(a * c == ad);
        }

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

        [TestMethod]
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

        [TestMethod]
        public void MatrixPermutationTest1()
        {
            List<List<int>> twoPermutations = new List<List<int>>()
            {
                new List<int>() {1,2},
                new List<int>() {2,1}
            };

            List<List<int>> threePermutations = new List<List<int>>()
            {
                new List<int>() {1,2,3},
                new List<int>() {1,3,2},

                new List<int>() {2,1,3},
                new List<int>() {2,3,1},

                new List<int>() {3,1,2},
                new List<int>() {3,2,1},
            };
            List<List<int>> fourPermutations = new List<List<int>>()
            {
                new List<int>() {1,2,3,4},
                new List<int>() {1,2,4,3},
                new List<int>() {1,4,2,3},
                new List<int>() {4,1,2,3},

                new List<int>() {1,3,2,4},
                new List<int>() {1,3,4,2},
                new List<int>() {1,4,3,2},
                new List<int>() {4,1,3,2},

                new List<int>() {2,1,3,4},
                new List<int>() {2,1,4,3},
                new List<int>() {2,4,1,3},
                new List<int>() {4,2,1,3},

                new List<int>() {2,3,1,4},
                new List<int>() {2,3,4,1},
                new List<int>() {2,4,3,1},
                new List<int>() {4,2,3,1},

                new List<int>() {3,1,2,4},
                new List<int>() {3,1,4,2},
                new List<int>() {3,4,1,2},
                new List<int>() {4,3,1,2},

                new List<int>() {3,2,1,4},
                new List<int>() {3,2,4,1},
                new List<int>() {3,4,2,1},
                new List<int>() {4,3,2,1},
            };

            var result2 = Matrix.GetPermutations(2);
            foreach(var test in twoPermutations)
            {
                Assert.IsTrue(result2.Any(permutation =>
                {
                    bool isValid = true;
                    for(int i = 0; i < test.Count; i++)
                    {
                        if (test.ElementAt(i) != permutation.ElementAt(i))
                        {
                            isValid = false;
                            break;
                        }
                    }
                    return isValid;
                }));
            }
            var result3 = Matrix.GetPermutations(3);
            foreach (var test in threePermutations)
            {
                Assert.IsTrue(result3.Any(permutation =>
                {
                    bool isValid = true;
                    for (int i = 0; i < test.Count; i++)
                    {
                        if (test.ElementAt(i) != permutation.ElementAt(i))
                        {
                            isValid = false;
                            break;
                        }
                    }
                    return isValid;
                }));
            }

            var result4 = Matrix.GetPermutations(4);
            foreach (var test in fourPermutations)
            {
                Assert.IsTrue(result4.Any(permutation =>
                {
                    bool isValid = true;
                    for (int i = 0; i < test.Count; i++)
                    {
                        if (test.ElementAt(i) != permutation.ElementAt(i))
                        {
                            isValid = false;
                            break;
                        }
                    }
                    return isValid;
                }));
            }
        }

        [TestMethod]
        public void MatrixInversionTest1()
        {
            List<int> list1 = new List<int>() { 1, 2, 3 };
            int result1 = 0;
            Assert.AreEqual(result1, Matrix.CountInversions(list1));

            List<int> list2 = new List<int>() { 1, 3, 2 };
            int result2 = 1;
            Assert.AreEqual(result2, Matrix.CountInversions(list2));


            List<int> list3 = new List<int>() { 3, 2, 1 };
            int result3 = 3;
            Assert.AreEqual(result3, Matrix.CountInversions(list3));
        }

        [TestMethod]
        public void MatrixDeterminantTest1()
        {
            Matrix a = new Matrix(2, 2);
            a.Set(0, 0, 3);
            a.Set(0, 1, 2);
            a.Set(1, 0, -9);
            a.Set(1, 1, 5);
            Debug.WriteLine(a.ToString());
            double determinantA = 33;
            Assert.AreEqual(a.Determinant(), determinantA);

            Matrix b = new Matrix(3, 3);
            b.Set(0, 0, 3);
            b.Set(0,1,5);
            b.Set(0,2,4);
            b.Set(1,0,-2);
            b.Set(1,1,-1);
            b.Set(1,2,8);
            b.Set(2,0,-11);
            b.Set(2,1,1);
            b.Set(2,2,7);
            Debug.WriteLine(b.ToString());
            double determinantB = -467;
            Assert.AreEqual(b.Determinant(), determinantB);

            Matrix c = new Matrix(3, 2);
            Assert.ThrowsException<InvalidOperationException>(new Action(() => c.Determinant()));
        }
    }
}