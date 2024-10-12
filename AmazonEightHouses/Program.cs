using System;

namespace AmazonEightHouses
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = cellCompete(new int[]{1,1,1,0,1,1,1,1},1);
            foreach(int i in result)
                Console.Write($"{i} ");
        }

        private static int[] cellCompete(int[] states, int days)
        {
            for(int i=1; i<=days; i++){
                var houses = new int[states.Length+2];
                houses[0]=0;
                houses[states.Length-1]=0;
                for(int h=0; h<states.Length; h++){
                    houses[h+1]= states[h];
                }
                for(int j=1; j<houses.Length-1; j++){
                    var a = houses[j-1];
                    var b = houses[j+1];
                    states[j-1]= a^b;
                }
            }
            return states;
        }
    }
}
