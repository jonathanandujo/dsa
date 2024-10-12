using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Round2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        public static int[] Merge(int[] input1, int[] input2) {
            var result = new List<int>();
            var storage = new HashSet<int>();

            for(int i=0; i<input1.Length || i < input2.Length; i++) {
                if(i < input2.Length)
                    if(storage.Add(input2[i]))
                        result.Add(input2[i]);
                
                if(i < input1.Length)
                    if(storage.Add(input1[i]))
                        result.Add(input1[i]);
            }

            return result.ToArray();
        }
    }

    class ProgramTest
    {
        [TestCase(new int[]{1,2,3,4}, new int[] {2,3,5,5})]
        public void ShouldPass1(int[] input1, int[] input2) {
            var resultP = Program.Merge(input1, input2);
            var result = new int[] {2,1,3,5,4};
            Assert.AreEqual(result,resultP);
        }

        [TestCase(new int[]{66,1,2,3,4,5}, new int[] {34,66,1,2,3,4,5,6})]
        public void ShouldPass2(int[] input1, int[] input2) {
            var resultP = Program.Merge(input1, input2);
            var result = new int[] {34,66,1,2,3,4,5,6};
            Assert.AreEqual(result,resultP);
        }
    }
}
