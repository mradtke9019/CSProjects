using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Matrices
{
    public class Matrix<T> : IComparable<Matrix<T>> where T : IComparable<T>, new()
    {
        public Matrix(int m, int n) 
        {
            matrix = new T[m,n];
            this.m = m;
            this.n = n;
        }
        public Matrix(T[,] matrix)
        {
            this.matrix = matrix;
            this.m = matrix.GetLength(0);
            this.n = matrix.GetLength(1);
        }

        public readonly int m;
        public readonly int n;
        private T[,] matrix;

        public T ElementAt(int m, int n)
        {
            return matrix[m,n];
        }

        public void Set(int m, int n, T value)
        {
            matrix[m,n] = value;
        }

        public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b)
        {
            if (a.m != b.m || a.n != b.n)
                throw new ArithmeticException();
            Matrix<T> result = new Matrix<T>(a.m, a.n);
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

        public static Matrix<T> operator -(Matrix<T> a, Matrix<T> b)
        {
            if (a.m != b.m || a.n != b.n)
                throw new ArithmeticException();
            Matrix<T> result = new Matrix<T>(a.m, a.n);
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

        public static Matrix<T> operator *(Matrix<T> a, Matrix<T> b)
        {
            if (a.n != b.m)
                throw new ArithmeticException();
            Matrix<T> result = new Matrix<T>(a.m, b.n);
            for (int i = 0; i < a.m; i++)
            {
                for (int j = 0; j < b.n; j++)
                {
                    result.matrix[i, j] = new T();
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

        public static bool operator ==(Matrix<T> a, Matrix<T> b)
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

        public static bool operator !=(Matrix<T> a, Matrix<T> b)
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

        public static Matrix<T> IdentityMatrix(int size)
        {
            Matrix<T> I = new Matrix<T>(size, size);
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

        public int CompareTo(Matrix<T> other)
        {
            if (this.m != other.m || this.n != other.n)
                return -1;
            for (int i = 0; i < this.m; i++)
                for (int j = 0; j < this.n; j++)
                    if (this.matrix[i,j].CompareTo(other.matrix[i,j]) != 0)
                        return this.matrix[i,j].CompareTo(other.matrix[i,j]);
            return 0;
        }
    }
}
