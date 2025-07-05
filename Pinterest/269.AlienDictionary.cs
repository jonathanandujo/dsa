using System;
using System.Collections.Generic;
using System.Text;

public class Solution
{
    private Dictionary<char, List<char>> reverseAdjList = new();
    private Dictionary<char, bool> seen = new();
    private StringBuilder output = new();

    public string AlienOrder(string[] words)
    {
        // Step 0: Initialize reverseAdjList with all unique characters
        foreach (var word in words)
        {
            foreach (var c in word)
            {
                if (!reverseAdjList.ContainsKey(c))
                {
                    reverseAdjList[c] = new List<char>();
                }
            }
        }

        // Step 1: Build reverse adjacency list
        for (int i = 0; i < words.Length - 1; i++)
        {
            string word1 = words[i];
            string word2 = words[i + 1];

            // Invalid case: prefix conflict
            if (word1.Length > word2.Length && word1.StartsWith(word2))
            {
                return "";
            }

            for (int j = 0; j < Math.Min(word1.Length, word2.Length); j++)
            {
                if (word1[j] != word2[j])
                {
                    reverseAdjList[word2[j]].Add(word1[j]);
                    break;
                }
            }
        }

        // Step 2: DFS to detect cycles and build output
        foreach (var c in reverseAdjList.Keys)
        {
            if (!Dfs(c))
            {
                return "";
            }
        }

        return output.ToString();
    }

    private bool Dfs(char c)
    {
        if (seen.ContainsKey(c))
        {
            return seen[c]; // false means cycle detected
        }

        seen[c] = false; // mark as visiting (gray)

        foreach (var neighbor in reverseAdjList[c])
        {
            if (!Dfs(neighbor))
            {
                return false;
            }
        }

        seen[c] = true; // mark as visited (black)
        output.Append(c);
        return true;
    }
}
