// Plan a round trip between two cities with minimum flight cost.
// From departure city to destination city, the fees are stored in array D[].
// From destination city to departure city, the fees are stored in array R[].
// Both arrays should have equal length, and index corresponds to dates.
Console.WriteLine("Hello, World!");
var D = new int[] { 10, 8, 9, 11, 7 };
var R = new int[] { 8, 8, 10, 7, 9 };

Console.WriteLine(MinCost(D, R));
Console.WriteLine(MinCostCopilot(D, R));
Console.WriteLine(MinCostLC(D, R));

D = new int[] { 10, 50, 19, 11, 7, 10 };
R = new int[] { 18, 28, 10, 47, 9, 10 };

Console.WriteLine(MinCost(D, R));
Console.WriteLine(MinCostCopilot(D, R));
Console.WriteLine(MinCostLC(D, R));

D = new int[] { 10, 50, 19, 11, 7, 1 };
R = new int[] { 18, 28, 10, 47, 9, 1 };

Console.WriteLine(MinCost(D, R));
Console.WriteLine(MinCostCopilot(D, R));
Console.WriteLine(MinCostLC(D, R));

static int MinCostCopilot(int[] D, int[] R)
{
    int n = D.Length;
    int minCost = int.MaxValue;

    // Array to store the minimum return cost from day i to the end
    int[] minReturnCost = new int[n];
    minReturnCost[n - 1] = R[n - 1];

    // Fill the minReturnCost array
    for (int i = n - 2; i >= 0; i--)
    {
        minReturnCost[i] = Math.Min(R[i], minReturnCost[i + 1]);
    }

    // Calculate the minimum round trip cost
    for (int i = 0; i < n; i++)
    {
        int cost = D[i] + minReturnCost[i];
        if (cost < minCost)
        {
            minCost = cost;
        }
    }

    return minCost;
}

static int MinCost(int[] D, int[] R)
{
    int minCost = int.MaxValue;
    var minReturnCost = new int[R.Length];
    int maxNumber = int.MaxValue;

    for (int i = R.Length - 1; i >= 0; i--)
    {
        minReturnCost[i] = Math.Min(R[i], maxNumber);
        maxNumber = minReturnCost[i];
    }
    for (int i = 0; i < D.Length; i++)
    {
        minCost = Math.Min(D[i] + minReturnCost[i], minCost);
    }

    return minCost;
}

static int MinCostLC(int[] D, int[] R)
{
    if (D.Length <= 1 || R.Length <= 1)
        return -1;

    int minSofar = R[^1];
    int minCost = int.MaxValue;
    for(int i = 2; i <= D.Length; i++)
    {
        int cost = D[^i] + minSofar;
        minCost = Math.Min(minCost, cost);
        minSofar = Math.Min(minSofar, R[^i]);
    }

    return minCost;
}