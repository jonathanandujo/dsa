using System;
using System.Collections.Generic;

namespace MedianStream
{
    class MyStructure{
        private Stack<int> low;
        private Stack<int> high;
        
        public MyStructure(){
            low = new Stack<int>();
            high = new Stack<int>();
        }
        
        public void Add(int i){
            if(low.Count == 0)
                low.Push(i);
            else {
            if(i<low.Peek())
                InsertLow(i);
            else
                InsertHigh(i);
            
            Balance();
            }
        }
        
        private void InsertLow(int i){
            while(low.Count > 0 && i<low.Peek())
            high.Push(low.Pop());
            low.Push(i);
        }
        
        private void InsertHigh(int i){
            while(high.Count > 0 && i>high.Peek())
                low.Push(high.Pop());
            high.Push(i);
        }
        
        private void Balance(){
            Console.WriteLine($"start Balance low-{low.Count} high-{high.Count}");
            while(low.Count > high.Count)
            high.Push(low.Pop());
            while(low.Count < high.Count)
            low.Push(high.Pop());
            Console.WriteLine($"end Balance low-{low.Count} high-{high.Count}");
        }
        
        public int GetMedian(){
            if(low.Count > high.Count)
            return low.Peek();
            return (low.Peek() + high.Peek()) / 2;
        }
        }
    class Program
    {
        static void Main(string[] args)
        {
            findMedian(new int[]{5,15,1,3});
        }

        private static int[] findMedian(int[] arr) {
            if(arr.Length == 0) return arr;

            var st = new MyStructure();

            for(int i=0; i<arr.Length; i++){
                st.Add(arr[i]);
                arr[i] = st.GetMedian();
                Console.WriteLine(st.GetMedian());
            }
            return arr;
        }
    }
}
