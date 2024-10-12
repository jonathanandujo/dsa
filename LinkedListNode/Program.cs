using System;

namespace ListNodeNode
{
    class Program
    {
        class ListNode{
            public int val;
            public ListNode next;
            public ListNode(int val){
                this.val = val;
            }
            public ListNode(int val, ListNode next){
                this.val = val;
                this.next = next;
            }

            public void Insert(int val)
            {
                ListNode end = new ListNode(val);
                ListNode n = this;
                while (n.next != null) {
                    n = n.next;
                }
                n.next = end;
            }

            public void Insert(int val, int index){
                if (index == 0)
                {
                    InsertHead(val);
                    return;
                }

                ListNode newNode = new ListNode(val);
                ListNode n = this;
                while (n.next != null) {
                    if(index-- == 0)
                        break;
                    n = n.next;
                }
                newNode.next = n.next;
                n.next = newNode;
            }

            public void Remove(int val){
                ListNode n = this;
                ListNode prev = null;
                var found = false;

                while (n != null && !found){
                    if(n.val == val && n == this){
                        found = true;
                        n.val = n.next.val;
                        n.next = n.next.next;
                        return;
                    }
                    else if (n.val == val){
                        prev.next = n.next;
                        return;
                    }
                    else
                        prev = n;
                        n = n.next;
                }
            }

            public void InsertHead(int val){
                ListNode n = this;
                ListNode newll = new ListNode(this.val);
                newll.next = n.next;
                n.next = newll;
                n.val = val;
            }

            public void Print(){
                ListNode n = this;
                while(n != null) {
                    Console.Write($"{n.val} ");
                    n = n.next;
                }
                Console.WriteLine();
            }

            public ListNode Search(int val){
                ListNode n = this;
                while(n != null){
                    if(n.val == val)
                        return n;
                    else
                        n=n.next;
                }
                return null;
            }

            // Runtime: O(N)
            public int Size(){
                int count = 1;
                ListNode n = this;
                while(n.next != null){
                    count++;
                    n = n.next;
                }
                return count;
            }
        }


        static void Main(string[] args)
        {
            ListNode head = new ListNode(0);
            head.Insert(1);
            head.Insert(2);
            head.InsertHead(-1);
            head.Insert(3,0);
            head.Print();
            head.Remove(2);
            head.Print();
            head.Remove(3);
            head.Print();
        }
    }
}
