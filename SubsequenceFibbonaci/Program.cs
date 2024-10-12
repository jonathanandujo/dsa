using System;
using System.Collections.Generic;
using System.Linq;

namespace SubsequenceFibbonaci
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = GetValues(new int[] {2,5,4,2,9,21, 1, 0, 23, 234, 71, 83, 90, 9000, 34});
            PrintArray(x);
        }

        private static int[] GetValues(int[] arr){
            int max = arr.Max(); //if not possible we use a for
            int previous1 = 0, previos2 = 1;
            var fibbonacci = new HashSet<int>();
            var result = new List<int>();
            while( previos2 < max){
                var fib = previous1 + previos2;
                previous1 = previos2;
                previos2 = fib;
                fibbonacci.Add(fib);
            }
            foreach(int i in arr){
                if(fibbonacci.Contains(i))
                    result.Add(i);
            }
            return result.ToArray();
        }
        private static void PrintArray(int[] arr){
            foreach(int i in arr){
                Console.Write($"{i} ");
            }
            Console.WriteLine();
        }
    }
}
