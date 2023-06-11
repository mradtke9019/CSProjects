using System;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            AddTwoNumbersTest();
            var sol = new Solution();
            var square = sol.MySqrt(8);
            square = sol.MySqrt(99);
            square = sol.MySqrt(100);
        }

        public static void AddTwoNumbersTest()
        {
            //901
            //875
            Solution s = new Solution();
            Solution.ListNode listNode = new Solution.ListNode(1, new Solution.ListNode(0, new Solution.ListNode(9)));
            Solution.ListNode listNode2 = new Solution.ListNode(5, new Solution.ListNode(7, new Solution.ListNode(8)));
            s.AddTwoNumbers(listNode, listNode2);
        }
    }
}
