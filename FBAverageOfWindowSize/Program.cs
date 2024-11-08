using System.Diagnostics;

class Program
{
    static void Main()
    {
        //int[] array = { 5,12,7,2,78,6,3,1,2 };
        int[] array = Enumerable.Range(1, 1000000).ToArray();
        int windowSize = 3;

        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        var x = AverageWindowSize(array, windowSize);
        stopwatch.Stop();
        Console.WriteLine($"AverageWindowSize execution time: {stopwatch.ElapsedMilliseconds} ms");

        stopwatch.Reset();

        stopwatch.Start();
        var y = AverageWindowSizeCopilot(array, windowSize);
        stopwatch.Stop();
        Console.WriteLine($"AverageWindowSizeCopilot execution time: {stopwatch.ElapsedMilliseconds} ms");

        // foreach (var item in x)
        // {
        //     Console.WriteLine(item);
        // }
        // Console.WriteLine("---------");
        // foreach (var item in y)
        // {
        //     Console.WriteLine(item);
        // }
    }

    static double[] AverageWindowSizeCopilot(int[] arr, int windowSize)
    {
        double[] result = new double[arr.Length - windowSize + 1];
        int windowSum = 0;
        int windowStart = 0;
        for (int windowEnd = 0; windowEnd < arr.Length; windowEnd++)
        {
            windowSum += arr[windowEnd];
            if (windowEnd >= windowSize - 1)
            {
                result[windowStart] = windowSum / windowSize;
                windowSum -= arr[windowStart];
                windowStart++;
            }
        }
        return result;
    }

    static double[] AverageWindowSize(int[] arr, int windowSize)
    {
        double[] result = new double[arr.Length - windowSize + 1];
        // validate window size
        if (windowSize < 1)
            return result;
        double sum = 0;
        for (int i = 0; i <= arr.Length - windowSize; i++)
        {
            if (i == 0)
            {
                int start = i;
                while (start < windowSize + i)
                {
                    sum += arr[start++];
                }
            }
            else
            {
                sum = sum - arr[i - 1] + arr[i+windowSize-1];
            }
            result[i] = sum / windowSize;
        }
        return result;
    }
}
