using System;
using System.Collections.Generic;
using System.Text;

namespace MathLib
{
    public interface IVector
    {
        public double Length();
        public double LengthSquared();
        public double Dot(IVector? otherVector);
        public IVector Rotate(double theta, bool inDegrees = true);

        public double AngleBetweenOtherVector(IVector? otherVector)
        {
            if(otherVector == null)
                throw new ArgumentNullException(nameof(otherVector));
            return Math.Acos(this.Dot(otherVector) / (this.Length() * otherVector.Length()));
        }

        public double AngleBetweenVectors(IVector v1, IVector v2)
        {
            double dotProduct = v1.Dot(v2);
            double combinedLengths = v1.Length() * v2.Length();
            return Math.Acos(dotProduct / combinedLengths);
        }
    }
}
