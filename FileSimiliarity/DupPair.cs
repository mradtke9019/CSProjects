using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FileSimiliarity
{
    class DupPair
    {
        public string A;
        public string B;
        public double similarity;

        public int CompareTo([AllowNull] DupPair other)
        {
            if ((this.A.CompareTo(other.A) == 0 && this.B.CompareTo(other.B) == 0) || (this.A.CompareTo(other.B) == 0 && this.B.CompareTo(other.A) == 0))
                return 0;
            else
                return this.A.CompareTo(other.A);
        }

        public override string ToString()
        {
            return similarity.ToString("##.##") + "% " + A + ":" + B;
        }
    }
}
