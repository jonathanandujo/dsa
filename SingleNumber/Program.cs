using System;
using System.Collections.Generic;
using System.Linq;

namespace SingleNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //var y =SingleNumberXor(new int[6]{1,2,1,3,2,5});
            var y =SingleNumberXor(new int[6]{54,52,56,53,52,54});
            Console.WriteLine($"{y[0]},{y[1]}");
            // Console.WriteLine(SingleNumber(new int[5]{1,1,4,2,2}));
            // var x = SingleNumber2(new int[6]{1,2,1,3,2,5});
            // Console.WriteLine($"{x[0]},{x[1]}");
        }

        static int SingleNumber(int[] nums) {
            HashSet<int> numbers = new HashSet<int>();
            foreach(int n in nums){
                if(!numbers.Contains(n)){
                    numbers.Add(n);
                }
                else{
                    numbers.Remove(n);
                }
            }
            return numbers.First();
        }
        static int[] SingleNumberXor(int[] nums){
            int xor= 0;
        foreach(int n in nums)
        {
            xor ^= n;
        }
        
        //get right most sign bit
        int rightMostOneBit = xor & (-xor);
        
        //group numbers by rightMostOneBit
        int num = 0;
        foreach(int n in nums)
        {
            //if((n & rightMostOneBit) == 0)
            if((n & rightMostOneBit) != 0)
                num ^= n;
        }
        
        return new int[]{ num, xor ^ num };
        }
        static int[] SingleNumber2(int[] nums){
            var result = new int[2];
            var hs = new HashSet<int>();
            foreach(int n in nums){
                if (hs.Contains(n))
                    hs.Remove(n);
                else
                    hs.Add(n);
            }
            
            var index = 0;
            foreach(int n in hs){
                result[index++]=n;
            }
            return result;
        }
    }
}
