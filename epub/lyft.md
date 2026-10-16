# Lyft Interview Preparation

## How to Use This Book

For each exercise:

1. Identify the pattern from the problem's constraints and required output.
2. Try to solve it before reading the key insight.
3. Trace the algorithm on a small example.
4. Explain the time and space complexity.
5. Summarize the approach in one interview-ready sentence.

## Contents

- [Sliding Window](#sliding-window)
- [Hashing](#hashing)
- [Graphs](#graphs)
- [Trees](#trees)
- [Trie](#trie)
- [Heaps](#heaps)
- [Data Structure Design](#data-structure-design)
- [Linked Lists](#linked-lists)
- [Backtracking](#backtracking)
- [Dynamic Programming](#dynamic-programming)
- [Intervals](#intervals)
- [Prefix Sum](#prefix-sum)
- [Binary Search](#binary-search)
- [Two Pointers](#two-pointers)
- [Math](#math)
- [Simulation](#simulation)
- [Pattern Recognition Cheat Sheet](#pattern-recognition-cheat-sheet)
- [Last Minute Review](#last-minute-review)

# Sliding Window

## 76. Minimum Window Substring

### Pattern

Variable-size sliding window

### Recognition

- Minimum substring containing all required characters
- Window expands and contracts

### Key Insight

Track the multiplicity of each required character. Expand until the window is valid, then shrink from the left while recording the smallest valid window.

### Complexity

- Time: O(|s| + |t|)
- Space: O(1) for the fixed ASCII alphabet.

### Interview One-Liner

Expand until all required characters are covered, then shrink the window to find the shortest valid substring.

### Solution

```csharp
public class Solution
{
    public string MinWindow(string s, string t)
    {
        if (s.Length < t.Length)
            return "";
        int[] need = new int[128];
        foreach(char c in t)
        {
            need[c]++;
        }
        int required = t.Length;
        int left = 0;
        int bestStart = left;
        int bestLength = int.MaxValue;

        for(int right = 0; right < s.Length; right++)
        {
            char current = s[right];
            if(need[current] > 0)
                required--;
            need[current]--;
            while(required == 0)
            {
                int windowLength = right - left +1;
                if(windowLength < bestLength)
                {
                    bestLength = windowLength;
                    bestStart = left;
                }
                char leftChar = s[left];
                need[leftChar]++;
                if(need[leftChar] > 0)
                    required++;
                left++;
            }
        }
        return bestLength == int.MaxValue ? "" : s.Substring(bestStart, bestLength);
    }
}
```

---

## 3. Longest Substring Without Repeating Characters

### Pattern

Sliding window + hash set

### Recognition

- Longest substring with a uniqueness constraint
- Duplicates invalidate the current window

### Key Insight

Maintain a duplicate-free window. When the right character is already present, remove characters from the left until it can be added.

### Complexity

- Time: O(n)
- Space: O(min(n, alphabet size)).

### Interview One-Liner

Keep a duplicate-free sliding window and update the maximum length as its right edge advances.

### Solution

```csharp
public class Solution {
    public int LengthOfLongestSubstring(string s) {
        HashSet<char> store = new();
        int left = 0;
        int right = 0;
        int maxLen = 0;
        while(right < s.Length)
        {
            if(store.Add(s[right]))
                right++;
            else
                 store.Remove(s[left++]);
            maxLen = Math.Max(maxLen, right - left);
        }
        return maxLen;
    }
}
```

---

# Hashing

## 128. Longest Consecutive Sequence

### Pattern

Hash set

### Recognition

- Consecutive values, regardless of input order
- Expected linear-time target

### Key Insight

Put values in a hash set and start a scan only at values whose predecessor is absent. Count forward from each sequence start.

### Complexity

- Time: O(n) expected
- Space: O(n).

### Interview One-Liner

Start counting only at numbers with no predecessor, so every consecutive run is scanned once.

### Solution

```csharp
public class Solution {
    public int LongestConsecutive(int[] nums) {
        if (nums == null || nums.Length == 0) return 0;

        HashSet<int> numSet = new(nums);
        int maxLength = 0;

        foreach (int num in numSet) {
            if (!numSet.Contains(num - 1)) {
                int currentNum = num;
                int currentStreak = 1;

                while (numSet.Contains(currentNum + 1)) {
                    currentNum++;
                    currentStreak++;
                }
                maxLength = Math.Max(maxLength, currentStreak);
            }
        }
        return maxLength;
    }
}
```

---

# Graphs

## 994. Rotting Oranges

### Pattern

Multi-source breadth-first search

### Recognition

- Spread from multiple starting points
- Each step represents one time unit

### Key Insight

Start BFS from every rotten orange at once. Process one queue level per minute and count fresh oranges as they become rotten.

### Complexity

- Time: O(rows × columns)
- Space: O(rows × columns).

### Interview One-Liner

Run level-order BFS from all initially rotten oranges; each level is one minute of spread.

### Solution

```csharp
public class Solution {
    public int OrangesRotting(int[][] grid) {
        int minutes = 0;
        Queue<(int,int)> rottenOranges = new();
        int  rows = grid.Length;
        int cols = grid[0].Length;
        int goodOranges =0;

        for(int row =0; row < rows; row++)
        {
            for(int col = 0; col < cols; col++)
            {
                if(grid[row][col] == 2)
                    rottenOranges.Enqueue((row,col));
                else if (grid[row][col] == 1)
                    goodOranges++;
            }
        }

        if (goodOranges == 0)
            return 0;

        int[][] directions =
        {
            new int[] {-1, 0},
            new int[] {1,0},
            new int[] {0,-1},
            new int[] {0,1}
        };

        while(rottenOranges.Count > 0 && goodOranges > 0)
        {
            int levelSize = rottenOranges.Count;
            for(int i =0; i< levelSize; i++)
            {
                var (row, col) = rottenOranges.Dequeue();

                foreach(var d in directions)
                {
                    int newRow = row + d[0];
                    int newCol = col + d[1];

                    if(newRow < 0 || newRow >= rows
                    || newCol < 0 || newCol >= cols
                    || grid[newRow][newCol] != 1)
                    {
                        continue;
                    }

                    grid[newRow][newCol] = 2;
                    goodOranges--;
                    rottenOranges.Enqueue((newRow, newCol));
                }
            }
            minutes++;
        }

        return goodOranges == 0 ? minutes : -1;
    }
}
```

---

## 207. Course Schedule

### Pattern

Topological sort + cycle detection

### Recognition

- Prerequisite dependencies
- Determine whether every course can be completed

### Key Insight

Build the directed prerequisite graph and count indegrees. Kahn's algorithm can process all courses exactly when the graph is acyclic.

### Complexity

- Time: O(V + E)
- Space: O(V + E).

### Interview One-Liner

Repeatedly remove zero-indegree courses; all courses are finishable if every node is removed.

### Solution

```csharp
public class Solution
{
    public bool CanFinish(int numCourses, int[][] prerequisites)
    {
        List<int>[] graph = new List<int>[numCourses];

        for (int i = 0; i < numCourses; i++)
            graph[i] = new List<int>();

        int[] indegree = new int[numCourses];

        foreach (var p in prerequisites)
        {
            int course = p[0];
            int prereq = p[1];

            graph[prereq].Add(course);
            indegree[course]++;
        }

        Queue<int> queue = new();

        for (int i = 0; i < numCourses; i++)
        {
            if (indegree[i] == 0)
                queue.Enqueue(i);
        }

        int completed = 0;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            completed++;

            foreach (int next in graph[current])
            {
                indegree[next]--;

                if (indegree[next] == 0)
                    queue.Enqueue(next);
            }
        }

        return completed == numCourses;
    }
}
```

---

## 210. Course Schedule II

### Pattern

Topological sort (Kahn's algorithm)

### Recognition

- Return a valid dependency order
- Return an empty array when a cycle exists

### Key Insight

Use indegrees and a queue of available courses to build a topological ordering. The ordering is valid only if it contains every course.

### Complexity

- Time: O(V + E)
- Space: O(V + E).

### Interview One-Liner

Use Kahn's algorithm to build the course order and return it only if all courses are included.

### Solution

```csharp
public class Solution
{
    public int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        var graph = new List<int>[numCourses];

        for (int i = 0; i < numCourses; i++)
            graph[i] = new List<int>();

        int[] indegree = new int[numCourses];

        foreach (var p in prerequisites)
        {
            int course = p[0];
            int prereq = p[1];

            graph[prereq].Add(course);
            indegree[course]++;
        }

        Queue<int> queue = new();

        for (int i = 0; i < numCourses; i++)
        {
            if (indegree[i] == 0)
                queue.Enqueue(i);
        }

        List<int> order = new();

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            order.Add(current);

            foreach (int next in graph[current])
            {
                indegree[next]--;

                if (indegree[next] == 0)
                    queue.Enqueue(next);
            }
        }

        return order.Count == numCourses
            ? order.ToArray()
            : Array.Empty<int>();
    }
}
```

---

## 2101. Detonate the Maximum Bombs

### Pattern

Directed graph + DFS/BFS

### Recognition

- Reachability through one-way chain reactions
- Each bomb has its own blast radius

### Key Insight

Create a directed edge from bomb i to bomb j when j lies within i's blast radius. Traverse from every bomb and keep the largest reachable count.

### Complexity

- Time: O(n × (n + E)), worst case O(n³)
- Space: O(n²).

### Interview One-Liner

Build the directed blast graph, then run a traversal from every bomb and take the largest reachable set.

### Solution

```csharp
public class Solution
{
    public int MaximumDetonation(int[][] bombs)
    {
        int n = bombs.Length;

        List<int>[] graph = new List<int>[n];

        for (int i = 0; i < n; i++)
            graph[i] = new List<int>();

        // Build graph
        for (int i = 0; i < n; i++)
        {
            long x1 = bombs[i][0];
            long y1 = bombs[i][1];
            long r = bombs[i][2];

            long rSquared = r * r;

            for (int j = 0; j < n; j++)
            {
                if (i == j)
                    continue;

                long dx = x1 - bombs[j][0];
                long dy = y1 - bombs[j][1];

                long distSquared = dx * dx + dy * dy;

                if (distSquared <= rSquared)
                    graph[i].Add(j);
            }
        }

        int answer = 0;

        for (int start = 0; start < n; start++)
        {
            answer = Math.Max(answer, Dfs(start, graph));
        }

        return answer;
    }

    private int Dfs(int start, List<int>[] graph)
    {
        HashSet<int> visited = new();

        Stack<int> stack = new();
        stack.Push(start);

        while (stack.Count > 0)
        {
            int node = stack.Pop();

            if (!visited.Add(node))
                continue;

            foreach (int next in graph[node])
            {
                stack.Push(next);
            }
        }

        return visited.Count;
    }
}
```

---

## 126. Word Ladder II

### Pattern

BFS + backtracking

### Recognition

- Find every shortest transformation sequence
- Each step changes exactly one character

### Key Insight

Use BFS by levels to record every parent that reaches a word at its shortest distance. Once the target level is found, backtrack through those parent links to emit all shortest paths.

### Complexity

- Time: O(N × L × 26 + output size)
- Space: O(N × L + output size), where N is the dictionary size and L is word length.

### Interview One-Liner

BFS records all shortest-path parents; backtracking through that graph returns every minimum-length word ladder.

### Solution

```csharp
public class Solution
{
    public IList<IList<string>> FindLadders(
        string beginWord,
        string endWord,
        IList<string> wordList)
    {
        HashSet<string> dict = new(wordList);

        List<IList<string>> result = new();

        if (!dict.Contains(endWord))
            return result;

        Dictionary<string, List<string>> parents = new();

        HashSet<string> currentLevel = new()
        {
            beginWord
        };

        bool found = false;

        while (currentLevel.Count > 0 && !found)
        {
            foreach (string word in currentLevel)
                dict.Remove(word);

            HashSet<string> nextLevel = new();

            foreach (string word in currentLevel)
            {
                char[] chars = word.ToCharArray();

                for (int i = 0; i < chars.Length; i++)
                {
                    char original = chars[i];

                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        if (c == original)
                            continue;

                        chars[i] = c;

                        string next = new(chars);

                        if (!dict.Contains(next))
                            continue;

                        if (!parents.ContainsKey(next))
                            parents[next] = new List<string>();

                        parents[next].Add(word);

                        nextLevel.Add(next);

                        if (next == endWord)
                            found = true;
                    }

                    chars[i] = original;
                }
            }

            currentLevel = nextLevel;
        }

        if (!found)
            return result;

        List<string> path = new();

        Backtrack(
            endWord,
            beginWord,
            parents,
            path,
            result);

        return result;
    }

    private void Backtrack(
        string word,
        string beginWord,
        Dictionary<string, List<string>> parents,
        List<string> path,
        List<IList<string>> result)
    {
        path.Add(word);

        if (word == beginWord)
        {
            List<string> sequence = new(path);
            sequence.Reverse();

            result.Add(sequence);
        }
        else if (parents.ContainsKey(word))
        {
            foreach (string parent in parents[word])
            {
                Backtrack(
                    parent,
                    beginWord,
                    parents,
                    path,
                    result);
            }
        }

        path.RemoveAt(path.Count - 1);
    }
}
```

---

# Trees

## 98. Validate Binary Search Tree

### Pattern

DFS with lower and upper bounds

### Recognition

- Validate the entire BST ordering
- A descendant can violate an ancestor's constraint

### Key Insight

Carry an exclusive lower and upper bound down the tree. Each node must satisfy all ancestor constraints, not just its parent-child comparison.

### Complexity

- Time: O(n)
- Space: O(h) recursion stack.

### Interview One-Liner

Validate each node against bounds inherited from every ancestor, not only against its direct children.

### Solution

```csharp
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution
{
    public bool IsValidBST(TreeNode root)
    {
        return Validate(root, long.MinValue, long.MaxValue);
    }

    private bool Validate(TreeNode node, long min, long max)
    {
        if (node == null)
            return true;

        if (node.val <= min || node.val >= max)
            return false;

        return Validate(node.left, min, node.val) &&
               Validate(node.right, node.val, max);
    }
}
```

---

## 958. Check Completeness of a Binary Tree

### Pattern

Breadth-first search

### Recognition

- Check complete-tree shape
- Last level is filled from left to right

### Key Insight

In level-order traversal, after the first missing child position, no later position may contain a node.

### Complexity

- Time: O(n)
- Space: O(n).

### Interview One-Liner

During BFS, once a null position appears, every subsequent position must also be null.

### Solution

```csharp
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution
{
    public bool IsCompleteTree(TreeNode root)
    {
        Queue<TreeNode> queue = new();
        queue.Enqueue(root);

        bool seenNull = false;

        while (queue.Count > 0)
        {
            TreeNode node = queue.Dequeue();

            if (node == null)
            {
                seenNull = true;
                continue;
            }

            if (seenNull)
                return false;

            queue.Enqueue(node.left);
            queue.Enqueue(node.right);
        }

        return true;
    }
}
```

---

## 109. Convert Sorted List to Binary Search Tree

### Pattern

Divide and conquer + slow/fast pointers

### Recognition

- Convert sorted linked data to a balanced BST
- Input has no random access

### Key Insight

Find the middle list node to use as the root, split the list, and recursively build the left and right subtrees.

### Complexity

- Time: O(n log n)
- Space: O(log n) recursion stack.

### Interview One-Liner

Choose the middle list node as the root, then recursively build balanced subtrees from both halves.

### Solution

```csharp
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution
{
    public TreeNode SortedListToBST(ListNode head)
    {
        if (head == null)
            return null;

        if (head.next == null)
            return new TreeNode(head.val);

        ListNode prev = null;
        ListNode slow = head;
        ListNode fast = head;

        while (fast != null && fast.next != null)
        {
            prev = slow;
            slow = slow.next;
            fast = fast.next.next;
        }

        prev.next = null;

        TreeNode root = new(slow.val);

        root.left = SortedListToBST(head);
        root.right = SortedListToBST(slow.next);

        return root;
    }
}
```

---

## 1008. Construct BST from Preorder Traversal

### Pattern

Preorder traversal + value bounds

### Recognition

- Construct a BST from preorder without sorting
- Subtrees occupy bounded value ranges

### Key Insight

Consume preorder values in order. Recursively build a node only when the next value lies within the subtree's exclusive bounds.

### Complexity

- Time: O(n)
- Space: O(h) recursion stack.

### Interview One-Liner

Consume preorder once, using lower and upper bounds to assign each value to its valid subtree.

### Solution

```csharp
public class Solution
{
    private int index;

    public TreeNode BstFromPreorder(int[] preorder)
    {
        index = 0;
        return Build(preorder, long.MinValue, long.MaxValue);
    }

    private TreeNode Build(int[] preorder, long lower, long upper)
    {
        if (index == preorder.Length ||
            preorder[index] <= lower ||
            preorder[index] >= upper)
            return null;

        int value = preorder[index++];
        TreeNode node = new(value);
        node.left = Build(preorder, lower, value);
        node.right = Build(preorder, value, upper);
        return node;
    }
}
```

---

## 236. Lowest Common Ancestor of a Binary Tree

### Pattern

Postorder DFS

### Recognition

- Find the lowest shared ancestor of two nodes
- The tree is not necessarily a BST

### Key Insight

A recursive search returns a target node when found. If the left and right subtrees each return a non-null result, the current node is the lowest common ancestor.

### Complexity

- Time: O(n)
- Space: O(h) recursion stack.

### Interview One-Liner

Search both subtrees; the first node receiving a target from each side is the lowest common ancestor.

### Solution

```csharp
public class Solution
{
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        if (root == null || root == p || root == q)
            return root;

        TreeNode left = LowestCommonAncestor(root.left, p, q);
        TreeNode right = LowestCommonAncestor(root.right, p, q);

        if (left != null && right != null)
            return root;

        return left ?? right;
    }
}
```

---

## 124. Binary Tree Maximum Path Sum

### Pattern

Postorder DFS + running maximum

### Recognition

- Maximum path can start and end anywhere
- A path may use a node's left and right branches

### Key Insight

For each node, return the best one-sided gain to its parent, clamping negative gains to zero. Separately update the global answer using both child gains and the node value.

### Complexity

- Time: O(n)
- Space: O(h) recursion stack.

### Interview One-Liner

Return one positive branch upward, but update the global best with the node and both positive branches.

### Solution

```csharp
public class Solution
{
    private int bestPath;

    public int MaxPathSum(TreeNode root)
    {
        bestPath = int.MinValue;
        MaxGain(root);
        return bestPath;
    }

    private int MaxGain(TreeNode node)
    {
        if (node == null)
            return 0;

        int leftGain = Math.Max(0, MaxGain(node.left));
        int rightGain = Math.Max(0, MaxGain(node.right));

        bestPath = Math.Max(
            bestPath,
            node.val + leftGain + rightGain);

        return node.val + Math.Max(leftGain, rightGain);
    }
}
```

---

# Trie

## 208. Implement Trie

### Pattern

Prefix tree

### Recognition

- Repeated prefix lookups
- Insert, exact search, and prefix search

### Key Insight

Store characters along shared trie paths. Mark the final node of each inserted word so exact-word search differs from prefix search.

### Complexity

- Insert/search/prefix: O(L)
- Space: O(total inserted characters).

### Interview One-Liner

Walk or create one trie edge per character, and mark terminal nodes to distinguish words from prefixes.

### Solution

```csharp
public class Trie {
    private readonly Node root = new();

    public void Insert(string word) {
        var node = root;
        foreach (var ch in word) {
            int i = ch - 'a';
            node.Children[i] ??= new Node();
            node = node.Children[i];
        }
        node.IsFinished = true;
    }

    public bool Search(string word) {
        var node = Traverse(word);
        return node != null && node.IsFinished;
    }

    public bool StartsWith(string prefix) {
        return Traverse(prefix) != null;
    }

    private Node Traverse(string str) {
        var node = root;
        foreach (var ch in str) {
            int i = ch - 'a';
            if (node.Children[i] == null) return null;
            node = node.Children[i];
        }
        return node;
    }

    private class Node {
        public Node[] Children { get; } = new Node[26];
        public bool IsFinished { get; set; }
    }
}
```

---

# Heaps

## 253. Meeting Rooms II

### Pattern

Min-heap of meeting end times

### Recognition

- Minimum number of rooms for overlapping intervals
- A room is reusable when its meeting has ended

### Key Insight

Sort meetings by start time and track occupied room end times in a min-heap. Remove every meeting that ends before the next meeting starts.

### Complexity

- Time: O(n log n)
- Space: O(n).

### Interview One-Liner

Process meetings by start time and reuse every room whose earliest ending meeting has finished.

### Solution

```csharp
public class Solution
{
    public int MinMeetingRooms(int[][] intervals)
    {
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
        PriorityQueue<int, int> endingTimes = new();
        int rooms = 0;

        foreach (int[] interval in intervals)
        {
            while (endingTimes.Count > 0 &&
                   endingTimes.Peek() <= interval[0])
                endingTimes.Dequeue();

            endingTimes.Enqueue(interval[1], interval[1]);
            rooms = Math.Max(rooms, endingTimes.Count);
        }

        return rooms;
    }
}
```

---

## 632. Smallest Range Covering Elements from K Lists

### Pattern

Min-heap + moving range maximum

### Recognition

- Choose one value from every sorted list
- Minimize the range width

### Key Insight

Keep one current value from each list in a min-heap and track the current maximum. Repeatedly advance the list containing the minimum; stop when that list is exhausted.

### Complexity

- Time: O(N log k)
- Space: O(k), where N is the total number of values.

### Interview One-Liner

Track one value per list, advance the current minimum, and record the narrowest range covering all lists.

### Solution

```csharp
public class Solution
{
    public int[] SmallestRange(IList<IList<int>> nums)
    {
        PriorityQueue<(int Value, int List, int Index), int> minHeap = new();
        int currentMax = int.MinValue;

        for (int list = 0; list < nums.Count; list++)
        {
            int value = nums[list][0];
            minHeap.Enqueue((value, list, 0), value);
            currentMax = Math.Max(currentMax, value);
        }

        int bestStart = minHeap.Peek().Value;
        int bestEnd = currentMax;

        while (minHeap.Count == nums.Count)
        {
            var current = minHeap.Dequeue();
            if (currentMax - current.Value < bestEnd - bestStart ||
                (currentMax - current.Value == bestEnd - bestStart &&
                 current.Value < bestStart))
            {
                bestStart = current.Value;
                bestEnd = currentMax;
            }

            int nextIndex = current.Index + 1;
            if (nextIndex == nums[current.List].Count)
                break;

            int nextValue = nums[current.List][nextIndex];
            currentMax = Math.Max(currentMax, nextValue);
            minHeap.Enqueue(
                (nextValue, current.List, nextIndex),
                nextValue);
        }

        return new[] { bestStart, bestEnd };
    }
}
```

---

## 215. Kth Largest Element in an Array

### Pattern

Quickselect with three-way partition

### Recognition

- Find an order statistic without fully sorting
- Duplicates may occur

### Key Insight

The kth largest element is at target index n-k in ascending order. Three-way partition around a pivot and continue only in the partition containing that index.

### Complexity

- Average time: O(n), worst case: O(n²)
- Extra space: O(1).

### Interview One-Liner

Quickselect toward index n-k, discarding the partition that cannot contain the kth largest value.

### Solution

```csharp
public class Solution
{
    public int FindKthLargest(int[] nums, int k)
    {
        int target = nums.Length - k;
        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int pivot = nums[left + (right - left) / 2];
            int less = left;
            int current = left;
            int greater = right;

            while (current <= greater)
            {
                if (nums[current] < pivot)
                {
                    (nums[less], nums[current]) = (nums[current], nums[less]);
                    less++;
                    current++;
                }
                else if (nums[current] > pivot)
                {
                    (nums[current], nums[greater]) =
                        (nums[greater], nums[current]);
                    greater--;
                }
                else
                {
                    current++;
                }
            }

            if (target < less)
                right = less - 1;
            else if (target > greater)
                left = greater + 1;
            else
                return nums[target];
        }

        throw new InvalidOperationException("k is outside the valid range.");
    }
}
```

---

# Data Structure Design

## 155. Min Stack

### Pattern

Stack with prefix minimum

### Recognition

- Retrieve the minimum in constant time
- Support push and pop

### Key Insight

Store each value with the minimum seen at that stack depth. The top entry then provides both the current value and minimum.

### Complexity

- Push/pop/top/get-min: O(1)
- Space: O(n).

### Interview One-Liner

Store the running minimum alongside each pushed value so every operation, including GetMin, stays O(1).

### Solution

```csharp
public class MinStack {
    Stack<(int val,int min)> stack;
    public MinStack() {
        stack = new();
    }
    
    public void Push(int val) {
        int min = stack.Count == 0 ? val : stack.Peek().min;
        min = Math.Min(min, val);
        stack.Push((val,min));
    }
    
    public void Pop() {
        stack.Pop();
    }
    
    public int Top() {
        return stack.Peek().val;
    }
    
    public int GetMin() {
        return stack.Peek().min;
    }
}

/**
 * Your MinStack object will be instantiated and called as such:
 * MinStack obj = new MinStack();
 * obj.Push(val);
 * obj.Pop();
 * int param_3 = obj.Top();
 * int param_4 = obj.GetMin();
 */
```

---

## 146. LRU Cache

### Pattern

Hash map + doubly linked list

### Recognition

- Fixed-capacity cache
- Evict the least recently used item in O(1)

### Key Insight

Map keys to nodes in a doubly linked list ordered by recent use. Move accessed or updated nodes to the front, and evict from the back when capacity is exceeded.

### Complexity

- Get/put: O(1) expected
- Space: O(capacity).

### Interview One-Liner

Combine a hash map for direct lookup with a doubly linked list for O(1) recency updates and eviction.

### Solution

```csharp
public class LRUCache
{
    private sealed class Entry
    {
        public int Key { get; }
        public int Value { get; set; }

        public Entry(int key, int value)
        {
            Key = key;
            Value = value;
        }
    }

    private readonly int capacity;
    private readonly Dictionary<int, LinkedListNode<Entry>> nodes = new();
    private readonly LinkedList<Entry> recency = new();

    public LRUCache(int capacity)
    {
        this.capacity = capacity;
    }

    public int Get(int key)
    {
        if (!nodes.TryGetValue(key, out LinkedListNode<Entry> node))
            return -1;

        recency.Remove(node);
        recency.AddFirst(node);
        return node.Value.Value;
    }

    public void Put(int key, int value)
    {
        if (nodes.TryGetValue(key, out LinkedListNode<Entry> node))
        {
            node.Value.Value = value;
            recency.Remove(node);
            recency.AddFirst(node);
            return;
        }

        if (nodes.Count == capacity)
        {
            LinkedListNode<Entry> leastRecent = recency.Last;
            nodes.Remove(leastRecent.Value.Key);
            recency.RemoveLast();
        }

        LinkedListNode<Entry> entry =
            recency.AddFirst(new Entry(key, value));
        nodes.Add(key, entry);
    }
}
```

---

## 716. Max Stack

### Pattern

Doubly linked list + ordered value index

### Recognition

- Pop the most recent item or the maximum item
- Duplicate maximum values require recency ordering

### Key Insight

Keep stack order in a doubly linked list and index nodes by value in ordered value buckets. The ordered set identifies the maximum; each bucket's last node is its most recently pushed occurrence.

### Complexity

- Push/pop/top/peek-max: O(log n) with ordered indexing
- Pop-max: O(log n)
- Space: O(n).

### Interview One-Liner

Maintain stack order separately from an ordered value index so PopMax removes the newest occurrence of the maximum.

### Solution

```csharp
public class MaxStack
{
    private sealed class Entry
    {
        public int Value { get; }
        public LinkedListNode<Entry> OrderNode { get; set; }

        public Entry(int value)
        {
            Value = value;
        }
    }

    private readonly LinkedList<Entry> order = new();
    private readonly Dictionary<int, LinkedList<Entry>> nodesByValue = new();
    private readonly SortedSet<int> values = new();

    public void Push(int x)
    {
        Entry entry = new(x);
        entry.OrderNode = order.AddLast(entry);

        if (!nodesByValue.TryGetValue(x, out LinkedList<Entry> entries))
        {
            entries = new LinkedList<Entry>();
            nodesByValue.Add(x, entries);
            values.Add(x);
        }

        entries.AddLast(entry);
    }

    public int Pop()
    {
        Entry entry = order.Last.Value;
        Remove(entry);
        return entry.Value;
    }

    public int Top() => order.Last.Value.Value;

    public int PeekMax() => values.Max;

    public int PopMax()
    {
        int maxValue = values.Max;
        Entry entry = nodesByValue[maxValue].Last.Value;
        Remove(entry);
        return entry.Value;
    }

    private void Remove(Entry entry)
    {
        order.Remove(entry.OrderNode);

        LinkedList<Entry> entries = nodesByValue[entry.Value];
        entries.RemoveLast();
        if (entries.Count == 0)
        {
            nodesByValue.Remove(entry.Value);
            values.Remove(entry.Value);
        }
    }
}
```

---

## 981. Time Based Key-Value Store

### Pattern

Hash map + binary search

### Recognition

- Look up the latest value at or before a timestamp
- Each key has a timestamped history

### Key Insight

Store each key's values in timestamp order. For Get, binary-search for the rightmost timestamp not greater than the query.

### Complexity

- Set: O(1) amortized
- Get: O(log m)
- Space: O(n), where m is that key's history length.

### Interview One-Liner

Keep each key's history sorted and binary-search for the latest timestamp that does not exceed the query.

### Solution

```csharp
public class TimeMap
{
    private readonly Dictionary<string, List<(int Timestamp, string Value)>> map;

    public TimeMap()
    {
        map = new();
    }

    public void Set(string key, string value, int timestamp)
    {
        if (!map.ContainsKey(key))
            map[key] = new();

        map[key].Add((timestamp, value));
    }

    public string Get(string key, int timestamp)
    {
        if (!map.ContainsKey(key))
            return "";

        var list = map[key];

        int left = 0;
        int right = list.Count - 1;
        string result = "";

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (list[mid].Timestamp <= timestamp)
            {
                result = list[mid].Value;
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return result;
    }
}

/**
 * Your TimeMap object will be instantiated and called as such:
 * TimeMap obj = new TimeMap();
 * obj.Set(key,value,timestamp);
 * string param_2 = obj.Get(key,timestamp);
 */
```

---

# Linked Lists

## 148. Sort List

### Pattern

Merge sort + slow/fast pointers

### Recognition

- Sort a linked list in O(n log n)
- No random access to list positions

### Key Insight

Split the list with slow and fast pointers, recursively sort both halves, and merge them in linear time.

### Complexity

- Time: O(n log n)
- Space: O(log n) recursion stack.

### Interview One-Liner

Use merge sort: split with slow/fast pointers, recursively sort each half, and merge the sorted lists.

### Solution

```csharp
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution
{
    public ListNode SortList(ListNode head)
    {
        if (head == null || head.next == null)
            return head;

        ListNode slow = head;
        ListNode fast = head;
        ListNode prev = null;

        while (fast != null && fast.next != null)
        {
            prev = slow;
            slow = slow.next;
            fast = fast.next.next;
        }

        prev.next = null;

        ListNode left = SortList(head);
        ListNode right = SortList(slow);

        return Merge(left, right);
    }

    private ListNode Merge(ListNode l1, ListNode l2)
    {
        ListNode dummy = new(0);
        ListNode current = dummy;

        while (l1 != null && l2 != null)
        {
            if (l1.val < l2.val)
            {
                current.next = l1;
                l1 = l1.next;
            }
            else
            {
                current.next = l2;
                l2 = l2.next;
            }

            current = current.next;
        }

        current.next = l1 ?? l2;

        return dummy.next;
    }
}
```

---

## 23. Merge K Sorted Lists

### Pattern

Min-heap / priority queue

### Recognition

- Merge multiple sorted linked lists
- Always take the smallest current head

### Key Insight

Put each non-empty list head in a min-priority queue. Repeatedly append the smallest node and insert its successor.

### Complexity

- Time: O(N log k)
- Space: O(k), where N is the total number of nodes.

### Interview One-Liner

Use a min-heap containing one current node per list, adding each removed node's successor.

### Solution

```csharp
public class Solution
{
    public ListNode MergeKLists(ListNode[] lists)
    {
        PriorityQueue<ListNode, int> minHeap = new();

        foreach (ListNode head in lists)
        {
            if (head != null)
                minHeap.Enqueue(head, head.val);
        }

        ListNode dummy = new(0);
        ListNode tail = dummy;

        while (minHeap.Count > 0)
        {
            ListNode node = minHeap.Dequeue();
            tail.next = node;
            tail = node;

            if (node.next != null)
                minHeap.Enqueue(node.next, node.next.val);
        }

        return dummy.next;
    }
}
```

---

# Backtracking

## 17. Letter Combinations of a Phone Number

### Pattern

DFS backtracking

### Recognition

- Generate all choices across digit positions
- Each digit maps to several letters

### Key Insight

At each digit, recursively try every mapped letter. Add a combination when one letter has been chosen for every digit.

### Complexity

- Time: O(4ⁿ × n) including output
- Auxiliary space: O(n).

### Interview One-Liner

Use DFS to choose one mapped letter per digit and emit each completed combination.

### Solution

```csharp
public class Solution {
    public IList<string> LetterCombinations(string digits) {
        List<string> result = new();
        if (digits.Length == 0)
            return result;
        Dictionary<int,List<char>> keys = new();
        keys.Add(2, new List<char>(){'a','b','c'});
        keys.Add(3, new List<char>(){'d','e','f'});
        keys.Add(4, new List<char>(){'g','h','i'});
        keys.Add(5, new List<char>(){'j','k','l'});
        keys.Add(6, new List<char>(){'m','n','o'});
        keys.Add(7, new List<char>(){'p','q','r','s'});
        keys.Add(8, new List<char>(){'t','u','v'});
        keys.Add(9, new List<char>(){'w','x','y', 'z'});
        Backtracking(0, "");
        return result;
        void Backtracking(int index, string current)
        {
            if(current.Length == digits.Length)
            {
                result.Add(current);
                return;
            }
            int digit = digits[index] - '0'; // Convert char to int 
            if (keys.ContainsKey(digit)) 
            { 
                foreach (char letter in keys[digit]) 
                { 
                    Backtracking(index + 1, current + letter); 
                }
            }
        }
    }
}
```

---

# Dynamic Programming

## 91. Decode Ways

### Pattern

One-dimensional DP

### Recognition

- Count interpretations of a digit string
- Zero is valid only as part of 10 or 20

### Key Insight

At each position, add the ways from the previous position for a valid single digit, and from two positions back for a valid pair from 10 through 26.

### Complexity

- Time: O(n)
- Extra space: O(1).

### Interview One-Liner

For each index, combine valid one-digit and two-digit decodings while keeping only the previous two DP values.

### Solution

```csharp
public class Solution
{
    public int NumDecodings(string s)
    {
        if (string.IsNullOrEmpty(s) || s[0] == '0')
            return 0;

        int prev2 = 1;
        int prev1 = 1;

        for (int i = 1; i < s.Length; i++)
        {
            int current = 0;

            if (s[i] != '0')
                current += prev1;

            int twoDigit = (s[i - 1] - '0') * 10 + (s[i] - '0');

            if (twoDigit >= 10 && twoDigit <= 26)
                current += prev2;

            prev2 = prev1;
            prev1 = current;
        }

        return prev1;
    }
}
```

---

# Intervals

## 1094. Car Pooling

### Pattern

Sweep line / difference array

### Recognition

- Passenger occupancy changes at trip endpoints
- Drop-offs happen before pickups at the same location

### Key Insight

Add passengers at each pickup and subtract them at each drop-off. A prefix sum gives occupancy after all changes at a location; reject any value above capacity.

### Complexity

- Time: O(n + U)
- Space: O(U), where U is the location range.

### Interview One-Liner

Record passenger-count changes at each endpoint and scan the running total to ensure capacity is never exceeded.

### Solution

```csharp
public class Solution
{
    public bool CarPooling(int[][] trips, int capacity)
    {
        int[] diff = new int[1001];

        foreach (var trip in trips)
        {
            int passengers = trip[0];
            int from = trip[1];
            int to = trip[2];

            diff[from] += passengers;
            diff[to] -= passengers;
        }

        int current = 0;

        foreach (int change in diff)
        {
            current += change;

            if (current > capacity)
                return false;
        }

        return true;
    }
}
```

---

# Prefix Sum

## 304. Range Sum Query 2D - Immutable

### Pattern

Two-dimensional prefix sum

### Recognition

- Many rectangular sum queries on an immutable matrix
- Each query must take constant time

### Key Insight

Precompute a padded 2D prefix-sum table. Use inclusion-exclusion at the four corners to answer each rectangle query.

### Complexity

- Build: O(rows × columns)
- Query: O(1)
- Space: O(rows × columns).

### Interview One-Liner

Precompute a 2D prefix table and use four-corner inclusion-exclusion for each constant-time query.

### Solution

```csharp
public class NumMatrix
{
    private readonly int[][] prefix;

    public NumMatrix(int[][] matrix)
    {
        int rows = matrix.Length;
        int cols = rows == 0 ? 0 : matrix[0].Length;
        prefix = new int[rows + 1][];

        for (int row = 0; row <= rows; row++)
            prefix[row] = new int[cols + 1];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                prefix[row + 1][col + 1] =
                    matrix[row][col]
                    + prefix[row][col + 1]
                    + prefix[row + 1][col]
                    - prefix[row][col];
            }
        }
    }

    public int SumRegion(int row1, int col1, int row2, int col2)
    {
        return prefix[row2 + 1][col2 + 1]
            - prefix[row1][col2 + 1]
            - prefix[row2 + 1][col1]
            + prefix[row1][col1];
    }
}
```

---

# Binary Search

## 875. Koko Eating Bananas

### Pattern

Binary search on the answer

### Recognition

- Find the minimum speed that meets a time limit
- Feasibility is monotonic: if a speed works, every faster speed works

### Key Insight

Binary search the eating speed from 1 to the largest pile. For each candidate speed, sum the hours needed per pile and check whether the total fits within `h`.

### Complexity

- Time: O(n log m), where n is the number of piles and m is the largest pile
- Space: O(1).

### Interview One-Liner

Binary search the minimum feasible speed, using the total hours required at each speed as a monotonic feasibility check.

### Solution

```csharp
public class Solution
{
    public int MinEatingSpeed(int[] piles, int h)
    {
        int left = 1;
        int right = piles.Max();

        while (left < right)
        {
            int mid = left + (right - left) / 2;

            if (CanEat(mid))
                right = mid;
            else
                left = mid + 1;
        }

        return left;

        bool CanEat(int speed)
        {
            long hours = 0;

            foreach (int pile in piles)
            {
                hours += (pile - 1L) / speed + 1;

                if (hours > h)
                    return false;
            }

            return true;
        }
    }
}
```

---

# Two Pointers

## 443. String Compression

### Pattern

Read/write pointers

### Recognition

- Compress consecutive character runs in place
- Only the resulting prefix is returned

### Key Insight

Read each run once and write its character and, when greater than one, the decimal digits of its count into the array's prefix.

### Complexity

- Time: O(n)
- Extra space: O(1).

### Interview One-Liner

Use separate read and write pointers to replace each run with its character and optional count.

### Solution

```csharp
public class Solution
{
    public int Compress(char[] chars)
    {
        int read = 0, write = 0;

        while (read < chars.Length)
        {
            char current = chars[read];
            int count = 0;

            // Count consecutive characters
            while (read < chars.Length && chars[read] == current)
            {
                read++;
                count++;
            }

            // Write the character
            chars[write++] = current;

            // Write the count if greater than 1
            if (count > 1)
            {
                foreach (char digit in count.ToString())
                {
                    chars[write++] = digit;
                }
            }
        }
        return write;
    }
}
```

---

# Math

## 365. Water and Jug Problem

### Pattern

Greatest common divisor

### Recognition

- Measure an exact target using two jug capacities
- Operations fill, empty, and pour

### Key Insight

By Bézout's identity, a target is measurable exactly when it does not exceed the total capacity and is divisible by gcd(x, y). Handle target zero directly.

### Complexity

- Time: O(log(min(x, y)))
- Space: O(1).

### Interview One-Liner

The target is measurable iff it is at most x+y and is a multiple of gcd(x,y), with zero always measurable.

### Solution

```csharp
public class Solution
{
    public bool CanMeasureWater(int x, int y, int target)
    {
        if (target > x + y)
            return false;

        if (target == 0)
            return true;

        return target % Gcd(x, y) == 0;
    }

    private int Gcd(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}
```

---

# Simulation

## 1298. Maximum Candies You Can Get from Boxes

### Pattern

BFS over owned and unlocked boxes

### Recognition

- Opening boxes reveals candies, keys, and more boxes
- A locked box may become openable later

### Key Insight

Track owned boxes and unlocked status separately. Whenever both conditions hold, enqueue the box once, open it, and process its contents.

### Complexity

- Time: O(n + K + B)
- Space: O(n), where K and B are the total key and contained-box entries.

### Interview One-Liner

Use a queue for boxes that are both owned and unlocked; opening each box once may reveal further boxes and keys.

### Solution

```csharp
public class Solution
{
    public int MaxCandies(
        int[] status,
        int[] candies,
        int[][] keys,
        int[][] containedBoxes,
        int[] initialBoxes)
    {
        Queue<int> queue = new();
        HashSet<int> boxes = new();
        HashSet<int> opened = new();

        foreach (int box in initialBoxes)
        {
            boxes.Add(box);

            if (status[box] == 1)
                queue.Enqueue(box);
        }

        int result = 0;

        while (queue.Count > 0)
        {
            int box = queue.Dequeue();

            if (!opened.Add(box))
                continue;

            result += candies[box];

            foreach (int key in keys[box])
            {
                status[key] = 1;

                if (boxes.Contains(key) && !opened.Contains(key))
                    queue.Enqueue(key);
            }

            foreach (int nextBox in containedBoxes[box])
            {
                boxes.Add(nextBox);

                if (status[nextBox] == 1 && !opened.Contains(nextBox))
                    queue.Enqueue(nextBox);
            }
        }

        return result;
    }
}
```

---

## 1620. Coordinate With Maximum Network Quality

### Pattern

Bounded-grid brute force

### Recognition

- Find the coordinate with maximum signal quality
- Candidate coordinates are bounded from 0 to 50

### Key Insight

Evaluate every coordinate in the 51-by-51 grid, summing the floored contribution from each tower within the radius. Keep the first coordinate on ties.

### Complexity

- Time: O(51² × t) for t towers
- Space: O(1).

### Interview One-Liner

Because the candidate grid is small, evaluate each point and sum its in-range tower signals.

### Solution

```csharp
public class Solution
{
    public int[] BestCoordinate(int[][] towers, int radius)
    {
        int bestX = 0;
        int bestY = 0;
        int bestQuality = -1;

        for (int x = 0; x <= 50; x++)
        {
            for (int y = 0; y <= 50; y++)
            {
                int quality = 0;

                foreach (var tower in towers)
                {
                    int dx = tower[0] - x;
                    int dy = tower[1] - y;

                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance <= radius)
                    {
                        quality += (int)Math.Floor(
                            tower[2] / (1 + distance));
                    }
                }

                if (quality > bestQuality)
                {
                    bestQuality = quality;
                    bestX = x;
                    bestY = y;
                }
            }
        }

        return new[] { bestX, bestY };
    }
}
```

---

## 158. Read N Characters Given read4 II

### Pattern

Persistent buffer

### Recognition

- Repeated reads may request fewer characters than read4 returns
- Unread characters must survive between calls

### Key Insight

Keep the read4 buffer and its valid range as object state. Copy buffered characters first, refilling only when they are exhausted or empty.

### Complexity

- Time: O(n + read4 calls)
- Extra space: O(1).

### Interview One-Liner

Persist the read4 buffer and indices across calls so unused characters are returned before reading more input.

### Solution

```csharp
public class Solution
{
    private readonly char[] buffer = new char[4];
    private int bufferIndex;
    private int bufferCount;

    public int Read(char[] buf, int n)
    {
        int written = 0;

        while (written < n)
        {
            if (bufferIndex == bufferCount)
            {
                bufferCount = read4(buffer);
                bufferIndex = 0;

                if (bufferCount == 0)
                    break;
            }

            while (written < n && bufferIndex < bufferCount)
                buf[written++] = buffer[bufferIndex++];
        }

        return written;
    }
}
```

---

# Pattern Recognition Cheat Sheet

Use the defining constraint or requested result to identify a likely starting pattern. Confirm it against edge cases before committing to an approach.

- **Sliding window:** contiguous substring or subarray with a longest, shortest, or validity condition.
- **Hash set or map:** fast membership checks, frequencies, or grouping by a derived key.
- **Two pointers:** sorted data, in-place compaction, pair search, or a moving boundary.
- **Prefix sum:** repeated range-sum queries or cumulative changes across positions.
- **Binary search:** sorted search space or a monotonic feasibility condition.
- **BFS:** shortest paths in an unweighted graph, level order, or minimum-step spread.
- **DFS / backtracking:** explore connected states, enumerate choices, or build candidate paths.
- **Topological sort:** order tasks with prerequisites; failure to process all nodes signals a cycle.
- **Heap:** repeatedly retrieve the smallest/largest item or merge several sorted streams.
- **Monotonic stack:** nearest greater/smaller element or a span constrained by prior values.
- **Intervals / sweep line:** overlapping ranges or values that change at ordered endpoints.
- **Tree DFS:** validate or aggregate information from child subtrees; pass bounds or return subtree state.
- **Dynamic programming:** overlapping subproblems with a compact state and recurrence.
- **Greedy:** a locally optimal choice can be justified by an exchange argument or invariant.
- **Union-find:** dynamically connect components and answer connectivity queries.
- **Trie:** repeated prefix queries over strings.
- **Linked-list merge sort:** sorting linked data without random access.
- **Quickselect:** find one rank/order statistic without sorting every element.
- **GCD / number theory:** integer combinations or divisibility constraints.
- **Simulation:** the state space is small or the problem directly specifies a bounded process.

# Last Minute Review

## Before Coding

- Restate the input, output, and constraints.
- Identify whether order, contiguity, duplicates, or in-place mutation matters.
- Ask what the output shape implies: one optimum, all paths, a count, or a validity result.
- Choose a simple example and include an edge case.

## While Coding

- State the invariant that makes the algorithm correct.
- Define when the state changes and when a candidate answer is recorded.
- Handle empty input, one element, duplicates, boundaries, and unreachable states.
- For graphs, avoid revisiting nodes; for trees, clarify the meaning returned by recursion.
- For heaps, state exactly what each heap entry represents.

## Before Finishing

- Dry-run the example and one edge case.
- Derive runtime from the actual operations and data structure.
- Account for auxiliary memory separately from output storage.
- Check integer overflow, tie-breaking, and whether input mutation is allowed.
- Give the interviewer the one-line summary before discussing optimizations.

## Complexity Reminders

- Hash-table operations are expected O(1), not guaranteed worst-case O(1).
- A heap operation is O(log n); building a heap from n values can be O(n).
- BFS/DFS is O(V + E) with adjacency lists.
- Merge sort is O(n log n); recursive auxiliary space depends on the structure.
- Quickselect is average O(n), but worst-case O(n²).
- Include the cost of producing all requested output paths or combinations.
