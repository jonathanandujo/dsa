using System;
using System.Text;

namespace BreakAPalindrome
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(BreakPalindrome("aca"));
        }

        private static string BreakPalindrome(string palindrome) {
            var len = palindrome.Length;
            if (len == 1)
                return "";
            var arr = palindrome.ToCharArray();
            for(int i =0; i<len/2; i++){
                if(arr[i]!='a'){
                    arr[i]='a';
                    return new string(arr);
                }
                
            }
            arr[len-1] = 'b';
            return new string(arr);
        }
    }
}
