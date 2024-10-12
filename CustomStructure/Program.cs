using System;

namespace CustomStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var x = new MyStructure(5);
            Console.WriteLine(x.GetAverage());
            x.Add(1);
            Console.WriteLine(x.GetAverage());
            x.Add(2);
            x.Add(3);
            x.Add(4);
            x.Add(5);
            Console.WriteLine(x.GetAverage());
            x.Add(5);
            x.Add(5);
            x.Add(5);
            x.Add(5);
            Console.WriteLine(x.GetAverage()); //5
        }
    }

    public class MyStructure{
        private int index;
        private int[] storage;
        private int len;
        public MyStructure(int size){
            this.storage = new int[size];
        }

        public void Add(int val){
            if(len < storage.Length)
                len++;
            
            storage[index++] = val;

            // reset index
            if (index >= storage.Length)
                index = 0;
        }

        public double GetAverage(){
            if (len == 0)
                return 0;

            var sum =0;
            for(int i =0; i<len; i++){
                sum+= storage[i];
            }
            return sum/len;
        }
    }
}
