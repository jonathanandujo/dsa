using System;

namespace BinarySearchRecursive
{
    class Program
    {
        static void Main(string[] args)
        {
            var len = 9000000;
            var arr = new int[len];
            for(int i = 1; i<=len; i++)
                arr[i-1]=i;
            Console.WriteLine(FindNumber(arr, len));
        }
        private static int FindNumber(int[] arr, int target){
            if (arr.Length == 0 || (arr.Length == 1 && arr[0] != target))
                return -int.MaxValue;
            var mid = (arr.Length) / 2;
            if(arr[mid] == target)
                return 1;
            if(arr[mid] < target)
                return FindNumber(subarray(arr,mid,arr.Length-mid),target)+1;
            else
                return FindNumber(subarray(arr,0,mid),target)+1;
        }

        private static int[] subarray(int[] arr, int start, int len){
            int[] suba = new int[len];
            Array.Copy(arr,start, suba,0,len);
            return suba;
        }
    }
}
