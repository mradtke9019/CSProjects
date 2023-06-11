using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    /**
     * Definition for singly-linked list.
     * public class ListNode {
     *     public int val;
     *     public ListNode next;
     *     public ListNode(int val=0, ListNode next=null) {
     *         this.val = val;
     *         this.next = next;
     *     }
     * }
     */
    public partial class Solution
    {
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 == null)
                return new ListNode();
            if(l1 == null)
                return new ListNode(l2.val);
            if (l2 == null)
                return new ListNode(l1.val);
            string sX = l1.val.ToString();
            string sY = l2.val.ToString();
            
            int x = l1.val;
            int y = l2.val;
            int i = 0;

            while (l1 != null)
            {
                if (i != 0)
                {
                    sX = string.Concat(l1.val, sX);
                }
                l1 = l1.next;
                i++;
            }
            x = int.Parse(sX);

            i = 0;

            while (l2 != null)
            {
                if (i != 0)
                {
                    sY = string.Concat(l2.val, sY);
                }
                l2 = l2.next;
                i++;
            }
            y = int.Parse(sY);

            int result = x + y;
            int[] resultArray = result.ToString().Select(x => int.Parse(x.ToString())).ToArray();

            ListNode answer = new ListNode();
            ListNode curr = answer;

            for (i = resultArray.Length -1; i >= 0; i--)
            {
                curr.val = resultArray[i];

                if(i > 0)
                {
                    curr.next = new ListNode();
                    curr = curr.next;
                }
            }

            return answer;
        }
    }
}
