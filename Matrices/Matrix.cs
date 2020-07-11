using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Vectors;
using System.Collections;

namespace Matrices
{
    public class Matrix : IComparable<Matrix>, IEnumerable
    {

        public readonly int m;
        public readonly int n;
        private float[,] matrix;
        public Matrix(int m, int n) 
        {
            matrix = new float[m,n];
            this.m = m;
            this.n = n;
        }
        public Matrix(float[,] matrix)
        {
            this.matrix = matrix;
            this.m = matrix.GetLength(0);
            this.n = matrix.GetLength(1);
        }


        public float ElementAt(int m, int n)
        {
            return matrix[m,n];
        }

        public void Set(int m, int n, float value)
        {
            matrix[m,n] = value;
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.m != b.m || a.n != b.n)
                throw new ArithmeticException();
            Matrix result = new Matrix(a.m, a.n);
            for (int i = 0; i < a.m; i++)
            {
                for (int j = 0; j < a.n; j++)
                {
                    dynamic aVal = a.matrix[i,j];
                    dynamic bVal = b.matrix[i,j];
                    result.matrix[i,j] = aVal + bVal;
                }
            }
            return result;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.m != b.m || a.n != b.n)
                throw new ArithmeticException();
            Matrix result = new Matrix(a.m, a.n);
            for (int i = 0; i < a.m; i++)
            {
                for (int j = 0; j < a.n; j++)
                {
                    dynamic aVal = a.matrix[i,j];
                    dynamic bVal = b.matrix[i,j];
                    result.matrix[i,j] = aVal - bVal;
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.n != b.m)
                throw new ArithmeticException();
            Matrix result = new Matrix(a.m, b.n);
            for (int i = 0; i < a.m; i++)
            {
                for (int j = 0; j < b.n; j++)
                {
                    result.matrix[i, j] = new float();
                    for (int k = 0; k < a.n; k++)
                    {
                        dynamic aVal = a.matrix[i,k];
                        dynamic bVal = b.matrix[k, j];
                        result.matrix[i, j] += aVal * bVal;
                    }
                }
            }
            return result;
        }

        public static Matrix operator *(float scale, Matrix a)
        {
            Matrix result = new Matrix(a.m,a.n);
            for (int i = 0; i < a.m; i++)
            {
                for (int j = 0; j < a.n; j++)
                {
                    result.matrix[i, j] = a.matrix[i,j] * scale;
                }
            }
            return result;
        }

        public static bool operator ==(Matrix a, Matrix b)
        {
            if (a.m != b.m || a.n != b.n)
                return false;
            for (int i = 0; i < a.m; i++)
            {
                for (int j = 0; j < a.n; j++)
                {
                    if (a.matrix[i,j].CompareTo(b.matrix[i,j]) != 0)
                        return false;
                }
            }
            return true;
        }

        public static bool operator !=(Matrix a, Matrix b)
        {
            if (a.m == b.m && a.n == b.n)
                return true;
            for (int i = 0; i < a.m; i++)
            {
                for (int j = 0; j < a.n; j++)
                {
                    if (a.matrix[i,j].CompareTo(b.matrix[i,j]) == 0)
                        return false;
                }
            }
            return true;
        }

        public static Vector2D operator *(Matrix a, Vector2D v)
        {
            Matrix vMat = ToMatrix(v);
            var result = a * vMat;
            var vResult = new Vector2D(result.ElementAt(0, 0), result.ElementAt(1, 0));
            return vResult;
        }

        public static Matrix IdentityMatrix(int size)
        {
            Matrix I = new Matrix(size, size);
            dynamic one = 1;
            dynamic zero = 0;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (i == j)
                        I.matrix[i,j] = one;
                    else
                        I.matrix[i,j] = zero;

            return I;
        }

        public static Vector2D Scale(float xScale, float yScale, Vector2D v)
        {
            return new Vector2D(v.X * xScale, v.Y * yScale);
        }

        public Vector2D Transform(Vector2D v)
        {
            if (this.m != 2 && this.n != 2)
                throw new ArithmeticException();
            //for
            return this * v;
            throw new NotImplementedException();
            //return new Vector2D();
        }

        public static Matrix ToMatrix(Vector2D v)
        {
            Matrix vMat = new Matrix(2, 1);
            vMat.Set(0, 0, v.X);
            vMat.Set(1, 0, v.Y);
            return vMat;
        }

        public bool Equals(Matrix other)
        {
            return this == other;
        }

        override
        public string ToString()
        {
            string temp = "";
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                for(int j = 0; j < matrix.GetLength(1); j++)
                {
                    temp += matrix[i,j] + " ";
                }

                temp += "\n";
            }
            return temp;
        }

        public int CompareTo(Matrix other)
        {
            if (this.m != other.m || this.n != other.n)
                return -1;
            for (int i = 0; i < this.m; i++)
                for (int j = 0; j < this.n; j++)
                    if (this.matrix[i,j].CompareTo(other.matrix[i,j]) != 0)
                        return this.matrix[i,j].CompareTo(other.matrix[i,j]);
            return 0;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
