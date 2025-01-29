List<LogEntry> logs = new();
logs.Add(new LogEntry("C", "w"));
logs.Add(new LogEntry("A", "a"));
logs.Add(new LogEntry("A", "b"));
logs.Add(new LogEntry("C", "x"));
logs.Add(new LogEntry("B", "b"));
logs.Add(new LogEntry("A", "c"));
logs.Add(new LogEntry("A", "d"));
logs.Add(new LogEntry("C", "y"));
logs.Add(new LogEntry("B", "c"));
logs.Add(new LogEntry("A", "e"));
logs.Add(new LogEntry("C", "z"));
logs.Add(new LogEntry("B", "d"));
logs.Add(new LogEntry("B", "a"));
logs.Add(new LogEntry("B", "e"));
Console.WriteLine(PopularSequence(logs));

string PopularSequence(List<LogEntry> logs)
{
    Dictionary<string, List<string>> customerPages = new();
    Dictionary<string, int> sequenceCount = new();
    foreach (LogEntry log in logs)
    {
        if (!customerPages.ContainsKey(log.customerID))
            customerPages.Add(log.customerID, new());
        customerPages[log.customerID].Add(log.pageID);
    }
    PriorityQueue<string, int> pq = new();
    foreach (var customerData in customerPages)
    {
        List<string> pages = customerData.Value;
        for (int i = 0; i < pages.Count - 2; i++)
        {
            string sequence = $"{pages[i]} {pages[i + 1]} {pages[i + 2]}";
            if (!sequenceCount.ContainsKey(sequence))
                sequenceCount.Add(sequence, 0);
            sequenceCount[sequence]++;
            pq.Enqueue(sequence, -sequenceCount[sequence]);
        }
    }
    return pq.Dequeue();
}


class LogEntry {
    public string customerID;
    public string pageID;
    public LogEntry(string customerID, string pageID) {
        this.customerID = customerID;
        this.pageID = pageID;
    }
 }
