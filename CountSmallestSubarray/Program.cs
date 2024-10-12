using System;

namespace CountSmallestSubarray
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SmallestSubWithSum(new int[]{6, 1,4,45,6,0,19}, 51));
        }

        private static int SmallestSubWithSum(int[] arr, int x){
            int len = arr.Length;
            int min = len;
            int sum =0, start =0, end=0;
            while (end < len){
                while (sum<=x && end<len)
                    sum+= arr[end++];

                while(sum > x && start < len){
                    if(end-start<min)
                        min = end-start;

                    sum-= arr[start++];
                }
            }
            return min;
        }

        private static int SmallestSubWithSumSlow(int[] arr, int x){
            int min = arr.Length + 1;
            for(int start =0; start < arr.Length; start++){
                int sum = arr[start];

                if (sum > x) return 1;

                for(int end= start+1; end < arr.Length; end++){
                    sum += arr[end];
                    if(sum > x && end-start+1< min)
                        min = end-start+1;
                    if(sum > x) break;
                }
            }
            return min;
        }
    }
}
