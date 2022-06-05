using System;
using System.Collections.Generic;
using System.Text;

namespace Vectors
{
    public interface IVector
    {
        public double Length();
        public double LengthSquared();
        public double DotProduct(IVector v1, IVector v2);
        public double Dot(IVector otherVector);
        public IVector Normalized();

        public double AngleBetweenVectors(IVector v1, IVector v2)
        {
            double dotProduct = DotProduct(v1, v2);
            double combinedLengths = v1.Length() * v2.Length();
            return Math.Acos(dotProduct / combinedLengths);
        }
    }
}
