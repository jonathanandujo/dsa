using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main()
    {
        // Generate a large wood array with random lengths
        Random rand = new Random();
        int[] wood = new int[1000000];
        for (int i = 0; i < wood.Length; i++)
        {
            wood[i] = rand.Next(1, 200);  // Random lengths between 1 and 1000
        }

        int k = 20;  // Required number of pieces

        // Measure execution time for MaxLen
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        int resultMaxLen = MaxLen(wood, k);
        stopwatch.Stop();
        Console.WriteLine($"MaxLen result: {resultMaxLen}, Time: {stopwatch.ElapsedTicks} ticks");

        // Measure execution time for MaxLenCopilot
        stopwatch.Restart();
        int resultMaxLenCopilot = MaxLenCopilot(wood, k);
        stopwatch.Stop();
        Console.WriteLine($"MaxLenCopilot result: {resultMaxLenCopilot}, Time: {stopwatch.ElapsedTicks} ticks");
    }

    static int MaxLen(int[] wood, int k)
    {
        long loops = 0;
        int low = 1, high = wood.Max();
        bool found = false;
        while (low < high)
        {
            int mid = (low + high + 1) / 2;
            int pieces = 0;
            foreach (int w in wood)
            {
                loops++;
                pieces += w / mid;
                if (pieces >= k)
                    break;
            }
            if (pieces < k)
            {
                high = mid - 1;
            }
            else
            {
                found = true;
                low = mid;
            }
        }
        Console.WriteLine($"Loops: {loops}");
        return found ? low : 0;
    }

    static int MaxLenCopilot(int[] wood, int k)
    {
        long loops = 0;
        int low = 1, high = wood.Max();
        while (low < high)
        {
            int mid = (low + high + 1) / 2;
            int pieces = 0;
            foreach (int w in wood)
            {
                loops++;
                pieces += w / mid;
            }
            if (pieces < k)
            {
                high = mid - 1;
            }
            else
            {
                low = mid;
            }
        }
        Console.WriteLine($"Loops copilot: {loops}");
        return low;
    }
}