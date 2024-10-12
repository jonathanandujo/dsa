using System;

namespace RotateMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            //var matrix = new int[][] {new int[] {1,2}, new int[]{3,4}};
            var matrix = new int[][] {new int[] {1,2,3}, new int[]{4,5,6}, new int[] {7,8,9} };
            //var matrix = new int[][] {new int[] {1,2,3,4,5}, new int[]{6,7,8,9,10}, new int[] {11,12,13,14,15}, new int[] {16,17,18,19,20}, new int[] {21,22,23,24,25} };
            //var matrix = new int[][] {new int[] {1,2,3,4}, new int[]{5,6,7,8}, new int[] {9,10,11,12}, new int[] {13,14,15,16}};
            ShowMatrix(matrix);
            Rotate(matrix);
            ShowMatrix(matrix);
            Rotate(matrix);
            ShowMatrix(matrix);
            Rotate(matrix);
            ShowMatrix(matrix);
            Rotate(matrix);
            ShowMatrix(matrix);
        }

        private static void Rotate(int[][] matrix){
            var len = matrix.Length;
            for (int i =0; i<len; i++){
                for(int j=i+1; j<len; j++){
                    SwapValue(matrix,new int[]{i,j}, new int[] {j,i});
                }
            }

            for (int i =0; i<len; i++){
                for(int j=0; j<(len/2); j++){
                    SwapValue(matrix,new int[]{i,j}, new int[]{i, len-1-j});
                }
            }
        }

        private static void SwapValue(int[][] matrix, int[] a, int[] b){
            // int tmp = matrix[a[0]][a[1]];
            // matrix[a[0]][a[1]] = matrix[b[0]][b[1]];
            // matrix[b[0]][b[1]] = tmp;
            matrix[a[0]][a[1]] += matrix[b[0]][b[1]];
            matrix[b[0]][b[1]] = matrix[a[0]][a[1]] - matrix[b[0]][b[1]];
            matrix[a[0]][a[1]] = matrix[a[0]][a[1]] - matrix[b[0]][b[1]];
        }

        private static void ShowMatrix(int[][] matrix){
            if(matrix != null)
                foreach(int[] i in matrix){
                    foreach(int j in i){
                        Console.Write($" {j} ");
                    }
                    Console.WriteLine();
                }
            Console.WriteLine();
        }
    }
}
