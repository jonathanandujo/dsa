using System;
using NUnit.Framework;

namespace MaxLenRepeatedSubarray
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(FindLength(new int[]{1,2,3,2,1}, new int[]{3,2,1,4,7}));
        }

        public static int FindLength(int[] nums1, int[] nums2) {
            int result = 0;
            var storage = new int[nums1.Length+1][];
            for(int i = 0; i < storage.Length; i++)
                storage[i] = new int[nums2.Length+1];
                
            for(int i = 1; i <= nums1.Length; i++) {
                for(int j = 1; j <= nums2.Length; j++) {
                    if (nums1[^i] == nums2[^j]) {
                        storage[^(i+1)][^(j+1)] = storage[^i][^j] +1;
                        if (result < storage[^(i+1)][^(j+1)])
                            result = storage[^(i+1)][^(j+1)];
                    }
                }
            }
            return result;
        }
    }
    
    class ProgramTest
    {
        [TestCase(new int[]{1,2,3,2,1}, new int[]{3,2,1,4,7})]
        public void Should_Retrun_3(int[] nums1, int[] nums2) {
            var result = Program.FindLength(nums1,nums2);
            Assert.AreEqual(result,3);
        }
    }
}
