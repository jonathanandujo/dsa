using System;
using System.Collections.Generic;

namespace LinkedListMergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = new Random();
            int ran = 7;
            var list = new LinkedList<int>();
            for(int i =0; i<ran;i++){
                list.AddLast(new LinkedListNode<int>(r.Next(500)));
            }
            PrintList(list);
            var sorted = MergeSort(list);
            PrintList(sorted);
            Console.WriteLine("finish");
        }

        private static LinkedList<int> MergeSort(LinkedList<int> input){
            if (input.Count <= 1)
                return input;
            var left = SubList(input,0, input.Count/2);
            var right = SubList(input, left.Count, input.Count - left.Count);
            left = MergeSort(left);
            right = MergeSort(right);

            return Merge(left,right);
        }
        private static LinkedList<int> Merge(LinkedList<int> a, LinkedList<int> b){
            var result = new LinkedList<int>();
            var left = a.First;
            var rigth = b.First;
            while(left != null && rigth != null){
                if(left.Value < rigth.Value) {
                    result.AddLast(new LinkedListNode<int>(left.Value));
                    left = left.Next;
                }
                else {
                    result.AddLast(new LinkedListNode<int>(rigth.Value));
                    rigth = rigth.Next;
                }
            }
            while(left != null){
                result.AddLast(new LinkedListNode<int>(left.Value));
                left = left.Next;
            }
            while(rigth != null){
                result.AddLast(new LinkedListNode<int>(rigth.Value));
                rigth = rigth.Next;
            }
            return result;
        }

        private static LinkedList<int> SubList(LinkedList<int> input, int start, int len){
            var result = new LinkedList<int>();
            int index=0;
            foreach(int val in input){
                if(index >= start){
                    result.AddLast(new LinkedListNode<int>(val));
                }
                if(result.Count == len)
                    break;
                index++;
            }
            return result;
        }
        private static void PrintList(LinkedList<int> input){
            foreach(int val in input){
                Console.Write($"{val} ");
            }
            Console.WriteLine();
        }
    }
}
