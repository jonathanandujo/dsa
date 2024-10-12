using System;

namespace ReverseInteger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Reverse(1534236469));
            Console.WriteLine(Reverse(789));
        }
        static int Reverse(int x) {
            int result = 0;
            while (x!=0){
                if(Math.Abs(result) > int.MaxValue/10) return 0;
                result=result*10+(x%10);
                x=x/10;
            }
            return result;
        }
    }
}
