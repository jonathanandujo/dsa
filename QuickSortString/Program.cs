using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickSortString
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = new Random();
            int ran = 5;
            var arr = new string[ran];
            for(int i =0; i<ran;i++){
                arr[i] = Guid.NewGuid().ToString();
            }
            string[] sorted = SortArray(arr);
            PrintArray(arr);
            PrintArray(sorted);
            Console.WriteLine("finish");
        }

        private static string[] SortArray(string[] arr){
            if (arr.Length <= 1)
                return arr;

            string pivot = arr[0];
            List<string> left = new List<string>();
            List<string> right = new List<string>();
            for(int i =1; i<arr.Length; i++){
                if(arr[i][0] < pivot[0])
                    left.Add(arr[i]);
                else
                    right.Add(arr[i]);
            }
            var merge = SortArray(left.ToArray()).ToList();
            merge.Add(pivot);
            merge.AddRange(SortArray(right.ToArray()).ToList());
            return merge.ToArray();
        }

        private static string[] SubArray(string[] arr, int start, int len){
            var result = new string[len];
            Array.Copy(arr,start,result,0,len);
            return result;
        }
        private static void PrintArray(string[] arr){
            foreach(string i in arr)
                Console.Write($"{i} ");
            Console.WriteLine();
        }
    }
}