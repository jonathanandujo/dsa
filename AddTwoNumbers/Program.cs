using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTreeAverage
{
    public class ListNode {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }
    class Program
    {
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
            int total = 0;
            ListNode result = null;
            if (l1 != null  || l2 != null){
                total += (l1 != null ? l1.val : 0);
                total += (l2 != null ? l2.val : 0);
                result = new ListNode(total%10);
                //add carry
                if (total>=10){
                    if(l1.next != null)
                        l1.next.val+=1;
                    else
                        l1.next = new ListNode(1);
                }
                l1 = l1 != null ? l1.next : null;
                l2 = l2 != null ? l2.next : null;
                result.next = AddTwoNumbers(l1,l2);
            }
            return result;
        }
        static void Main(string[] args)
        {
            var n1 = new ListNode(5){next = new ListNode(4){next = new ListNode(3)}};
            var n2 = new ListNode(5){next = new ListNode(4){next = new ListNode(3)}};
            var a = AddTwoNumbers(n1,n2);
            PrintLinkedList(n1);
            PrintLinkedList(n2);
            PrintLinkedList(a);
            Console.WriteLine();
        }
        private static void PrintLinkedList(ListNode input) {
            while(input != null){
                Console.Write($"{input.val} ");
                input = input.next;
            }
            Console.WriteLine();
        }
    }
}
