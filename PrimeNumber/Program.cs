using System;

namespace PrimeNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var n = 5000;
            for(int i = 1; i<=n; i=i+2){
                if (isPrime(i))
                    Console.Write($"{i} ");
            }
        }
        static bool isPrime(int n){
            for(int i = 2; i*i<=n;i++){
                if (n%i==0)
                    return false;
            }
            return true;
        }
    }
}
