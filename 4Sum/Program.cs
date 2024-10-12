using System;
using System.Collections.Generic;

namespace _4Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var x = FourSum(new int[]{1,0,-1,0,-2,2}, 0);
            foreach(List<int> l in x) {
                foreach(int i in l)
                    Console.Write($"{i} ");
                Console.WriteLine();
            }
        }

        public static IList<IList<int>> FourSum(int[] nums, int target) {
            Array.Sort(nums);
            return RecursiveSolution(nums, target, 0, 4);
        }
        
        private static List<IList<int>> RecursiveSolution(int[] nums, int target, int start, int k) {
            var res = new List<IList<int>>();
            if (start == nums.Length || nums[start] * k > target || target > nums[nums.Length - 1] * k)
                return res;
            if (k == 2)
                return twoSum(nums, target, start);
            for (int i = start; i < nums.Length; ++i)
                if (i == start || nums[i - 1] != nums[i]){
                    foreach (List<int> ls in RecursiveSolution(nums, target - nums[i], i + 1, k - 1)) {
                        res.Add(new List<int>() {nums[i]});
                        foreach(int val in ls)
                            res[res.Count-1].Add(val);
                    }
                }
            return res;
        }

        private static List<IList<int>> twoSum(int[] nums, int target, int start) {
            var res = new List<IList<int>>();
            var s = new HashSet<int>();
            for (int i = start; i < nums.Length; ++i) {
                 if (res.Count == 0 || res[res.Count-1][1] != nums[i])
                    if (s.Contains(target - nums[i]))
                        res.Add(new List<int> {target - nums[i],nums[i]});
                s.Add(nums[i]);
            }
            return res;
        }
    }
}
