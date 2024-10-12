using System;
using System.Text;

namespace StringCompressionInt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Compress(new char[] {'a','a','b','b','c','c','c'}));
        }

        private static int Compress(char[] chars) {
            if (chars.Length <= 2)
                return chars.Length;
            
            var compressed = new StringBuilder();
            char lastChar = chars[0];
            int count = 1;
            for(int i=1; i<=chars.Length;i++){
                if (i==chars.Length || chars[i] != lastChar){
                    compressed.Append(lastChar);
                    if(count > 1)
                        compressed.Append(count);
                    count=1;
                    if (i<chars.Length)
                        lastChar = chars[i];
                }
                else
                    count++;
            }
            return compressed.Length;
        }
    }
}
