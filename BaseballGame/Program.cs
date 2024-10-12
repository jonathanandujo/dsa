using System;

namespace BaseballGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CalPoints(new string[]{"5","-2","4","C","D","9","+","+"}));
        }
        private static int CalPoints(string[] ops) {
            var storage = new int[ops.Length];
            var index = 0;
            foreach(string s in ops){
                switch (s){
                    case "+":
                        storage[index]=storage[index-1]+storage[index-2];
                        index++;
                    break;
                        
                    case "D":
                        storage[index]=storage[index-1]*2;
                        index++;
                        break;
                    case "C":
                        index--;
                        break;
                    default:
                        storage[index]=Int32.Parse(s);
                        index++;
                        break;
                }
            }
            int result=0;
            for(int i=0; i<index;i++)
                result+=storage[i];
            return result;
        }
    }
}
