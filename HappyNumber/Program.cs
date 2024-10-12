using System;
using System.Collections.Generic;

namespace HappyNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = (int)Math.Sqrt(2);
            Console.WriteLine("Hello World!");
            Console.WriteLine(IsHappy(9));
            Console.WriteLine(IsHappy(19));
            Console.WriteLine(NumIdenticalPairs(new int[6]{1,2,3,1,1,3}));
        }
        static int NumIdenticalPairs(int[] nums) {
            var result = 0;
            var di = new Dictionary<int,int>();
            for(int i=0; i<nums.Length; i++){
                if(di.ContainsKey(nums[i]))
                        di[nums[i]]++;
                    else
                        di.Add(nums[i],1);
            }
            foreach(KeyValuePair<int,int> obj in di)
            {
                Console.WriteLine(obj);
                if (obj.Value>1){
                    result += (obj.Value*(obj.Value-1))/2;
                }
            }
            return result;
        }
        static bool IsHappy(int n) {
            if(n==1) return true;
            if(n<1) return false;
            HashSet<int> stored = new HashSet<int>();
            while(n>1){
                if(stored.Contains(n))
                    return false;
                stored.Add(n);
                int aux=0;
                while(n != 0)
                {
                    aux += (n%10) *  (n%10);
                    n/=10;
                }
                n=aux;
            }
            return true;
        }
    }
}