// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var x = new SortedDictionary<int, int>();
x.Add(4, 2);
x.Add(5, 1);
x.Add(1, 5);
x.Add(2, 4);
x.Add(3, 3);

foreach (var item in x)
{
    Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
}

// PriorityQueue<char,int> pq = new();
// pq.Enqueue('c', 3);
// pq.Enqueue('e', 5);
// pq.Enqueue('b', 2);
// pq.Enqueue('x', 16);
// pq.Enqueue('a', 1);
// pq.Enqueue('f', 4);

// while (pq.Count > 0)
// {
//     var item = pq.Dequeue();
//     Console.WriteLine($"Item: {item}");
// }