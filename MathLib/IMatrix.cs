using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib
{
    public interface IMatrix: IComparable<IMatrix>
    {
        /// <summary>
        /// Returns the transpose of the current matrix. Rows become columns and columns become rows.
        /// </summary>
        /// <returns></returns>
        public Matrix Transpose();

        /// <summary>
        /// Sums the diagonal of a square matrix.
        /// </summary>
        /// <returns></returns>
        public double Trace();

        public double Determinant();

        public Matrix Inverse();

        public bool HasInverse();
    }
}
