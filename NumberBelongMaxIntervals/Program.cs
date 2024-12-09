// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
int[][] intervals = [[-3,5],[0,2],[8,10],[6,7]]; //0 1 2 or
Console.WriteLine(NumberBelongMaxIntervals(intervals));
int NumberBelongMaxIntervals(int[][] intervals)
{
    Dictionary<int,int> counter = new();
    foreach(int[] interval in intervals)
    {
        while(interval[0] <= interval[1])
        {
            if(!counter.ContainsKey(interval[0]))
                counter[interval[0]] = 0;
            counter[interval[0]++]++;
        }
    }
    int maxNumber = 0;
    foreach(int val in counter.Values)
    {
        maxNumber = Math.Max(maxNumber, val);
    }
    return maxNumber;
}