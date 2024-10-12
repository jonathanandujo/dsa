using System;

namespace BinarySearch
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
            int attempts = 0;
            var first = 0;
            var last = arr.Length-1;
            while (first<=last){
                attempts++;
                var mid = (first + last) / 2;
                if(arr[mid] == target)
                    return attempts;
                if(arr[mid] < target)
                    first = mid +1;
                else
                    last = mid-1;
            }

            return attempts;
        }
    }
}
