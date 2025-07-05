public class Relation
{
    // This method should be overridden or mocked in testing.
    public virtual bool Knows(int a, int b)
    {
        throw new NotImplementedException();
    }
}

public class Solution : Relation
{
    private int numberOfPeople;
    private readonly Dictionary<(int, int), bool> cache = new();

    public override bool Knows(int a, int b)
    {
        var key = (a, b);
        if (!cache.ContainsKey(key))
        {
            cache[key] = base.Knows(a, b);
        }
        return cache[key];
    }

    public int FindCelebrity(int n)
    {
        numberOfPeople = n;
        int celebrityCandidate = 0;

        for (int i = 0; i < n; i++)
        {
            if (Knows(celebrityCandidate, i))
            {
                celebrityCandidate = i;
            }
        }

        return IsCelebrity(celebrityCandidate) ? celebrityCandidate : -1;
    }

    private bool IsCelebrity(int i)
    {
        for (int j = 0; j < numberOfPeople; j++)
        {
            if (i == j) continue;
            if (Knows(i, j) || !Knows(j, i))
            {
                return false;
            }
        }
        return true;
    }
}
