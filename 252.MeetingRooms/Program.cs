// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine(CanAttendMeetings(new int[][] { new int[] { 0, 30 }, new int[] { 5, 10 }, new int[] { 15, 20 } })); // false
Console.WriteLine(CanAttendMeetings(new int[][] { new int[] { 7, 10 }, new int[] { 2, 4 } })); // true
Console.WriteLine(CanAttendMeetings(new int[][] { new int[] { 13, 15 }, new int[] { 1, 13 } })); // true

static bool CanAttendMeetings(int[][] intervals)
{
    Array.Sort(intervals, (a, b) => a[0] - b[0]);

    for(int i =1; i<intervals.Length; i++)
    {
        if(intervals[i][0] < intervals[i-1][1])
            return false;
    }
    return true;
}