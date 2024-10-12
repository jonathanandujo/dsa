using System;

namespace LinkedListBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var ll = new LinkedList(0);
            ll.Insert(1);
            ll.Insert(2);
            ll.Insert(3);
            ll.Insert(4);
            Console.WriteLine(ll.Size());
            Console.WriteLine(ll.size);
            ll.Print();
            ll.InsertHead(5);
            ll.Print();
            var x = reversed(ll);
            x.Print();
            Console.WriteLine(ll.Size());
            Console.WriteLine(ll.size);
            var test1 = ll.Search(2);
            test1.Print();
            test1.Insert(6,1);
            test1.Print();
            test1.Insert(7,10);
            test1.Print();
        }
        private static LinkedList reversed(LinkedList head){
            if (head == null) return head;
            var reversed = new LinkedList(head.value);
            while(head.next != null){
                head = head.next;
                reversed = new LinkedList(head.value, reversed);
            }
            return reversed;
        }
    }

    class LinkedList
    {
        public LinkedList next;
        public int value;

        // Runtime: O(1)
        public int size;
        public LinkedList(int val, LinkedList next = null)
        {
            this.value = val;
            this.next = next;
            this.size = 1;
        }
        public void Insert(int val)
        {
            LinkedList end = new LinkedList(val);
            LinkedList n = this;
            while (n.next != null) {
                n = n.next;
            }
            n.next = end;
            this.size++;
        }

        public void Insert(int val, int index){
            LinkedList next = new LinkedList(val);
            LinkedList n = this;
            while (n.next != null) {
                if(index-- == 0)
                    break;
                n = n.next;
            }
            next.next = n;
            n = next;
            this.size++;
        }

        public void InsertHead(int val){
            LinkedList n = this;
            LinkedList newll = new LinkedList(this.value);
            newll.next = n.next;
            n.next = newll;
            n.size++;
            n.value = val;
        }

        public void Print(){
            LinkedList n = this;
            while(n != null) {
                Console.Write($"{n.value} ");
                n = n.next;
            }
            Console.WriteLine();
        }

        public LinkedList Search(int val){
            LinkedList n = this;
            while(n != null){
                if(n.value == val)
                    return n;
                else
                    n=n.next;
            }
            return null;
        }

        // Runtime: O(N)
        public int Size(){
            int count = 1;
            LinkedList n = this;
            while(n.next != null){
                count++;
                n = n.next;
            }
            return count;
        }
    }
}
