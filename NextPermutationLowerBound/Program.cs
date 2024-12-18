// See https://aka.ms/new-console-template for more information
using System.Text;

Console.WriteLine("Hello, World!");
Console.WriteLine(BuildNextNumber(new List<char> {'1', '2', '3'}, "200")); // 2 1 3
Console.WriteLine(BuildNextNumber(new List<char> {'1', '2', '3'}, "250")); // 3 1 2
Console.WriteLine(BuildNextNumber(new List<char> {'1', '5', '8', '6', '4'}, "1")); // 1 4 5 6 8
Console.WriteLine(BuildNextNumber(new List<char> {'1', '5', '8', '6', '4'}, "30585")); // 4 1 5 6 8

static string BuildNextNumber(List<char> digits, string lowerBound)
{
    int[] digitsArray = digits.Select(x => int.Parse(x.ToString())).ToArray();
    Array.Sort(digitsArray);
    int currentValue = digitsArray.Aggregate((x, y) => x * 10 + y);
    while(currentValue < int.Parse(lowerBound))
    {
        NextPermutation(digitsArray);
        currentValue = digitsArray.Aggregate((x, y) => x * 10 + y);
    }
    return string.Join(" ", digitsArray);
}

static void NextPermutation(int[] nums) {
    int indexA = -1;
    int indexB = -1;
    for(int i = 1; i <= nums.Length-1; i++)
    {
        if(nums[^(i+1)]<nums[^i])
        {
            indexA = nums.Length-(i+1);
            break;
        }
    }

    if (indexA == -1){
        Reverse(nums,0, nums.Length-1);
        return;
    }

    for(int i = 1; i <= nums.Length; i++)
    {
        if(nums[^i]>nums[indexA])
        {
            indexB = nums.Length-i;
            break;
        }
    }
    (nums[indexA],nums[indexB]) = (nums[indexB],nums[indexA]);
    Reverse(nums, indexA+1,nums.Length-1);

    void Reverse(int[] nums, int left, int right)
    {
        while(left<right)
            (nums[left],nums[right]) = (nums[right--],nums[left++]);
    }
}