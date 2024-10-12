using System;

namespace AmazonGCD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GCD(new int[]{1,2,3}));
            Console.WriteLine(GCD(new int[]{2,4,6,8}));
            Console.WriteLine(GCD(new int[]{20,40,60,80}));
            Console.WriteLine(GCD(new int[]{21,93}));
        }
        private static int GCD(int[] nums){
            var result =0;
            foreach(int i in nums){
                result = gcd(i,result);
                if(result == 1)
                    return 1;
            }
            return result;
        }
        private static int gcd(int a, int b){
            if (a==0)
                return b;
            return gcd(b%a,a);
        }
    }
}
