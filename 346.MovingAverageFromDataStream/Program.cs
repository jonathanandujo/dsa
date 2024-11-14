Console.WriteLine("Hello World!");
MovingAverage mavg = new MovingAverage(3);
Console.WriteLine(mavg.Next(1));
Console.WriteLine(mavg.Next(10));
Console.WriteLine(mavg.Next(3));
Console.WriteLine(mavg.Next(5));
Console.WriteLine(mavg.Next(5));
Console.WriteLine(mavg.Next(5));

class MovingAverage{
    private int[] window;
    private int n, insert;
    private long sum;
    /** Initialize your data structure here. */
    public MovingAverage(int size) {
        window = new int[size];
        n = insert = 0;
        sum = 0;
    }
    
    public double Next(int val) {
        if (n < window.Length) n++;
        sum -= window[insert];
        sum += val;
        window[insert] = val;
        insert = (insert + 1) % window.Length;
        return (double)sum / n;
    }
}