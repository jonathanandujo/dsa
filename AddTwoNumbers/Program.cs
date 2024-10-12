using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTreeAverage
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }
    class Program
    {
        public static ListNode AddTwoNumbersRecursive(ListNode l1, ListNode l2)
        {
            int total = 0;
            ListNode result = null;
            if (l1 != null || l2 != null)
            {
                total += l1?.val ?? 0;
                total += l2?.val ?? 0;
                result = new ListNode(total % 10);
                //add carry
                if (total >= 10)
                {
                    if (l1.next != null)
                        l1.next.val += 1;
                    else
                        l1.next = new ListNode(1);
                }
                l1 = l1?.next;
                l2 = l2?.next;
                result.next = AddTwoNumbersRecursive(l1, l2);
            }
            return result;
        }

        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode tmpHead = new ListNode(0);
            ListNode current = tmpHead;
            int carry = 0;

            while (l1 != null || l2 != null)
            {
                int x = l1?.val ?? 0;
                int y = l2?.val ?? 0;
                int sum = carry + x + y;
                carry = sum / 10;

                current.next = new ListNode(sum % 10);
                current = current.next;

                l1 = l1?.next;
                l2 = l2?.next;
            }

            if (carry > 0)
            {
                current.next = new ListNode(carry);
            }

            return tmpHead.next;
        }
        static void Main(string[] args)
        {
            var n1 = new ListNode(5) { next = new ListNode(4) { next = new ListNode(3) } };
            var n2 = new ListNode(5) { next = new ListNode(4) { next = new ListNode(7) } };
            var a = AddTwoNumbers(n1, n2);
            PrintLinkedList(n1);
            PrintLinkedList(n2);
            PrintLinkedList(a);
            Console.WriteLine();
        }
        private static void PrintLinkedList(ListNode input)
        {
            while (input != null)
            {
                Console.Write($"{input.val} ");
                input = input.next;
            }
            Console.WriteLine();
        }
    }
}
