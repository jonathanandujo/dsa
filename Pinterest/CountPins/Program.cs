using System;
using System.Collections.Generic;
using System.Linq;

public class Pin
{
    public int Top { get; set; }
    public int Bottom { get; set; }
    public char Column { get; set; }

    public Pin(int top, int bottom, char column)
    {
        Top = top;
        Bottom = bottom;
        Column = column;
    }
}

public static class PinterestScreenMine
{
    public static int GetMaxPins(List<Pin> pins, int screenLen)
    {

        // Sort by Bottom index
        pins.Sort((a, b) => a.Bottom.CompareTo(b.Bottom));
        var fittedPins = new Queue<Pin>();
        int maxPins = 0;

        bool CanFit(Queue<Pin> queue, Pin newPin)
        {
            if (queue.Count == 0) return true;
            return newPin.Bottom - queue.Peek().Top <= screenLen;
        }

        foreach (var pin in pins)
        {
            while (fittedPins.Count > 0 && !CanFit(fittedPins, pin))
            {
                fittedPins.Dequeue();
            }

            if (CanFit(fittedPins, pin) && (pin.Bottom - pin.Top) <= screenLen)
            {
                fittedPins.Enqueue(pin);
                maxPins = Math.Max(maxPins, fittedPins.Count);
            }
        }

        return maxPins;
    }
}

public static class PinterestScreenCopilot
{
    public static int GetMaxPins(List<Pin> pins, int screenLen)
    {
        return MaxPinsInColumn(pins.Where(p => p.Column == 'L').ToList(), screenLen) +
               MaxPinsInColumn(pins.Where(p => p.Column == 'R').ToList(), screenLen);
    }

    private static int MaxPinsInColumn(List<Pin> pins, int screenLen)
    {
        int maxCount = 0;
        var tops = pins.Select(p => p.Top).Distinct().OrderBy(t => t);

        foreach (var start in tops)
        {
            int end = start + screenLen;
            int count = pins.Count(p => p.Top >= start && p.Bottom <= end);
            maxCount = Math.Max(maxCount, count);
        }

        return maxCount;
    }
}
public class Program
{
    public static void Main()
    {
        var testCases = new List<(List<Pin> pins, int screenLen, int expected)>
        {
            (new List<Pin> { new(1,4,'L'), new(2,3,'R'), new(4,8,'R'), new(6,10,'L') }, 5, 2),
            (new List<Pin> { new(1,4,'L'), new(2,3,'R'), new(5,6,'L'), new(4,10,'R') }, 7, 3),
            (new List<Pin> { new(1,4,'L'), new(4,15,'R'), new(5,6,'L'), new(2,3,'R') }, 7, 3),
            (new List<Pin> { new(1,3,'L'), new(2,4,'R'), new(3,6,'L'), new(4,7,'R'), new(6,9,'L') }, 5, 3),
            (new List<Pin> { new(1,3,'L'), new(3,6,'R') }, 5, 2),
            (new List<Pin> { new(0,4,'L'), new(2,6,'R'), new(4,8,'L') }, 8, 3),
            (new List<Pin> { new(1,4,'L'), new(2,8,'R'), new(3,9,'L') }, 6, 1),
            (new List<Pin> { new(1,4,'L'), new(2,7,'R'), new(3,9,'L') }, 6, 2),
            (new List<Pin> { new(1,3,'L'), new(2,4,'R'), new(3,6,'L'), new(4,7,'R'), new(6,9,'L') }, 5, 3),
            (new List<Pin> { new(1,9,'L'), new(10,18,'R') }, 7, 0)
        };

        int testNumber = 1;
        foreach (var (pins, screenLen, expected) in testCases)
        {
            int result = PinterestScreenCopilot.GetMaxPins(pins, screenLen);
            Console.WriteLine($"Test {testNumber++}: ScreenLen = {screenLen}, Expected = {expected}, Output = {result}, Pass = {result == expected}");
        }

        testNumber = 1;
        foreach (var (pins, screenLen, expected) in testCases)
        {
            int result = PinterestScreenMine.GetMaxPins(pins, screenLen);
            Console.WriteLine($"Test {testNumber++}: ScreenLen = {screenLen}, Expected = {expected}, Output = {result}, Pass = {result == expected}");
        }
    }
}


