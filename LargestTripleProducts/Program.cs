using System;

namespace LargestTripleProducts
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = findMaxProduct(new int[5]{1,2,3,4,5});
            foreach(int i in result){ Console.WriteLine(i);}
            Console.WriteLine();
            result = findMaxProduct(new int[5]{2,1,2,1,2});
            foreach(int i in result){ Console.WriteLine(i);}
            Console.WriteLine();
            result = findMaxProduct(new int[9]{1,2,3,4,5,6,7,8,9});
            foreach(int i in result){ Console.WriteLine(i);}
        }

         private static int[] findMaxProduct(int[] arr) {
            //validate input
            if(arr.Length == 0) return new int[0];
            if(arr.Length == 1) return new int[1]{-1};
            if(arr.Length == 2) return new int[2]{-1,-1};
            
            var result = new int[arr.Length];
            var storage = new int[4];
            
            for(int i =0; i< arr.Length; i++){
            storage[0]=arr[i];
            Array.Sort(storage);
            if(i<3){
                result[i]=-1;
            }
            else{
                result[i]=storage[3]*storage[2]*storage[1];
            }
            }
            return result;
        }
    }
}
