using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReorganizeStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ReorganizeString("aabccee"));
        }

        public static string ReorganizeString(string S) {
            // save char and frequency
            var storage = new Dictionary<char,int>();
            foreach(char c in S){
                if(storage.ContainsKey(c))
                    storage[c]++;
                else
                    storage.Add(c,1);
            }
            
            var list = storage.Select(x => new int[]{(int) x.Key, x.Value}).ToList();
            list.Sort((x, y) => -x[1].CompareTo(y[1]));

            if (list[0][1] > (S.Length + 1) / 2) 
                return ""; 
        
            var result = new char[S.Length];
            int index = 0;
            foreach(var elem in list) 
            {
                while (elem[1] > 0) 
                {
                    if (index >= result.Length) 
                        index = 1;
                    
                    result[index] = (char) elem[0];
                    index += 2;
                    elem[1]--;
                }
            }
            
            return new String(result);
        }
    }
}
