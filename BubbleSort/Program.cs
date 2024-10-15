using System;
using System.Diagnostics;
using NUnit.Framework;

namespace BubbleSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = new Random();
            int ran = 80;
            var arr = new int[ran];
            for (int i = 0; i < ran; i++)
            {
                arr[i] = r.Next(20);
            }
            var arr2 = (int[])arr.Clone();
            PrintArray(arr);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var s = SortArray(arr);
            sw.Stop();
            Console.WriteLine($"Elapsed time: {sw.ElapsedTicks} ticks and {s} swaps");
            PrintArray(arr);
            sw.Restart();
            s = SortArrayStop(arr2);
            sw.Stop();
            Console.WriteLine($"Elapsed time: {sw.ElapsedTicks} ticks and {s} swaps");
            PrintArray(arr2);
            Console.WriteLine("finish");
        }

        public static int SortArray(int[] arr)
        {
            int swaps = 0;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        swaps++;
                    }
                }
            }
            return swaps;
        }

        public static int SortArrayStop(int[] arr)
        {
            int swaps = 0;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                var isSorted = true;
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        isSorted = false;
                        swaps++;
                    }
                }
                if (isSorted)
                    return swaps;
            }
            return swaps;
        }

        private static void PrintArray(int[] arr)
        {
            foreach (int i in arr)
                Console.Write($"{i} ");
            Console.WriteLine();
        }
    }

    class ProgramTest
    {
        [TestCase(new int[] { 5, 32, 3, 23, 5, 4435, 546, 45643, 445, 234, 234, 4, 32, 2, 1, 43, 5546, 456 })]
        public void ShouldReturnSorted(int[] arr)
        {
            var copied = new int[arr.Length];
            Array.Copy(arr, copied, arr.Length);
            Array.Sort(copied);
            Program.SortArray(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != copied[i])
                    Assert.Fail();
            }
        }
    }
}
