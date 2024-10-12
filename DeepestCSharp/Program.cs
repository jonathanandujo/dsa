using System;

namespace DeepestCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Deepest(0));
        }
        private static int Deepest(int counter){
            if(counter == 15995)
                return counter;
            counter++;
            var arrmem = new int[50000];
            for(int i=0; i<30000;i++)
                arrmem[i] = i*2;
            return Deepest(counter);
        }
    }
}
