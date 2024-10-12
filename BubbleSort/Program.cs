using System;
using NUnit.Framework;

namespace BubbleSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = new Random();
            int ran = 10;
            var arr = new int[ran];
            for(int i =0; i<ran;i++){
                arr[i] = r.Next(15);
            }
            PrintArray(arr);
            SortArray(arr);
            PrintArray(arr);
            Console.WriteLine("finish");
        }

        public static void SortArray(int[] arr){
            for(int i =0; i<arr.Length-1; i++){
                for(int j =0; j<arr.Length-1; j++){
                    if(arr[j]>arr[j+1]){
                        var tmp = arr[j+1];
                        arr[j+1] = arr[j];
                        arr[j]=tmp;
                    }
                }
            }
        }

        private static void PrintArray(int[] arr){
            foreach(int i in arr)
                Console.Write($"{i} ");
            Console.WriteLine();
        }
    }

    class ProgramTest 
    {
        [TestCase(new int[]{5,32,3,23,5,4435,546,45643,445,234,234,4,32,2,1,43,5546,456})]
        public void ShouldReturnSorted(int[] arr){
            var copied = new int[arr.Length];
            Array.Copy(arr,copied,arr.Length);
            Array.Sort(copied);
            Program.SortArray(arr);
            for (int i = 0; i < arr.Length; i++) {
                if(arr[i]!=copied[i])
                    Assert.Fail();
            }
        }
    }
}
