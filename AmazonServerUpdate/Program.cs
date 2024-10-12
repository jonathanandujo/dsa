using System;
using System.Collections.Generic;

namespace AmazonServerUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var cluster = new int[5][];
            cluster[0]= new int[5]{0,0,0,0,0};
            cluster[1]= new int[5]{0,0,0,0,0};
            cluster[2]= new int[5]{0,0,0,0,0};
            cluster[3]= new int[5]{0,0,0,0,0};
            cluster[4]= new int[5]{0,0,0,0,0};
            Console.WriteLine(NumberOfDays(cluster));
        }

        // Runtime O(N) Space O(N)
        private static int NumberOfDays(int[][] cluster){
            int days =0;
            bool foundOlder = false;
            bool foundUpdated = false;
            do{
                //restart to next day
                foundOlder=false;
                foundUpdated = false;
                var storage = new List<Tuple<int,int>>();

                for (int i=0; i<cluster.Length;i++){
                    for (int j=0; j<cluster[i].Length;j++){
                        if(cluster[i][j]==1){
                            //to validate that we have at least one server with update
                            foundUpdated = true;
                            //insert index to be update at the end of the day
                            storage.Add(new Tuple<int, int>(i,j));
                        }
                        else {
                            //found a server without update to be updated
                            foundOlder = true;
                        }
                    }
                }
                if(foundOlder && foundUpdated){
                    foreach(Tuple<int,int> vals in storage){
                        //update all neighbours here
                        update(cluster,vals.Item1-1,vals.Item2);
                        update(cluster,vals.Item1+1,vals.Item2);
                        update(cluster,vals.Item1,vals.Item2-1);
                        update(cluster,vals.Item1,vals.Item2+1);
                    }

                    days++;
                }
            } while(foundOlder && foundUpdated);
            return days;
        }
        private static void update(int[][]cluster, int i, int j){
            //validate indexes
            if(i<0 || j <0 || i>=cluster.Length || j>=cluster[i].Length)
                return;

            cluster[i][j]=1;
        }
    }
}
