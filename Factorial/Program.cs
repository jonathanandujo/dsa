using System;

namespace Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Factorial(20));
        }
        static long Factorial(long n){
            if (n == 1)
                return n;
            return n*(Factorial(n-1));
        }
    }
}
