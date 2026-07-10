Console.WriteLine("Hello, World!");
var cache = new LRUCache(2);
Console.WriteLine("Put 1, value: a");
cache.Put(1, "a");
Console.WriteLine("Put 2, value: b");
cache.Put(2, "b");
Console.WriteLine($"Get 1, value: {cache.Get(1)}"); // returns "a"
Console.WriteLine("Put 3, value: c");
cache.Put(3, "c"); // evicts key 2
Console.WriteLine($"Get 2, value: {cache.Get(2)}"); // returns null (not found)
Console.WriteLine("Put 4, value: d");
cache.Put(4, "d"); // evicts key 1
Console.WriteLine($"Get 1, value: {cache.Get(1)}"); // returns null (not found)
Console.WriteLine($"Get 3, value: {cache.Get(3)}"); // returns "c"
Console.WriteLine($"Get 4, value: {cache.Get(4)}"); // returns "d"
Console.WriteLine($"Get 4, value: {cache.Get(4)}"); // returns "d"
Console.WriteLine($"Get 3, value: {cache.Get(3)}"); // returns "c"

public class LRUCache
{
    private int capacity;
    private Dictionary<int, LinkedListNode<(int key, string value)>> cache;
    private LinkedList<(int key, string value)> lruList;

    public LRUCache(int capacity)
    {
        this.capacity = capacity;
        cache = new Dictionary<int, LinkedListNode<(int key, string value)>>();
        lruList = new LinkedList<(int key, string value)>();
    }

    public string Get(int key)
    {
        if (cache.TryGetValue(key, out var node))
        {
            lruList.Remove(node);
            lruList.AddFirst(node);
            return node.Value.value;
        }
        return null;
    }

    public void Put(int key, string value)
    {
        if (cache.TryGetValue(key, out var node))
        {
            lruList.Remove(node);
        }
        else if (cache.Count >= capacity)
        {
            var lruNode = lruList.Last;
            if (lruNode != null)
            {
                lruList.RemoveLast();
                cache.Remove(lruNode.Value.key);
            }
        }
        var newNode = new LinkedListNode<(int key, string value)>((key, value));
        lruList.AddFirst(newNode);
        cache[key] = newNode;
    }
}


public class LFUCache
{
    private readonly int capacity;
    private int minFrequency;

    private readonly Dictionary<int, LinkedListNode<CacheItem>> keyToNode;
    private readonly Dictionary<int, LinkedList<CacheItem>> frequencyToItems;

    private class CacheItem
    {
        public int Key;
        public string Value;
        public int Frequency;

        public CacheItem(int key, string value)
        {
            Key = key;
            Value = value;
            Frequency = 1;
        }
    }

    public LFUCache(int capacity)
    {
        this.capacity = capacity;
        minFrequency = 0;
        keyToNode = new Dictionary<int, LinkedListNode<CacheItem>>();
        frequencyToItems = new Dictionary<int, LinkedList<CacheItem>>();
    }

    public string Get(int key)
    {
        if (!keyToNode.TryGetValue(key, out var node))
        {
            return null;
        }

        IncreaseFrequency(node);
        return node.Value.Value;
    }

    public void Put(int key, string value)
    {
        if (capacity == 0)
        {
            return;
        }

        if (keyToNode.TryGetValue(key, out var existingNode))
        {
            existingNode.Value.Value = value;
            IncreaseFrequency(existingNode);
            return;
        }

        if (keyToNode.Count >= capacity)
        {
            EvictLeastFrequentlyUsed();
        }

        var newItem = new CacheItem(key, value);

        if (!frequencyToItems.TryGetValue(1, out var listForFrequencyOne))
        {
            listForFrequencyOne = new LinkedList<CacheItem>();
            frequencyToItems[1] = listForFrequencyOne;
        }

        var newNode = listForFrequencyOne.AddFirst(newItem);
        keyToNode[key] = newNode;
        minFrequency = 1;
    }

    private void IncreaseFrequency(LinkedListNode<CacheItem> node)
    {
        var currentItem = node.Value;
        var currentFrequency = currentItem.Frequency;

        var currentList = frequencyToItems[currentFrequency];
        currentList.Remove(node);

        if (currentList.Count == 0)
        {
            frequencyToItems.Remove(currentFrequency);
            if (minFrequency == currentFrequency)
            {
                minFrequency++;
            }
        }

        currentItem.Frequency++;
        var newFrequency = currentItem.Frequency;

        if (!frequencyToItems.TryGetValue(newFrequency, out var newList))
        {
            newList = new LinkedList<CacheItem>();
            frequencyToItems[newFrequency] = newList;
        }

        var newNode = newList.AddFirst(currentItem);
        keyToNode[currentItem.Key] = newNode;
    }

    private void EvictLeastFrequentlyUsed()
    {
        if (!frequencyToItems.TryGetValue(minFrequency, out var list) || list.Count == 0)
        {
            return;
        }

        var nodeToRemove = list.Last;
        if (nodeToRemove == null)
        {
            return;
        }

        list.RemoveLast();
        keyToNode.Remove(nodeToRemove.Value.Key);

        if (list.Count == 0)
        {
            frequencyToItems.Remove(minFrequency);
        }
    }
}