using System;
using System.Collections.Generic;

namespace JewelsAndStones
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(NumJewelsInStones("AB","ABcA"));
        }

        static int NumJewelsInStones(string J, string S) {
            int count = 0;
            var hs = new HashSet<int>();
            foreach(char c in J){
                hs.Add(c);
            }
            foreach (char c in S){
                if(hs.Contains(c))
                    count++;
            }
            return count;
        }

        static void ReverseString(char[] s) {
            Array.Reverse(s);
            for(int i=0; i<s.Length; i++){
                Console.Write(s[i]);
            }
        }
    }
}
