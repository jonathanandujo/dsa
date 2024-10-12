using System;

namespace FirstUniqueCharacter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FirstUniqueChar("jpqrstuakbcdefghijklmlmnonopqrstuvbcdefghiartsv"));
        }

        private static char FirstUniqueChar(string s){
            int[] storage = new int[256];
            foreach(char c in s)
                storage[c]++;
            foreach(char c in s)
                if(storage[c]==1)
                    return c;
            return '\0';
        }
    }
}
