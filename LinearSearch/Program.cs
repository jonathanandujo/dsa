using System;

namespace LinearSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var len = 10000;
            var arr = new int[len];
            for(int i = 1; i<=len; i++)
                arr[i-1]=i;
            Console.WriteLine(FindNumber(arr, len));
        }
        private static int FindNumber(int[] arr, int target){
            int attempts = 0;
            foreach (int i in arr) {
                attempts++;
                if(i == target)
                    return attempts;
            }

            return attempts;
        }
    }
}
