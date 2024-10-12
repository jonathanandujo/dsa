using System;
using System.Collections.Generic;

namespace ContainsDuplicate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(ContainsDuplicate(new int[]{1,1}));
        }

        static bool ContainsDuplicate(int[] nums) {
            if (nums.Length < 1) return false;
            
            var hs = new HashSet<int>();
            for(int i = 0; i < nums.Length; i++){
                if(hs.Contains(nums[i])) return true;
                hs.Add(nums[i]);
            }
            return false;
        }
    }
}
