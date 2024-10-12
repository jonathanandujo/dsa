using System;
using System.Collections.Generic;

namespace MaxNumberBalloons
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(MaxNumberOfBalloons("balon"));
        }

        static int MaxNumberOfBalloons(string text) {
            var d = new Dictionary<char,int>();
            var w = new Dictionary<char,int>();
            
            //any word that you want
            var word = "balloon";
            foreach(char c in word){
                if(w.ContainsKey(c))
                    w[c]++;
                else
                    w.Add(c,1);
            }
            foreach(char c in text){
                if(d.ContainsKey(c))
                    d[c]++;
                else
                    d.Add(c,1);
            }
            
            int result=int.MaxValue;
            foreach(KeyValuePair<char,int> c in w){
                if(!d.ContainsKey(c.Key)) return 0;
                result = Math.Min(result,d[c.Key]/c.Value);
            }
            return result;
        }
    }
}
