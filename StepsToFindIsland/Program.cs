using System;

namespace StepsToFindIsland
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = new int[][] {new int[]{0,0,0,0,0}, // j B
                                      new int[]{0,0,0,0,0}, // j B
                                      new int[]{0,0,0,0,1}, // j B
                                      new int[]{0,0,1,0,0},
                                      new int[]{0,0,0,0,0},
                                      };
            Console.WriteLine(Steps(matrix,4,3));
            Console.WriteLine(Steps(matrix,2,0));
            Console.WriteLine(Steps(matrix,0,2));
            Console.WriteLine(Steps(matrix,4,2));
            Console.WriteLine(Steps(matrix,2,4));
        }
        private static int Steps(int[][] matrix, int a, int b){
            int min = int.MaxValue;
            for (int i = 0; i < matrix.Length; i++){
                for (int j = 0; j < matrix[i].Length; j++){
                    if(matrix[i][j] == 1){
                        min = Math.Min(min, Math.Abs(i-a) + Math.Abs(j-b) );
                    }
                }
            }
            return min;
        }
    }
}
