using System;

namespace ArraySortedCountPairs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CountPairs(new int[]{2,4,6,8,9,10},14)); //2 4 - 2 6- 2 8- 2 9- 2 10 - 4 6 - 4 8 - 4 9
        }
        // Count pairs that are lower than target
        static int CountPairs(int[] input,int max){
            int result = 0, indexa = 0, indexb=input.Length-1;
            while(indexa<indexb){
                if(input[indexa]+input[indexb]<max){
                    result+=indexb-indexa;
                    indexa++;
                }
                else {
                    indexb--;
                }
            }
            return result;
        }
    }
}
