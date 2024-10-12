using System;
using System.Collections.Generic;

namespace LongestSubstring
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(LengthOfLongestSubstring("pwsljelsilhtsasadfwtsklnlñkczxoñviuñwer"));
            Console.WriteLine(LengthOfLongestSubstring("asdfghjdfghjk25648"));
            Console.WriteLine(LengthOfLongestSubstring("assdfahueso"));
            Console.WriteLine(LengthOfLongestSubstring("pwsljelsilhtsasadfwtsklnlñkczxoñviuñwerqwertyuiopasdfghjqwertyuiopasdfghjklñzxcvbnmsdfghjk"));
        }

        public static int LengthOfLongestSubstring(string s) {
            if (s.Length<=1) return s.Length;
            int left = 0, right = 0, maxLength = 0;
            Dictionary<char,int> table = new Dictionary<char,int>();
            bool existDuplicated = false;
            
            while(right < s.Length)
            {
                char curr = s[right];           
                if(table.ContainsKey(curr))
                {
                    table[curr]++;
                    if(table[curr] > 1)
                        existDuplicated=true;                       
                }             
                else
                    table.Add(curr, 1);
                right++;
                
                while(existDuplicated)
                {
                    char leftChar = s[left];
                    table[leftChar]--;
                    if(table[leftChar] == 1)
                        existDuplicated=false;
                    left++;
                }
                if(maxLength < right-left) maxLength=right-left;
            }
            
            return maxLength;
        }
    }
}
