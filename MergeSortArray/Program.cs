using System;

namespace MergeSortArray
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var r = new Random();
            int ran = 150;
            var arr = new int[ran];
            for(int i =0; i<ran;i++){
                arr[i] = r.Next(500);
            }
            int[] sorted = SortArray(arr);
            PrintArray(arr);
            PrintArray(sorted);
            Console.WriteLine("finish");
        }

        private static int[] SortArray(int[] arr){
            if(arr.Length <= 1)
                return arr;
            
            var left = new int[arr.Length/2];
            var right = new int[arr.Length-left.Length];
            left = SubArray(arr,0,left.Length);
            right = SubArray(arr,left.Length,right.Length);
            left = SortArray(left);
            right = SortArray(right);

            return Merge(left, right);
        }
        private static int[] Merge(int[] a, int[] b){
            var result = new int[a.Length+b.Length];
            int i =0, j =0, index =0;
            while(i < a.Length && j < b.Length){
                if(a[i] < b[j])
                    result[index++]=a[i++];
                else
                    result[index++]= b[j++];
            }
            //Fill with remaining
            while(i<a.Length)
                result[index++]=a[i++];

            while(j<b.Length)
                result[index++]=b[j++];

            return result;
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
}
