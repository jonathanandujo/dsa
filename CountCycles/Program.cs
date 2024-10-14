using System;
using NUnit.Framework;

namespace CountCycles
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var arr = new int[] {0,1,2,3};
            Console.WriteLine(CountCycles(arr));
            Console.WriteLine(CountCyclesNoExtraSpace(arr));
        }
        public static int CountCycles(int[] arr) {
            int result = 0;
            var visited = new bool[arr.Length];
            for(int i=0; i < arr.Length; i++){
                if (visited[i])
                    continue;
                result += 1;
                VisitCycle(arr,visited, i);
            }
            return result;
        }
        public static int CountCyclesNoExtraSpace(int[] arr) {
            int result = 0;
            for(int i=0; i < arr.Length; i++){
                if (arr[i]==-1)
                    continue;
                result += 1;
                VisitCycleNoExtraSpace(arr, i);
            }
            return result;
        }
        private static void VisitCycle(int[] arr, bool[] visited, int index) {
            if(visited[index])
                return;
            visited[index] = true;
            VisitCycle(arr, visited, arr[index]);
        }
        private static void VisitCycleNoExtraSpace(int[] arr, int index)
        {
            if (arr[index] == -1)
                return;
            int nextIndex = arr[index];
            arr[index]=-1;
            VisitCycleNoExtraSpace(arr,nextIndex);
        }
    }

    class ProgramTest
    {
        [TestCase(new int[]{3,0,1,2,4,6,5})]
        public void ShouldPass3(int[] arr) {
            var result = Program.CountCycles(arr);
            Assert.AreEqual(result, 3);
            result = Program.CountCyclesNoExtraSpace(arr);
            Assert.AreEqual(result, 3);
        }

        [TestCase(new int[]{0,1,2,3,4})]
        public void ShouldPass5(int[] arr) {
            var result = Program.CountCycles(arr);
            Assert.AreEqual(result, 5);
            result = Program.CountCyclesNoExtraSpace(arr);
            Assert.AreEqual(result, 5);
        }
    }
}
