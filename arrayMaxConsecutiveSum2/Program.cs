using System;

namespace arrayMaxConsecutiveSum2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(max(new int[]{1,2,5,-10,7,6,-3,4,5}));
        }
        static int max(int[] input){
            int max = input[0];
            int current=max;
            for(int i=1; i<input.Length; i++){
                current= Math.Max(input[i]+current,input[i]);
                max=Math.Max(current,max);
            }
            return max;
        }
    }
}
