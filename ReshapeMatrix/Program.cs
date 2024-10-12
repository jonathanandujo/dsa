using System;

namespace ReshapeMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var arr = new int[][]{
                new int[] {1,2},
                new int[] {3,4}
            };
            var result = MatrixReshape(arr,1,4);
            var result2 = MatrixReshape(arr,2,4);
            Console.ReadLine();
        }
        public static int[][] MatrixReshape(int[][] mat, int r, int c) {
            if(mat.Length == 0)
                return mat;
            int m = mat.Length;
            int n = mat[0].Length;
            if(m*n != r*c)
                return mat;
            
            int[][] result = new int[r][];
            for(int i =0; i<r;i++){
                result[i] = new int[c];
            }
            for(int i =0; i<r*c;i++){
                result[i/c][i%c] = mat[i/n][i%n];
            }
            return result;
        }
    }
}
