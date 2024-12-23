// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var obj = new StockSpanner();
// Console.WriteLine(obj.Next(100));
// Console.WriteLine(obj.Next(80));
// Console.WriteLine(obj.Next(60));
// Console.WriteLine(obj.Next(70));
// Console.WriteLine(obj.Next(60));
// Console.WriteLine(obj.Next(75));
// Console.WriteLine(obj.Next(85));
Console.WriteLine(obj.Next(31));
Console.WriteLine(obj.Next(41));
Console.WriteLine(obj.Next(41));
Console.WriteLine(obj.Next(48));
Console.WriteLine(obj.Next(59));
public class StockSpanner
{
    private int[] prices;
    private int currentIndex;
    public StockSpanner()
    {
        prices = new int[10000];
        currentIndex = 0;
    }

    public int Next(int price)
    {
        prices[currentIndex++] = price;
        int result = 1;
        for (int i = currentIndex - 2; i >= 0; i--)
        {
            if (prices[i] <= price)
                result++;
            else
                break;
        }
        return result;
    }

    // public int Next(int price)
    // {
    //     prices[currentIndex++] = price;
    //     int result = 1;
    //     for(int i = currentIndex-2; i > 0; i--)
    //     {
    //         if(prices[i] > price)
    //             return result;
    //         result++;
    //     }
    //     return result;
    // }
}

public class StockSpannerCopilot
{
    private Stack<int[]> stack;
    public StockSpannerCopilot()
    {
        stack = new Stack<int[]>();
    }

    public int Next(int price)
    {
        int span = 1;
        while (stack.Count > 0 && stack.Peek()[0] <= price)
        {
            span += stack.Pop()[1];
        }
        stack.Push(new int[] { price, span });
        return span;
    }
}