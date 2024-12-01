// See https://aka.ms/new-console-template for more information
using System.Security.AccessControl;

Console.WriteLine("Hello, World!");

List<int> UniqueNumbersInSortedArray(int[] nums)
{
    List<int> result = [nums[0]];
    int left = 1;
    while(result[^1] != nums[^1])
    {
        int right = nums.Length -1;
        while(left < right)
        {
            int mid = (left+right) / 2;
            if(nums[mid] == result[^1])
                left = mid +1;
            else
                right = mid;
        }
        result.Add(nums[left]);
    }
    return result;
}

foreach(int i in UniqueNumbersInSortedArray([1,1,1,1,1,2,2,3,3,3,3,4,5,5,5,5,5,79,90,100]))
    Console.Write($"{i} ");