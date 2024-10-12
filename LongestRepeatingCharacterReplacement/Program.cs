using System;

namespace LongestRepeatingCharacterReplacement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CharacterReplacement("AABABACAAA",3));
        }

        private static int CharacterReplacement(string s, int k) {
            int len = s.Length;
            int[] storage = new int[26];
            int tmpMax=0;
            int result=0;
            int start=0;
            for(int i=0; i<len;i++){
                storage[s[i]-'A']++;
                int current = storage[s[i]-'A'];
                tmpMax = Math.Max(tmpMax, current);
                
                while(i-start-tmpMax+1 >k){
                    storage[s[start]-'A']--;
                    start++;
                }
                
                result = Math.Max(tmpMax, i-start+1);
            }
            
            return result;
        }
    }
}
