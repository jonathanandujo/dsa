using System;
using System.Collections.Generic;

namespace AmazonItemsInContainers
{
    class Program
    {
        static void Main(string[] args)
        {
            var l = numberOfItems("|**|*|*", new List<int>(){1,1}, new List<int>(){5,6});
            foreach(int i in l){
                Console.WriteLine(i);
            }
        }
        static List<int> numberOfItems(string s, List<int> startIndices, List<int> endIndices){
            var result = new List<int>();
            for(int i =0; i<startIndices.Count;i++){
                bool startCompartment = false;
                int count = 0;
                int tmp = 0;
                for(int j = startIndices[i]; j <= endIndices[i]; j++) {
                    if(startCompartment){
                        // If found close comparment
                        if(s[j-1] == '|')
                            count=tmp;
                        else
                            tmp++;
                    }
                    else if(s[j-1] == '|') {
                        startCompartment = true;
                    }
                }
                result.Add(count);
            }
            return result;
        }
    }
}
