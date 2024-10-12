using System;

namespace CountNndSay
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CountnndSay(1));
            Console.WriteLine(CountnndSay(2));
            Console.WriteLine(CountnndSay(3));
            Console.WriteLine(CountnndSay(4));
            Console.WriteLine(CountnndSay(5));
            Console.WriteLine(CountnndSay(6));
            Console.WriteLine(CountnndSay(7));
            Console.WriteLine(CountnndSay(8));
            Console.WriteLine(CountnndSay(9));
            Console.WriteLine(CountnndSay(10));
        }

        private static string CountnndSay(int n)
        {
            string s = "1";
            // Dynamic programming
            for (int i = 1; i < n; i++) {
                string tmp = "";
                int counter = 1;
                for (int j = 1; j < s.Length; j++) {
                    if (s[j - 1] == s[j])
                        counter += 1;
                    else  {
                        tmp += $"{counter}{s[j - 1]}";
                        counter = 1;
                    }
                }
                s = $"{tmp}{counter}{s[s.Length-1]}";
            }
            return s;
        }
    }
}
