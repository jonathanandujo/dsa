using System;

namespace Spiral
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var a = generateSpiral(9);
            for(int i =0;i<a.Length;i++){
                for (int j=0;j<a[i].Length;j++){
                    Console.Write("{0} ",a[i][j]);
                }
                Console.WriteLine();
            }
        }
        static int[][] generateSpiral (int n){
            var matrix = new int[n][];
            for(int i=0;i<n;i++)
                matrix[i]=new int[n];
            
            int el=(n*n)+1;
            int x=0,y=0;
            int direction = 0;
            int[] dx = new int[]{1,0,-1,0};
            int[] dy = new int[]{0,1,0,-1};
            while(el>1){
                el--;
                matrix[y][x]=el;
                if(x+dx[direction] < 0 || x+dx[direction] >= n || y+dy[direction]<0 || y+dy[direction]>=n || matrix[y+dy[direction]][x+dx[direction]]>0)
                    direction=(direction+1)%4;
                
                x+=dx[direction];
                y+=dy[direction];
            }
            
            return matrix;
        }
    }
}
