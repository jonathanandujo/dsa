using System;
using System.Collections.Generic;

public class Solution
{
    private List<int> creditList;

    public int MinTransfers(int[][] transactions)
    {
        var creditMap = new Dictionary<int, int>();

        // Build net balance for each person
        foreach (var t in transactions)
        {
            int from = t[0], to = t[1], amount = t[2];
            if (!creditMap.ContainsKey(from)) creditMap[from] = 0;
            if (!creditMap.ContainsKey(to)) creditMap[to] = 0;

            creditMap[from] += amount;
            creditMap[to] -= amount;
        }

        creditList = new List<int>();
        foreach (var amount in creditMap.Values)
        {
            if (amount != 0)
                creditList.Add(amount);
        }

        int n = creditList.Count;
        int[] memo = new int[1 << n];
        Array.Fill(memo, -1);
        memo[0] = 0;

        return n - Dfs((1 << n) - 1, memo);
    }

    private int Dfs(int totalMask, int[] memo)
    {
        if (memo[totalMask] != -1)
            return memo[totalMask];

        int balanceSum = 0, answer = 0;

        for (int i = 0; i < creditList.Count; i++)
        {
            int curBit = 1 << i;
            if ((totalMask & curBit) != 0)
            {
                balanceSum += creditList[i];
                answer = Math.Max(answer, Dfs(totalMask ^ curBit, memo));
            }
        }

        memo[totalMask] = answer + (balanceSum == 0 ? 1 : 0);
        return memo[totalMask];
    }
}
