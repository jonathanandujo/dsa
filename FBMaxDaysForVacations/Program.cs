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

    public static int MaxConsecutiveHolidaysV2(string schedule, int pto)
    {
        int left = 0;
        int right = 0;
        while (right < schedule.Length)
        {
            if (schedule[right++] == 'W')
                pto--;
            if (pto < 0)
                if (schedule[left++] == 'W')
                    pto++;
            //Console.WriteLine($"l:{left}-r:{right}-p:{pto}");
        }
        return right - left;
    }

    public static void Main()
    {
        string input = "WHWHWWWHHHWHHHWH";
        int pto = 3;
        Console.WriteLine(MaxConsecutiveHolidays(input, pto)); // Output the result
        Console.WriteLine(MaxConsecutiveHolidaysV2(input, pto)); // Output the result
        Console.WriteLine();
        Random random = new Random();
        pto = random.Next(0, 11);
        int inputLength = random.Next(14, 71);
        char[] inputChars = new char[inputLength];
        for (int i = 0; i < inputLength; i++) 
        { 
            inputChars[i] = random.Next(2) == 0 ? 'W' : 'H'; 
        }
        input = new string(inputChars);
        Console.WriteLine(MaxConsecutiveHolidays(input, pto)); // Output the result
        Console.WriteLine(MaxConsecutiveHolidaysV2(input, pto)); // Output the result
    }
}