using System;
using System.Collections.Generic;
using System.Text;

public class TrieNode
{
    public Dictionary<char, TrieNode> Children = new();
    public Dictionary<string, int> Sentences = new();
}

public class AutocompleteSystem
{
    private readonly TrieNode root;
    private TrieNode currNode;
    private readonly TrieNode dead;
    private readonly StringBuilder currSentence;

    public AutocompleteSystem(string[] sentences, int[] times)
    {
        root = new TrieNode();
        for (int i = 0; i < sentences.Length; i++)
        {
            AddToTrie(sentences[i], times[i]);
        }

        currSentence = new StringBuilder();
        currNode = root;
        dead = new TrieNode(); // Sentinel node for dead-end paths
    }

    public IList<string> Input(char c)
    {
        if (c == '#')
        {
            AddToTrie(currSentence.ToString(), 1);
            currSentence.Clear();
            currNode = root;
            return new List<string>();
        }

        currSentence.Append(c);
        if (!currNode.Children.ContainsKey(c))
        {
            currNode = dead;
            return new List<string>();
        }

        currNode = currNode.Children[c];

        var heap = new PriorityQueue<string, SentenceComparer>();

        foreach (var kvp in currNode.Sentences)
        {
            heap.Enqueue(kvp.Key, new SentenceComparer(kvp.Key, kvp.Value));
            if (heap.Count > 3)
                heap.Dequeue();
        }

        var result = new List<string>();
        while (heap.Count > 0)
        {
            result.Add(heap.Dequeue());
        }

        result.Reverse(); // Since we want highest frequency first
        return result;
    }

    private void AddToTrie(string sentence, int count)
    {
        TrieNode node = root;
        foreach (char c in sentence)
        {
            if (!node.Children.ContainsKey(c))
            {
                node.Children[c] = new TrieNode();
            }

            node = node.Children[c];
            if (!node.Sentences.ContainsKey(sentence))
                node.Sentences[sentence] = 0;

            node.Sentences[sentence] += count;
        }
    }

    private class SentenceComparer : IComparable<SentenceComparer>
    {
        public string Sentence { get; }
        public int Frequency { get; }

        public SentenceComparer(string sentence, int frequency)
        {
            Sentence = sentence;
            Frequency = frequency;
        }

        public int CompareTo(SentenceComparer other)
        {
            if (Frequency != other.Frequency)
                return Frequency - other.Frequency; // Min-heap by frequency
            return string.CompareOrdinal(other.Sentence, Sentence); // Max-heap by lexicographic order
        }
    }
}
