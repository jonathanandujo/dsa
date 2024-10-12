using System;

namespace LongestPrefix
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(LongestCommonPrefix(new string[3]{"person","per","personal"}));
            Console.WriteLine(LongestCommonPrefix(new string[5]{"","","","",""}));
            Console.WriteLine(LongestCommonPrefix(new string[2]{"flow","flowers"}));
            Console.WriteLine(LongestCommonPrefix(new string[2]{"flow",""}));
            Console.WriteLine(LongestCommonPrefix(new string[2]{"flom","sts"}));
            Console.WriteLine(LongestCommonPrefix(new string[3]{"personal","personal","personal"}));
            Console.WriteLine(LongestCommonPrefix(new string[1]{"personal"}));
        }
        static string LongestCommonPrefix(string[] strs) {
            if(strs.Length<1) return string.Empty;
            var firstWord = strs[0];
            for (int i = 0; i < firstWord.Length; i++){
                for (int j = 1; j < strs.Length; j++){
                    if(i>=strs[j].Length || firstWord[i] != strs[j][i])
                        return firstWord.Substring(0,i);
                }
            }
            return firstWord;
        }
    }
}
