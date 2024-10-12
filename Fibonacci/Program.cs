using System;
using System.Diagnostics;

namespace Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var n = 590;
            Stopwatch sw = new Stopwatch();
            long result = 0;
            sw.Start();
            result = SFib(n);
            sw.Stop();
            Console.WriteLine("Fib={0} time:{1}",result,sw.ElapsedTicks);
            sw.Restart();
            result = RFib(n);
            sw.Stop();
            Console.WriteLine("RFib={0} time:{1}",result,sw.ElapsedTicks);
            sw.Restart();
            result = SFib(n);
            sw.Stop();
            Console.WriteLine("SFib={0} time:{1}",result,sw.ElapsedTicks);
        }
        //With storage
        static long SFib(long n){
            var storage = new long[n+1];
            return SFibAux(n,storage);
        }
        static long SFibAux(long n, long[] storage){
            if(storage[n]>0)
                return storage[n];
            if(n <= 2){
                storage[n]=1;
                return 1;
            }
            storage[n] = SFibAux(n-1,storage)+SFibAux(n-2,storage);
            return storage[n];
        }
        static long RFib(long n){
            if(n<= 2)
                return 1;
            var bu = new long[n+1];
            bu[1]=1;
            bu[2]=1;
            for(long i=3; i <= n;i++){
                bu[i]=bu[i-1]+bu[i-2];
            }
            return bu[n];
        }
        static long Fib(long n){
            if (n<=2){
                return 1;
            }
            return Fib(n-1)+Fib(n-2);
        }

        static long FibW(long n){
            if (n<=1){
                return 1;
            }
            return FibW(n-1)+FibW(n-1);
        }
    }
}
