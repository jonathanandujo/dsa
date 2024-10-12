using System;
using System.Collections.Generic;
using System.Linq;

namespace ThreeSubsequences
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Subsequences(new int[]{3,5,0,8,8});
            //var x = Subsequences(new int[]{1,1,1,2,2,2,3,3,4,5});
            if (x != null)
                foreach(int[] arr in x){
                    foreach(int i in arr){
                        Console.Write($"{i} ");
                    }
                    Console.WriteLine();
                }
        }

        private static int[][] Subsequences(int[] arr){
            var total = 0;
            foreach(int i in arr){
                total += i;
            }
            if (total % 3 != 0)
                return null;
            
            var subsequences = createSubsequences(arr, new List<int>(), new List<int>(), new List<int>(), total / 3);
            if (subsequences == null) return null;

            return subsequences;
        }

        private static int[][] createSubsequences(int[] arr, List<int> seq1, List<int> seq2, List<int> seq3, int total){
            if (seq1.Sum() == total && seq2.Sum() == total && seq3.Sum() == total) {
                var result = new int[3][];
                result[0] = seq1.ToArray();
                result[1] = seq2.ToArray();
                result[2] = seq3.ToArray();
                return result;
            }
            if (arr.Count() == 0) {
                return null;
            }
            var cloneseq1 = seq1.ToList();
            cloneseq1.Add(arr[0]);
            var subsequences = createSubsequences(SubArray(arr,1,arr.Length-1), cloneseq1, seq2, seq3, total);
            if (subsequences != null && subsequences.Count() == 3) {
                return subsequences;
            }
            var cloneseq2 = seq2.ToList();
            cloneseq2.Add(arr[0]);
            subsequences = createSubsequences(SubArray(arr,1,arr.Length-1), seq1, cloneseq2, seq3, total);
            if (subsequences != null && subsequences.Count() == 3) {
                return subsequences;
            }
            var cloneseq3 = seq3.ToList();
            cloneseq3.Add(arr[0]);
            return createSubsequences(SubArray(arr,1,arr.Length-1), seq1, seq2, cloneseq3, total);
        }

        private static int[] SubArray(int[] arr, int start, int len){
            int[] result = new int[len];
            Array.Copy(arr, start, result, 0, len);
            return result;
        }
    }
}
