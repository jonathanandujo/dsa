using System;
using System.Collections.Generic;

namespace LengthOfLongestSubstring
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MaxLen("abcdefgawer"));
        }
        private static int MaxLen(string s){
            if (s.Length <= 1) return s.Length;
            int left =0;
            int right =0;
            int max =0;
            bool existDuplicated = false;
            // save char and frequency
            var storage = new Dictionary<char,int>();
            while(right<s.Length){
                char c = s[right];
                if(!storage.ContainsKey(c))
                    storage.Add(c,0);
                storage[c]++;
                if(storage[c]>1)
                    existDuplicated = true;
                while(existDuplicated){
                    char cleft = s[left];
                    storage[cleft]--;
                    if(storage[cleft]==1)
                        existDuplicated = false;
                    left++;
                }
                right++;
                max = Math.Max(max,right-left);
            }
            return max;
        }
    }
}