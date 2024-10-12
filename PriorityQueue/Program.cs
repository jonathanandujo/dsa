using System;

namespace PriorityQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            var pq = new StructurePriorityQueue<string>();
            var persistent = new PriorityQueueDto<string>(5, "world");
            pq.Add(persistent);
            Console.WriteLine(pq.Peek().ToString());
            pq.Add(new PriorityQueueDto<string>(4, "hello"));
            Console.WriteLine(pq.Peek().ToString());
            pq.Add(new PriorityQueueDto<string>(6, "high"));
            Console.WriteLine(pq.Peek().ToString());
            pq.Add(new PriorityQueueDto<string>(7, "seven"));
            Console.WriteLine(pq.Get().ToString());
            Console.WriteLine(pq.Peek().ToString());
            pq.Remove(persistent);
            Console.WriteLine(pq.Peek().ToString());
            pq.Add(new PriorityQueueDto<string>(9, "nine"));
            pq.Add(new PriorityQueueDto<string>(7, "seven"));
            pq.Add(new PriorityQueueDto<string>(1, "one"));
            pq.Add(new PriorityQueueDto<string>(2, "two"));
            pq.Add(new PriorityQueueDto<string>(3, "three"));
            while(!pq.IsEmpty()){
                Console.WriteLine(pq.Get().ToString());
            }
        }
    }
}
