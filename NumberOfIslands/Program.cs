using System;

namespace NumberOfIslands
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            /*
            [[0,0,0,0,0]]
            */
            var world = new int[5][];
            world[0]=new int[]{0,0,0,0,1};
            world[1]=new int[]{0,0,1,0,0};
            world[2]=new int[]{0,1,1,1,0};
            world[3]=new int[]{0,0,1,0,0};
            world[4]=new int[]{0,1,0,0,0};
            Console.WriteLine(NumberOfIslands(world));
        }
        static int NumberOfIslands(int[][] world){
            var count = 0;
            for(int i=0; i<world.Length;i++){
                for(int j=0; j<world[i].Length;j++){
                    if(world[i][j]==1){
                        count++;
                        ConvertInSand(world,i,j);
                    }
                }
            }
            return count;
        }
        static void ConvertInSand(int[][] world, int i, int j){
            //valid if the neighbour has sand
            if( i<0 || i>=world.Length || j<0 || j >= world[i].Length || world[i][j]==0)
                return;
            world[i][j]=0;
            ConvertInSand(world,i-1,j-1);
            ConvertInSand(world,i-1,j);
            ConvertInSand(world,i-1,j+1);
            ConvertInSand(world,i,j-1);
            ConvertInSand(world,i,j+1);
            ConvertInSand(world,i+1,j-1);
            ConvertInSand(world,i+1,j);
            ConvertInSand(world,i+1,j+1);
        }
    }
}
