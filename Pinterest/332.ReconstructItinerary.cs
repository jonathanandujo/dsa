public class Solution
{
    public IList<string> FindItinerary(IList<IList<string>> tickets)
    {
        // Graph: departure -> min-heap of arrivals
        var graph = new Dictionary<string, PriorityQueue<string, string>>();

        foreach (var ticket in tickets)
        {
            string from = ticket[0], to = ticket[1];
            if (!graph.ContainsKey(from))
                graph[from] = new PriorityQueue<string, string>();

            graph[from].Enqueue(to, to);
        }

        var result = new LinkedList<string>();
        DFS("JFK", graph, result);
        return new List<string>(result);
    }

    private void DFS(string source, Dictionary<string, PriorityQueue<string, string>> graph, LinkedList<string> result)
    {
        if (graph.TryGetValue(source, out var destinations))
        {
            while (destinations.Count > 0)
            {
                var next = destinations.Dequeue();
                DFS(next, graph, result);
            }
        }

        // Add to front to avoid reversing at the end
        result.AddFirst(source);
    }
}
