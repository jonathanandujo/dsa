using System;
using System.Collections.Generic;
using System.Text;

namespace ReverseVowels
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(ReverseVowels("This Text Is Some Expensive"));
        }
        

        static string ReverseVowels(string s) {
            var result = new StringBuilder();
            Stack<char> vowels = new Stack<char>();
            var sUpper = s.ToUpper();
            for(int i=0; i<s.Length;i++){
                if(sUpper[i]=='A' || sUpper[i]=='E' ||sUpper[i]=='I' ||sUpper[i]=='O' ||sUpper[i]=='U')
                    vowels.Push(s[i]);
            }
            for(int i=0; i<s.Length;i++){
                if(sUpper[i]=='A' || sUpper[i]=='E' ||sUpper[i]=='I' ||sUpper[i]=='O' ||sUpper[i]=='U')
                    result.Append(vowels.Pop());
                else
                    result.Append(s[i]);
            }
            return result.ToString();
        }
    }
}
