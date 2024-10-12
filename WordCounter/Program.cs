using System;
using System.Collections.Generic;

namespace WordCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Counter("this is a text to take any text to count every word in the text counting");

            foreach(Tuple<string,int> i in x)
                Console.WriteLine($"{i.Item1}-{i.Item2}");

        }
        private static List<Tuple<string,int>> Counter(string input){
            var result = new List<Tuple<string,int>>();
            var arr = input.Split(" ");
            var storage = new Dictionary<string,int>();
            foreach(string s in arr){
                if(storage.ContainsKey(s))
                    storage[s]++;
                else
                    storage.Add(s,1);
            }
            foreach(KeyValuePair<string,int> kp in storage)
                result.Add(new Tuple<string, int>(kp.Key, kp.Value));
            return result;
        }
    }
}
