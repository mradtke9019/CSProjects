using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class Solution
    {
        public int MySqrt(int x)
        {
            if (x == 0)
                return 0;
            return Search(x, 1,int.MaxValue);
        }

        private int Search(int x, int start, int end)
        {
            long position = (long)start + (long)end;
            bool even = position % 2 == 0;
            position = position / (long)2;
            long curr = position * position;

            if (curr == x)
                return (int)position;

            // Base case
            if (end - start == 1)
            {
                if ((int)position < x)
                    return start;
                return end;
            }
            if (curr > x)
                return Search(x, start, even ? (int)position : (int)position + 1);
            if(curr < x)
                return Search(x, even ? (int)position : (int)position + 1, end);
            return (int)position;
        }
    }
}