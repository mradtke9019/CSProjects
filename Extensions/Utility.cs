using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public static class Utility
    {
        public static int EditDistance(string a, string b, int m, int n)
        {
            if (a.Length == 0)
            {
                return b.Length;
            }

            if (b.Length == 0)
            {
                return a.Length;
            }

            var d = new int[a.Length + 1, b.Length + 1];
            for (var i = 0; i <= a.Length; i++)
            {
                d[i, 0] = i;
            }

            for (var j = 0; j <= b.Length; j++)
            {
                d[0, j] = j;
            }

            for (var i = 1; i <= a.Length; i++)
            {
                for (var j = 1; j <= b.Length; j++)
                {
                    var cost = (b[j - 1] == a[i - 1]) ? 0 : 1;
                    d[i, j] = Extensions.Min(
                         d[i - 1, j] + 1,
                         d[i, j - 1] + 1,
                         d[i - 1, j - 1] + cost
                    );
                }
            }
            return d[a.Length, b.Length];
        }

        public static bool FuzzyCompare(string a, string b, double percentSimilar)
        {
            var distance = Utility.EditDistance(a, b, a.Length, b.Length);
            var min = Extensions.Min(new int[] { a.Length, b.Length });
            var similar = min.ToDouble() / (min + distance).ToDouble();

            if (similar >= percentSimilar)
                return true;
            return false;
        }

        public static double FuzzyCompare(string a, string b)
        {
            // "Some" => "Something" => editdistance 5 => some is 44% similar to something
            var distance = Utility.EditDistance(a, b, a.Length, b.Length);
            var min = (new int[] { a.Length, b.Length }).Min();
            var similar = (min).ToDouble() / (min + distance).ToDouble();

            return similar;
        }

    }
}
