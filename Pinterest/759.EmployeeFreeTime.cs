using System;
using System.Collections.Generic;

public class Interval
{
    public int start;
    public int end;

    public Interval(int s, int e)
    {
        start = s;
        end = e;
    }
}

public class Job
{
    public int eid;
    public int index;

    public Job(int e, int i)
    {
        eid = e;
        index = i;
    }
}

public class Solution
{
    public IList<Interval> EmployeeFreeTime(IList<IList<Interval>> avails)
    {
        var result = new List<Interval>();
        var comparer = Comparer<Job>.Create((a, b) =>
            avails[a.eid][a.index].start.CompareTo(avails[b.eid][b.index].start));

        var pq = new PriorityQueue<Job, Job>(comparer);
        int ei = 0;
        int anchor = int.MaxValue;

        foreach (var employee in avails)
        {
            pq.Enqueue(new Job(ei++, 0), new Job(ei - 1, 0));
            anchor = Math.Min(anchor, employee[0].start);
        }

        while (pq.Count > 0)
        {
            var job = pq.Dequeue();
            var iv = avails[job.eid][job.index];

            if (anchor < iv.start)
                result.Add(new Interval(anchor, iv.start));

            anchor = Math.Max(anchor, iv.end);

            job.index++;
            if (job.index < avails[job.eid].Count)
                pq.Enqueue(job, job);
        }

        return result;
    }
}
