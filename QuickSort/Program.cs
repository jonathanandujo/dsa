using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = new Random();
            int ran = 5;
            var arr = new int[ran];
            for(int i =0; i<ran;i++){
                arr[i] = r.Next(10);
            }
            int[] sorted = SortArray(arr);
            PrintArray(arr);
            PrintArray(sorted);
            Console.WriteLine("finish");
        }

        public static int[] SortArray(int[] arr){
            if (arr.Length <= 1)
                return arr;

            int pivot = arr[0];
            List<int> left = new List<int>();
            List<int> right = new List<int>();
            for(int i =1; i<arr.Length; i++){
                if(arr[i]<pivot)
                    left.Add(arr[i]);
                else
                    right.Add(arr[i]);
            }
            var merge = SortArray(left.ToArray()).ToList();
            merge.Add(pivot);
            merge.AddRange(SortArray(right.ToArray()).ToList());
            return merge.ToArray();
        }

        private static int[] SubArray(int[] arr, int start, int len){
            var result = new int[len];
            Array.Copy(arr,start,result,0,len);
            return result;
        }
        private static void PrintArray(int[] arr){
            foreach(int i in arr)
                Console.Write($"{i} ");
            Console.WriteLine();
        }
    }

    class ProgramTest 
    {
        [TestCase(new int[]{5,32,3,23,5,4435,546,45643,445,234,234,4,32,2,1,43,5546,456})]
        public void ShouldReturnSorted(int[] arr) {
            var copied = new int[arr.Length];
            Array.Copy(arr,copied,arr.Length);
            Array.Sort(copied);
            arr = Program.SortArray(arr);
            for (int i = 0; i < arr.Length; i++) {
                if(arr[i]!=copied[i])
                    Assert.Fail();
            }
        }
    }
}
