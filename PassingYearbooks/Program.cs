using System;
using System.Linq;

namespace PassingYearbooks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var result = findSignatureCounts(new int[]{4,3,2,5,1});
            foreach (int i in result){
                Console.WriteLine(i);
            }
        }

        // Runtime: O(N²) Space: O(N)
        private static int[] findSignatureCounts(int[] arr) {
            int[] result = Enumerable.Repeat(1,arr.Length).ToArray();
            
            for(int i = 0; i<arr.Length;i++){
                var j = i;
                while (arr[j] != i+1) {
                    result[i]++;
                    j = arr[j] -1;
                }
            }
            return result;
        }
    }
}
