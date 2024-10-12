using System;

namespace minCostTickets
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(minimum(new int[6]{1,4,6,7,8,20}, new int[3]{2,7,15}));
            Console.WriteLine(minimum(new int[10]{1,8,15,21,22,23,26,28,29,30}, new int[3]{2,7,15}));
            Console.WriteLine(minimum(new int[12]{1,2,3,4,5,6,7,8,9,10,30,31}, new int[3]{2,7,15}));
        }
        static int minimum(int[] days,int[] costs){
            int n = days.Length;
            if(n==0)
                return 0;
            var max_day = days[n-1];
            var arr= new int[max_day+1];
            int i,j = 0;
            arr[0]=0;
            for(i=1;i<=max_day;i++) {
                if(days[j]==i)
                {
                    if(i<7)
                        arr[i] = Math.Min(Math.Min(costs[0]+arr[i-1],costs[1]),costs[2]);
                    else if(i<30)
                        arr[i] = Math.Min(Math.Min(costs[0]+arr[i-1],costs[1]+arr[i-7]),costs[2]);
                    else
                        arr[i] = Math.Min(Math.Min(costs[0]+arr[i-1],costs[1]+arr[i-7]),costs[2]+arr[i-30]);

                    j++;
                }
                else
                    arr[i] = arr[i-1];
            }
            return arr[max_day];
        }
    }
}
