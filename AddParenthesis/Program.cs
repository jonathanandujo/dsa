using System;
using System.Text;

namespace AddParenthesis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Parenthesis("))())"));
            Console.WriteLine(Parenthesis("))(())"));
        }
        private static string Parenthesis(string input){
            int opened = 0, closed = 0;
            var result = new StringBuilder();
            foreach(char c in input){
                if( c == '(')
                    opened += 1;
                else
                    closed += 1;

                if(closed > opened){
                    result.Append('(');
                    opened += 1;
                }

                result.Append(c);
            }

            while (opened > closed){
                result.Append(')');
                closed += 1;
            }

            return result.ToString();
        }
    }
}
