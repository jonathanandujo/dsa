using System;

namespace PossibleSumInArray
{
    class Program
    {
        static int times =0;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(solution3(300, new int[25]{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,3,3,3,3,1}));
            Console.WriteLine(times);
            times=0;
            Console.WriteLine(solution3(10, new int[3]{1,4,7}));
            Console.WriteLine(times);
            times=0;
            Console.WriteLine(solution3(10, new int[5]{5,3,3,4,2}));
            Console.WriteLine(times);
            times=0;
            Console.WriteLine(solution3(10, new int[5]{5,2,3,4,2}));
            Console.WriteLine(times);
            times=0;
            Console.WriteLine(solution3(8, new int[5]{1,2,3,4,5}));
            Console.WriteLine(times);
        }

        private static bool solution3(int t, int[] input_arr) {
		    return recSolution(input_arr,t, input_arr.Length);
        }
        
        static bool recSolution(int[] input, int sum, int len)
        {
            times++;
            if (sum == 0)
                return true;
            if(len == 0)
                return false;
            
            if (input[len - 1] > sum)
                return recSolution(input, sum, len-1);
    
            return recSolution(input, sum, len-1) || recSolutionB(input, sum - input[len - 1], len-1);
        }
        static bool recSolutionB(int[] input, int sum, int len)
        {
            times++;
            if (sum == 0)
                return true;
            if(len == 0)
                return false;
            
            if (input[len - 1] > sum)
                return recSolutionB(input, sum, len-1);
    
            return recSolutionB(input, sum, len-1) || recSolutionB(input, sum - input[len - 1], len-1);
        }
    }
}
