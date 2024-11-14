using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int[] nums = { 0, 1, 3, 50, 75 };
        int lower = 0;
        int upper = 99;
        var result = FindMissingRanges(nums, lower, upper);
        foreach (var range in result)
        {
            Console.WriteLine($"[{range[0]}, {range[1]}]");
        }
    }

    static IList<IList<int>> FindMissingRanges(int[] nums, int lower, int upper)
    {
        IList<IList<int>> result = new List<IList<int>>();
        int previous = lower - 1;

        foreach (int n in nums)
        {
            if (n > previous + 1)
            {
                result.Add(new List<int> { previous + 1, n - 1 });
            }
            previous = n;
        }

        if (previous < upper)
        {
            result.Add(new List<int> { previous + 1, upper });
        }

        return result;
    }
}