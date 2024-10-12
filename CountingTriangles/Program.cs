using System;
using System.Collections.Generic;

namespace CountingTriangles
{
    class Program
    {
        static void Main(string[] args) {
            var arr = new int[3,3]{{5,8,7},{5,7,0},{18,5,6}};
            Console.WriteLine(countDistinctTriangles(arr));
            // Call countDistinctTriangles() with test cases here
        }

        private static int countDistinctTriangles(int[,] arr) {
            var storage = new HashSet<string>();
            for(int i =0; i<arr.Length/3;i++){
            storage.Add(sorted(arr[i,0],arr[i,1],arr[i,2]));
            }
            foreach(string i in storage){
                Console.WriteLine(i);
            }
            return storage.Count;
        }
        
        private static string sorted(int a, int b, int c){
            if(a>b) swap(ref a,ref b);
            if(a>c) swap(ref a,ref c);
            if(b>c) swap(ref b,ref c);
            return $"{a}.{b}.{c}";
        }
        private static void swap(ref int a,ref int b){
            //1 2
            //3 2
            //3 1
            //2 1
            a += b;
            b=a-b;
            a=a-b;
        }
    }
}
