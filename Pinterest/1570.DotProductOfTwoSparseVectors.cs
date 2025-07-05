using System;
using System.Collections.Generic;

public class SparseVector
{
    private List<(int index, int value)> pairs;

    public SparseVector(int[] nums)
    {
        pairs = new List<(int, int)>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != 0)
            {
                pairs.Add((i, nums[i]));
            }
        }
    }

    // Return the dot product of two sparse vectors
    public int DotProduct(SparseVector vec)
    {
        int result = 0;
        int p = 0, q = 0;

        while (p < this.pairs.Count && q < vec.pairs.Count)
        {
            var (index1, value1) = this.pairs[p];
            var (index2, value2) = vec.pairs[q];

            if (index1 == index2)
            {
                result += value1 * value2;
                p++;
                q++;
            }
            else if (index1 < index2)
            {
                p++;
            }
            else
            {
                q++;
            }
        }

        return result;
    }
}
