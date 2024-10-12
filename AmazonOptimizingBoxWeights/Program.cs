using System;
using System.Collections.Generic;

namespace AmazonOptimizingBoxWeights
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = minimalHeaviestSetA(new int[]{5,3,2,4,1,2});
            foreach(int i in x){
                Console.WriteLine(i);
            }
        }
        private static int[] minimalHeaviestSetA(int[] arr){
            //clone array
            int[]sorted = new int[arr.Length];
            for(int i=0;i<arr.Length;i++){
                sorted[i]=arr[i];
            }
            Array.Sort(sorted);
            List<int> result = new List<int>();
            int indexLeft=0, indexRight = sorted.Length-1;
            int sumA=0, sumB=0, min = int.MaxValue;
            while (indexLeft <= indexRight){
                if(sumB + sorted[indexLeft] < sumA){
                    sumB += sorted[indexLeft++];
                }
                else {
                    min = sorted[indexRight--];
                    sumA += min; 
                }
            }
            foreach(int i in arr){
                if(i>=min)
                 result.Add(i);
            }
            return result.ToArray();
        }
    }
}
