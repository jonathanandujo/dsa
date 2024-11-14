using System;

public class Program
{
    public static int MaxConsecutiveHolidays(string schedule, int pto)
    {
        int maxHolidays = 0;
        int left = 0;
        int workDays = 0;

        for (int right = 0; right < schedule.Length; right++)
        {
            if (schedule[right] == 'W')
            {
                workDays++;
            }

            while (workDays > pto)
            {
                if (schedule[left] == 'W')
                {
                    workDays--;
                }
                left++;
            }

            maxHolidays = Math.Max(maxHolidays, right - left + 1);
        }

        return maxHolidays;
    }

    public static void Main()
    {
        string input = "WHWHWWWHHHWHHHWH";
        int pto = 3;
        Console.WriteLine(MaxConsecutiveHolidays(input, pto)); // Output the result
    }
}