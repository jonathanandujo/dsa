using System;
using System.Collections.Generic;

namespace PalindromeLinkedList
{
    public class ListNode {
        public int val;
        public ListNode next;
        public ListNode(int val=0, ListNode next=null) {
            this.val = val;
            this.next = next;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ListNode head = new ListNode(1,new ListNode(2,new ListNode(1)));
            Console.WriteLine(IsPalindrome(head));
        }



        static bool IsPalindrome(ListNode head) {
            Stack<int> storage = new Stack<int>();
            var tmp = head;
            while(tmp != null){
                storage.Push(tmp.val);
                tmp = tmp.next;
            }
            
            while(head != null){
                if(head.val != storage.Pop())
                    return false;
                head = head.next;
            }
            return true;
        }
    }
}

