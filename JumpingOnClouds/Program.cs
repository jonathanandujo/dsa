using System;

namespace JumpingOnClouds
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(jumpingOnClouds(new int[]{0,0,1,0,0,1,0}));
        }
        
        private static int jumpingOnClouds(int[] c) {
            var jumps =0;
            for(int i =0;i<c.Length-1;i++){
                if(c.Length>i+2 && c[i+2]==0){
                    i++;
                }
                jumps++;
            }
            return jumps;
        }
    }
}
