using System;

namespace SumAllNonnegative
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Sum(10));
        }
        private static int Sum(int n){
            if(n<=0)
                return 0;
            return Sum(n-1)+n;
        }
    }
}
