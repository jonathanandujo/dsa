using System;
using System.Text;

namespace SortString
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SortStringInput("BABABA"));
            Console.WriteLine(SortStringInput("48856AASVDWAEADSVWAER8VC45ESASZ901575"));
        }
        private static string SortStringInput(String input){
            var storage = new int[256];
            foreach(char c in input){
                storage[c]++;
            }
            var result = new StringBuilder();
            for(int i = 0; i < storage.Length; i++){
                for(int j = 1; j <= storage[i]; j++) {
                    result.Append((char)(i));
                }
            }
            return result.ToString();
        }
    }
}
