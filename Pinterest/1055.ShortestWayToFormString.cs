class Solution {
    public int ShortestWay(string source, string target) {

        // Next Occurrence of Character after Index
        int[,] nextOccurrence = new int[source.Length, 26];

        // Base Case
        for (int c = 0; c < 26; c++) {
            nextOccurrence[source.Length - 1, c] = -1;
        }
        nextOccurrence[source.Length - 1, source[source.Length - 1] - 'a'] = source.Length - 1;

        // Fill using recurrence relation
        for (int idx = source.Length - 2; idx >= 0; idx--) {
            for (int c = 0; c < 26; c++) {
                nextOccurrence[idx, c] = nextOccurrence[idx + 1, c];
            }
            nextOccurrence[idx, source[idx] - 'a'] = idx;
        }

        // Pointer to the current index in source
        int sourceIterator = 0;

        // Number of times we need to iterate through source
        int count = 1;

        // Find all characters of target in source
        foreach (char c in target) {

            // If the character is not present in source
            if (nextOccurrence[0, c - 'a'] == -1) {
                return -1;
            }

            // If we have reached the end of source, or the character is not in
            // source after source_iterator, loop back to beginning
            if (sourceIterator == source.Length || nextOccurrence[sourceIterator, c - 'a'] == -1) {
                count++;
                sourceIterator = 0;
            }

            // Next occurrence of the character in source after source_iterator
            sourceIterator = nextOccurrence[sourceIterator, c - 'a'] + 1;
        }

        // Return the number of times we need to iterate through source
        return count;
    }
}