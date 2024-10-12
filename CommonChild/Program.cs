using System;

namespace CommonChild
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(commonChild("CASAS","CASAS"));
        }
        
        static int commonChild(string s1, string s2) {
            var grid = new int[s1.Length][];
            for(int i =0;i<s1.Length;i++){
                grid[i] = new int[s2.Length];
            }
            for(int i =0;i<s1.Length;i++){
                for(int j =0;j<s2.Length;j++){
                    if(s1[i]==s2[j])
                        grid[i][j]=grid[i-1<0?0:i-1][j-1<0?0:j-1]+1;
                    else
                        grid[i][j] = Math.Max(grid[i][j-1<0?0:j-1],grid[i-1<0?0:i-1][j]);
                }
            }
            
            return grid[s1.Length-1][s2.Length-1];
        }
    }
}
