namespace MathLib
{
    public class Matrix : IMatrix
    {

        public readonly int m;
        public readonly int n;
        private double[,] matrix;
        public Matrix(int m, int n)
        {
            if (m <= 0 || n <= 0)
                throw new InvalidOperationException();
            matrix = new double[m, n];
            this.m = m;
            this.n = n;
        }
        public Matrix(double[,] matrix)
        {
            this.matrix = matrix;
            this.m = matrix.GetLength(0);
            this.n = matrix.GetLength(1);
        }


        public double ElementAt(int m, int n)
        {
            return matrix[m, n];
        }

        public void Set(int m, int n, double value)
        {
            matrix[m, n] = value;
        }

        public void Set(int m, int n, float value)
        {
            matrix[m, n] = (double)value;
        }

        #region Operations
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.m != b.m || a.n != b.n)
                throw new ArithmeticException();
            Matrix result = new Matrix(a.m, a.n);
            for (int i = 0; i < a.m; i++)
            {
                for (int j = 0; j < a.n; j++)
                {
                    dynamic aVal = a.matrix[i, j];
                    dynamic bVal = b.matrix[i, j];
                    result.matrix[i, j] = aVal + bVal;
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
                    dynamic aVal = a.matrix[i, j];
                    dynamic bVal = b.matrix[i, j];
                    result.matrix[i, j] = aVal - bVal;
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
                    result.matrix[i, j] = new double();
                    for (int k = 0; k < a.n; k++)
                    {
                        dynamic aVal = a.matrix[i, k];
                        dynamic bVal = b.matrix[k, j];
                        result.matrix[i, j] += aVal * bVal;
                    }
                }
            }
            return result;
        }

        public static Matrix operator *(double scale, Matrix a)
        {
            Matrix result = new Matrix(a.m, a.n);
            for (int i = 0; i < a.m; i++)
            {
                for (int j = 0; j < a.n; j++)
                {
                    result.matrix[i, j] = a.matrix[i, j] * scale;
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
                    if (a.matrix[i, j].CompareTo(b.matrix[i, j]) != 0)
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
                    if (a.matrix[i, j].CompareTo(b.matrix[i, j]) == 0)
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

        public static Vector3D operator *(Matrix a, Vector3D v)
        {
            Matrix vMat = ToMatrix(v);
            var result = a * vMat;
            var vResult = new Vector3D(result.ElementAt(0, 0), result.ElementAt(1, 0), result.ElementAt(2, 0));
            return vResult;
        }
        #endregion

        public static Matrix IdentityMatrix(int size)
        {
            Matrix I = new Matrix(size, size);
            dynamic one = 1;
            dynamic zero = 0;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (i == j)
                        I.matrix[i, j] = one;
                    else
                        I.matrix[i, j] = zero;

            return I;
        }

        public static Matrix RotationMatrix<T>(double theta, bool inDegrees = true)
        {
            int size = -1;
            if (typeof(T) == typeof(Vector2D))
                size = 2;
            else
            {
                throw new NotImplementedException();
            }
            if (inDegrees)
                theta = Math.PI * theta / 180;
            Matrix r = new Matrix(size, size);
            if (size == 2)
            {
                r.Set(0, 0, Math.Cos(theta));
                r.Set(0, 1, -Math.Sin(theta));
                r.Set(1, 0, Math.Sin(theta));
                r.Set(1, 1, Math.Cos(theta));
            }
            if (size == 3)
            {
                throw new NotImplementedException();
            }
            return r;
        }
        public static Matrix TranslationMatrix<T>(double x, double y, double z)
        {
            if (typeof(T) == typeof(Vector2D))
            {
                Matrix translation = IdentityMatrix(3);
                translation.Set(0, 2, x);
                translation.Set(1, 2, y);
                return translation;
            }

            throw new InvalidOperationException();
        }

        public static Vector2D Scale(double xScale, double yScale, Vector2D v)
        {
            return new Vector2D(v.X * xScale, v.Y * yScale);
        }

        public Vector2D Transform(Vector2D v)
        {
            if (this.m != 2 && this.n != 2)
                throw new ArithmeticException();
            return this * v;
        }

        public static Matrix ToMatrix(IVector vector)
        {
            if (vector.GetType() == typeof(Vector2D))
            {
                Vector2D v = (Vector2D)vector;
                Matrix vMat = new Matrix(2, 1);
                vMat.Set(0, 0, v.X);
                vMat.Set(1, 0, v.Y);
                return vMat;
            }
            if (vector.GetType() == typeof(Vector3D))
            {
                Vector3D v = (Vector3D)vector;
                Matrix vMat = new Matrix(3, 1);
                vMat.Set(0, 0, v.X);
                vMat.Set(1, 0, v.Y);
                vMat.Set(2, 0, v.Z);
                return vMat;
            }
            throw new InvalidOperationException();
        }

        public bool Equals(Matrix other)
        {
            return this == other;
        }

        override
        public bool Equals(object other)
        {
            if (other.GetType() == this.GetType())
                return (Matrix)other == this;
            return false;
        }

        override
        public string ToString()
        {
            string temp = "";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    temp += matrix[i, j] + " ";
                }

                temp += "\n";
            }
            return temp;
        }

        public int CompareTo(IMatrix other)
        {
            Matrix otherMatrix = other as Matrix;
            if (otherMatrix == null)
                return 1;
            if (this.m != otherMatrix.m || this.n != otherMatrix.n)
                return -1;
            for (int i = 0; i < this.m; i++)
                for (int j = 0; j < this.n; j++)
                    if (this.matrix[i, j].CompareTo(otherMatrix.matrix[i, j]) != 0)
                        return this.matrix[i, j].CompareTo(otherMatrix.matrix[i, j]);
            return 0;
        }

        public Matrix Transpose()
        {
            Matrix transpose = new Matrix(this.n, this.m);
            for (int i = 0; i < this.m; i++)
            {
                for (int j = 0; j < this.n; j++)
                {
                    transpose.Set(j, i, this.matrix[i, j]);
                }
            }
            return transpose;
        }

        public double Trace()
        {
            if (this.m != this.n)
                throw new InvalidOperationException($"Operation not defined for non square matrices. Matrix dimensions is {this.m} by {this.n}");
            double sum = 0;
            for (int i = 0; i < this.m; i++)
                sum += this.matrix[i, i];
            return sum;
        }

        public double Determinant()
        {
            if (this.m != this.n)
                throw new InvalidOperationException($"Determinant does not exist for non square matrices. Matrix dimensions is {this.m} by {this.n}");

            List<List<int>> permutations = GetPermutations(this.n, true);
            double determinant = 0;
            foreach(var permutation in permutations)
            {
                double signedElementaryProduct = 1;
                int i = 0;
                int inversions = CountInversions(permutation);
                foreach (var j in permutation)
                {
                    signedElementaryProduct *=  this.matrix[i, j];
                    i++;
                }

                // If this is an odd inversion, we make this signed elementary product negative
                signedElementaryProduct *= inversions % 2 == 0 ? 1 : -1;
                determinant += signedElementaryProduct;
            }

            return determinant;
        }

        /// <summary>
        /// Gets all list permutations for a list of size n
        /// </summary>
        /// <param name="n"></param>
        /// <param name="zeroBased">Determine if we should use 0 based indexing for the list to be returned</param>
        /// <returns></returns>
        public static List<List<int>> GetPermutations(int n, bool zeroBased = false)
        {
            List<int> combination = new List<int>();
            if(zeroBased)
            {
                for (int i = 0; i < n; i++)
                {
                    combination.Add(i);
                }
            }
            else
            {
                for (int i = 1; i <= n; i++)
                {
                    combination.Add(i);
                }
            }
            return PermutationHelper(combination);
        }

        private static List<List<int>> PermutationHelper(List<int> list)
        {
            List<List<int>> result = new List<List<int>>();
            if(list.Count == 1)
            {
                result.Add(list);
                return result;
            }
            if(list.Count == 2)
            {
                result.Add(new List<int>() { list[0], list[1] });
                result.Add(new List<int>() { list[1], list[0] });
                return result;
            }

            foreach(var n in list)
            {
                var subPermutations = PermutationHelper(list.Except(new List<int>() { n }).ToList());
                foreach (var subPermutation in subPermutations)
                {
                    var currResult = new List<int>();
                    currResult.Add(n);
                    currResult.AddRange(subPermutation);
                    result.Add(currResult);
                }
            }
            return result;
        }

        /// <summary>
        /// Take a list of numbers and counts the number of occurences where a larger number appears before a smaller number
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int CountInversions(List<int> numbers)
        {
            int result = 0;

            foreach(var n in numbers)
            {
                foreach(var m in numbers.Where(x => numbers.IndexOf(x) > numbers.IndexOf(n)))
                {
                    if (n > m)
                        result++;
                }
            }

            return result;
        }

        public bool HasInverse()
        {
            return Determinant() != 0;
        }

        public Matrix Inverse()
        {
            if (this.m != this.n)
                throw new InvalidOperationException($"Inverse does not exist for non square matrices. Matrix dimensions is {this.m} by {this.n}");
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
