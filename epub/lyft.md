# Lyft Interview Preparation

## How to use this book

For each problem:

1. Read problem.
2. Try to solve without help.
3. Do a dry run.
4. Write solution in C#.
5. Explain complexity.
6. Explain why is better than brute force.

---

# Sliding Window

## 76. Minimum Window Substring

### Pattern

Sliding Window Variable

### Recognition

- Minimum substring
- Contains all characters
- Expand and shrink window

### Brute Force

Probar todas las ventanas posibles y verificar si contienen todos los caracteres.

### Optimal Idea

Expandir por la derecha hasta tener todos los caracteres requeridos.

Una vez válida la ventana:

- Guardar resultado.
- Comenzar a contraer por la izquierda.
- Mantener la ventana válida lo más pequeña posible.

### Complexity

Time: O(n)

Space: O(1)

### Common Mistakes

- No manejar caracteres repetidos de t.
- No actualizar correctamente el contador de caracteres faltantes.
- Confundirse con caracteres extra.

### My Solution
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

Sliding Window

### Recognition

- Longest substring
- Without duplicates

### Brute Force

Generar todas las substrings.

### Optimal Idea

Mantener una ventana sin duplicados usando HashSet o frecuencias.

Cuando aparece un duplicado:

- Mover left.
- Eliminar caracteres de la ventana.

### Complexity

Time: O(n)

Space: O(k)

### Common Mistakes

- Mover right sin limpiar duplicados.
- No actualizar el tamaño máximo.

### My Solution

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

HashSet

### Recognition

- Consecutive numbers
- O(n) expected

### Brute Force

Ordenar.

### Better

Usar HashSet.

Comenzar únicamente desde números que no tengan predecesor.

### Complexity

Time: O(n)

Space: O(n)

### Common Mistakes

- Comenzar secuencias desde cualquier número.
- Ordenar innecesariamente.

### My Solution

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

Multi Source BFS

### Recognition

- Spread
- Infection
- Time simulation

### Brute Force

Simular minuto por minuto revisando toda la matriz.

### Optimal Idea

Todas las naranjas podridas iniciales entran al queue.

Cada nivel del BFS representa un minuto.

### Complexity

Time: O(m*n)

Space: O(m*n)

### Common Mistakes

- Incrementar minutos incorrectamente.
- No procesar BFS por niveles.

### My Solution
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

Cycle Detection

### Recognition

- Prerequisites
- Dependencies

### Approaches

- DFS
- Kahn Topological Sort

### Optimal Idea

Si todos los nodos pueden procesarse:

No existe ciclo.

### Complexity

Time: O(V+E)

Space: O(V+E)

### Common Mistakes

- Usar un solo HashSet.
- No distinguir visited de visiting.

### My Solution
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

Topological Sort

### Recognition

- Return ordering
- Dependencies

### Idea

Aplicar Kahn y construir el orden.

English: Use Kahn's algorithm to build the ordering.

### Complexity

Time: O(V+E)

Space: O(V+E)

### My Solution
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

Graph DFS/BFS

### Recognition

- Reachability
- Chain reaction

### Idea

Construir grafo.

Desde cada bomba realizar DFS/BFS.

English: Build a graph, then run DFS or BFS from each bomb.

### One-Liner Interview Explanation

Model each bomb as a node in a directed graph. Add an edge i -> j when bomb j is inside bomb i's blast radius. Then run DFS/BFS from every node and return the maximum number of reachable nodes. Building the graph costs O(n²), and because n ≤ 100, traversing from every node is efficient.

### Complexity

Time: O(n²)

Space: O(n²)

### My Solution
```
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
---

## 126. Word Ladder II

### Pattern

BFS + Backtracking

### Recognition

- Shortest transformations
- Return all paths

### Idea

BFS para construir niveles.

Backtracking para reconstruir rutas.

English: Use BFS to build the levels, then use backtracking to reconstruct the paths.

### Complexity

High

### My Solution
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

Tree DFS

### Recognition

- BST validation

### Wrong Approach

Comparar únicamente hijos directos.

### Optimal Idea

Propagar límites válidos.

### Complexity

Time: O(n)

Space: O(h)

### Common Mistakes

- Verificar sólo padre e hijo.

### Notes
Pattern:
DFS + Min/Max Bounds

Key Insight:
A node must satisfy constraints from ALL ancestors,
not only its direct parent.

For each node:
min < node.val < max

Left subtree:
(min, node.val)

Right subtree:
(node.val, max)

Complexity:
Time: O(n)
Space: O(h)

Interview Trap:
Checking only:
node.left < node < node.right

is WRONG.

### My Solution
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

BFS

### Recognition

- Complete Tree

### Idea

Una vez encontrado un null, todos los nodos siguientes deben ser null.

English: Once a null node is found, all following nodes must also be null.

### Complexity

Time: O(n)

Space: O(n)

### My Notes
Pattern:
BFS / Level-Order Traversal

Key Insight:
After the first null node appears in BFS order,
every remaining node must also be null.

Algorithm:
1. BFS through the tree.
2. Once null is found, set seenNull = true.
3. If a non-null node appears afterward, return false.

Complexity:
Time: O(n)
Space: O(n)

Interview Trigger:
"Complete Binary Tree" => Think BFS.
### My Solution
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

Divide and Conquer

### Recognition

- Sorted List
- Balanced BST

### Idea

Encontrar nodo medio.

Construir recursivamente.

English: Find the middle node, then build the tree recursively.

### Complexity

Time: O(n log n)

### My Notes

**Pattern:** Fast & Slow Pointers + Divide and Conquer

**Key Insight:**
The middle node becomes the root because the input is sorted.
Use slow/fast pointers to find the middle element, then recursively build left and right subtrees.

**Algorithm**
1. Find middle node with slow/fast pointers.
2. Use middle as root.
3. Recursively build left subtree from left half.
4. Recursively build right subtree from right half.

**Complexity**
- Time: O(n log n)
- Space: O(log n)

**Interview One-Liner**
Use the middle node as the BST root, found with slow/fast pointers, and recursively build balanced left and right subtrees from the two halves.
### My Solution
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

BST Construction

### Recognition

- Preorder
- BST

### Idea

Usar límites min/max.

English: Use minimum and maximum bounds.

### Complexity

Time: O(n)

### My Solution
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
public class Solution {
    public TreeNode BstFromPreorder(int[] preorder) {
        TreeNode result = new TreeNode(preorder[0]);
        for(int i =1; i < preorder.Length; i++){
            InsertIntoTree(result, preorder[i]);
        }
        return result;
    }
    
    private void InsertIntoTree(TreeNode node, int val) {
        TreeNode before = null;
        TreeNode after = node;
        while (after != null){
            before = after;
            
            if(val < after.val)
                after = after.left;
            else if (val > after.val)
                after = after.right;
        }
        
        TreeNode newNode = new TreeNode(val);
        if (val < before.val)
            before.left = newNode;
        else
            before.right = newNode;
    }
}
```

---

# Trie

## 208. Implement Trie

### Pattern

Trie

### Recognition

- Prefix
- Dictionary
- Autocomplete

### Operations

- Insert
- Search
- StartsWith

### Complexity

Insert: O(n)

Search: O(n)

### My Solution
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

# Heap

## 253. Meeting Rooms II

### Pattern

Heap

### Recognition

- Overlapping intervals

### Idea

Ordenar por inicio.

MinHeap de tiempos de terminación.

English: Sort by start time and use a min-heap of end times.

### Complexity

Time: O(n log n)

### My Notes

---

## 632. Smallest Range Covering Elements from K Lists

### Pattern

Heap

### Recognition

- K sorted lists

### Idea

Mantener el mínimo actual con heap.

English: Use a heap to keep track of the current minimum.

### Complexity

Time: O(n log k)

### My Solution
```csharp
public class Solution
{
    public int[] SmallestRange(IList<IList<int>> nums)
    {
        var merged = new List<(int value, int listIndex)>();
        // Merge all numbers with their originating list index
        for (int i = 0; i < nums.Count; i++)
            foreach (var num in nums[i])
                merged.Add((num, i));
        // Sort by value
        merged.Sort((a, b) => a.value.CompareTo(b.value));
        var freq = new Dictionary<int, int>();
        int left = 0, count = 0;
        int rangeStart = 0, rangeEnd = int.MaxValue;

        for (int right = 0; right < merged.Count; right++)
        {
            int listId = merged[right].listIndex;
            if (!freq.ContainsKey(listId))
                freq[listId] = 0;

            freq[listId]++;
            if (freq[listId] == 1)
                count++;

            // Try to shrink the window when all lists are represented
            while (count == nums.Count)
            {
                int curStart = merged[left].value;
                int curEnd = merged[right].value;
                if (curEnd - curStart < rangeEnd - rangeStart)
                {
                    rangeStart = curStart;
                    rangeEnd = curEnd;
                }
                int leftListId = merged[left].listIndex;
                freq[leftListId]--;
                if (freq[leftListId] == 0)
                    count--;

                left++;
            }
        }
        return new int[] { rangeStart, rangeEnd };
    }
}
```

---

# Data Structure Design

## 155. Min Stack

### Pattern

Custom Stack

### Recognition

- Min
- Push
- Pop

### Idea

Stack auxiliar con mínimos.

English: Use an auxiliary stack to keep track of the minimum.

### Complexity

All operations O(1)

### My Solution
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

## 716. Max Stack

### Pattern

Data Structure Design

### Recognition

- Max retrieval
- Max removal

### Approaches

- Two stacks
- DLL + TreeMap

### My Notes

---

## 981. Time Based Key-Value Store

### Pattern

HashMap + Binary Search

### Recognition

- Timestamp lookups

### Idea

Guardar pares ordenados por tiempo.

Binary search durante consultas.

English: Store key-value pairs ordered by timestamp and use binary search for lookups.

### Complexity

Set: O(1)

Get: O(log n)

### My Notes
**Pattern:** HashMap + Binary Search

**Key Insight:**
For each key, store values ordered by timestamp.
Use binary search to find the latest timestamp <= target timestamp.

**Algorithm**
1. HashMap<Key, List<(timestamp, value)>>.
2. Append values during Set() since timestamps are strictly increasing.
3. During Get(), binary search for the rightmost timestamp <= target.

**Complexity**
- Set: O(1)
- Get: O(log n)
- Space: O(n)

**Interview One-Liner**
Store each key's history in timestamp order and use binary search to find the most recent value whose timestamp does not exceed the requested timestamp.
### My Solution
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

Merge Sort

### Recognition

- Sort linked list

### Idea

Slow/Fast Pointer.

Merge Sort.

English: Use slow and fast pointers to split the list, then apply merge sort.

### Complexity

Time: O(n log n)

Space: O(log n)

### My Notes
**Pattern:** Merge Sort on Linked List

**Key Insight:**
Linked lists do not support random access, making Merge Sort the optimal O(n log n) sorting algorithm.

**Algorithm**
1. Find the middle using slow/fast pointers.
2. Split the list into two halves.
3. Recursively sort both halves.
4. Merge the two sorted lists.

**Complexity**
- Time: O(n log n)
- Space: O(log n) recursion stack

**Interview One-Liner**
Use Merge Sort: split the linked list with slow/fast pointers, recursively sort both halves, then merge the sorted lists.
### My Solution
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

# Backtracking

## 17. Letter Combinations of a Phone Number

### Pattern

Backtracking

### Recognition

- Generate combinations

### Idea

DFS creando combinaciones carácter por carácter.

English: Use DFS to build combinations one character at a time.

### Complexity

O(4^n)

### My Solution
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

Dynamic Programming

### Recognition

- Count ways

### Idea

DP sobre la posición.

English: Use dynamic programming over the string positions.

### Complexity

Time: O(n)

Space: O(1)

### Common Mistakes

- Manejo de ceros.

### My Notes
**Pattern:** Dynamic Programming

**Key Insight:**
At each position, you can decode:
- One digit (1-9)
- Two digits (10-26)

The number of ways to reach a position is the sum of valid one-digit and two-digit decodings.

**Algorithm**
1. DP[i] = number of ways to decode up to index i.
2. If current digit is valid (1-9), add DP[i-1].
3. If current two-digit number is valid (10-26), add DP[i-2].
4. Optimize DP into two variables.

**Complexity**
- Time: O(n)
- Space: O(1)

**Interview One-Liner**
For each position, count valid one-digit and two-digit decodings, and use DP to accumulate the total number of ways.
### My Solution
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
There is a car with capacity empty seats. The vehicle only drives east (i.e., it cannot turn around and drive west).

You are given the integer capacity and an array trips where trips[i] = [numPassengersi, fromi, toi] indicates that the ith trip has numPassengersi passengers and the locations to pick them up and drop them off are fromi and toi respectively. The locations are given as the number of kilometers due east from the car's initial location.

Passengers are dropped off before new passengers are picked up at the same location. At every point along the route, the total number of passengers in the car must not exceed capacity.

Return true if it is possible to pick up and drop off all passengers for all the given trips, or false otherwise.

 

Example 1:

Input: trips = [[2,1,5],[3,3,7]], capacity = 4
Output: false
Explanation:
At kilometer 1, 2 passengers are picked up, so the car holds 2.
At kilometer 3, 3 more are picked up, so the car holds 5.
Since 5 > capacity = 4, the trips cannot all be completed.
Example 2:

Input: trips = [[2,1,5],[3,3,7]], capacity = 5
Output: true
Explanation:
At kilometer 1, the car holds 2 passengers.
At kilometer 3, the car holds 5 passengers.
At kilometer 5, the first 2 are dropped off, so the car holds 3.
At kilometer 7, the last 3 are dropped off, so the car holds 0.
The maximum occupancy is 5, which never exceeds capacity = 5.

### Pattern

Sweep Line

### Recognition

- Capacity
- Trips

### Idea

Registrar eventos de subida y bajada.

Recorrer cronológicamente.

English: Record pickup and drop-off events, then process them in chronological order.

### Complexity

Time: O(n log n)

### My Notes
**Pattern:** Line Sweep / Difference Array

**Key Insight:**
Instead of tracking each passenger individually, record passenger changes at pickup and dropoff locations.

**Algorithm**
1. Add passengers at pickup location.
2. Remove passengers at dropoff location.
3. Compute a running sum.
4. If passengers ever exceed capacity, return false.

**Complexity**
- Time: O(n + 1000)
- Space: O(1000)

**Interview One-Liner**
Use a difference array to track passenger count changes at each location and compute a running sum to verify capacity is never exceeded.
### My Solution
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

2D Prefix Sum

### Recognition

- Many sum queries

### Idea

Precalcular matriz acumulada.

English: Precompute a prefix-sum matrix.

### Complexity

Build: O(m*n)

Query: O(1)

### My Solution
```csharp
public class NumMatrix {
    private int[][] matrix;
    public NumMatrix(int[][] matrix) {
        int m = matrix.Length;
        int n = matrix[0].Length;
        
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (i > 0)
                    matrix[i][j] += matrix[i - 1][j];
                if (j > 0)
                    matrix[i][j] += matrix[i][j - 1];
                if (i > 0 && j > 0)
                    matrix[i][j] -= matrix[i - 1][j - 1];
            }
        }
        this.matrix = matrix;
    }
    
    public int SumRegion(int row1, int col1, int row2, int col2) {
        int result = matrix[row2][col2];
        if (row1 > 0)
            result -= matrix[row1 - 1][col2];
        if (col1 > 0)
            result -= matrix[row2][col1 - 1];
        if (row1 > 0 && col1 > 0)
            result += matrix[row1 - 1][col1 - 1];
        
        return result;
    }
}

/**
 * Your NumMatrix object will be instantiated and called as such:
 * NumMatrix obj = new NumMatrix(matrix);
 * int param_1 = obj.SumRegion(row1,col1,row2,col2);
 */
```

---

# Two Pointers

## 443. String Compression

### Pattern

Two Pointers

### Recognition

- In place compression

### Idea

Leer grupos.

Escribir resultado.

English: Read each group of characters and write the compressed result.

### Complexity

Time: O(n)

Space: O(1)

### My Solution
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

Math

### Recognition

- Jugs
- Measure volume

### Idea

Máximo común divisor.

English: Use the greatest common divisor.

### Complexity

O(log(min(a,b)))

### My Notes
**Pattern:** Math (Bézout's Identity)

**Key Insight:**
You can measure exactly `target` liters iff:
- `target <= x + y`
- `target` is a multiple of `gcd(x, y)`

**Algorithm**
1. If target exceeds total capacity, return false.
2. Compute gcd(x, y).
3. Check whether target % gcd(x, y) == 0.

**Complexity**
- Time: O(log(min(x, y)))
- Space: O(1)

**Interview One-Liner**
By Bézout's Identity, a target volume is measurable iff it is a multiple of gcd(x, y) and does not exceed the combined capacity of both
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

BFS Simulation

### Recognition

- Keys
- Locked boxes

### Idea

Abrir cajas progresivamente a medida que obtienes llaves.

English: Open boxes progressively as keys are obtained.

### Complexity

O(n)

### My Notes
**Pattern:** BFS / Simulation

**Key Insight:**
A box can only be opened if:
- You possess the box.
- You have its key (or it starts unlocked).

Opening a box may give:
- Candies
- More keys
- More boxes

Process boxes as soon as they become openable.

**Algorithm**
1. Track owned boxes.
2. Queue every box that is both owned and unlocked.
3. Open boxes with BFS.
4. Collect candies, keys, and contained boxes.
5. Enqueue newly openable boxes.

**Complexity**
- Time: O(n)
- Space: O(n)

**Interview One-Liner**
Use BFS to repeatedly open every box that is both owned and unlocked, collecting keys and new boxes that may unlock additional opportunities.

### My Solution
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

Brute Force Geometry

### Recognition

- Coordinates
- Signal strength

### Idea

Evaluar cada punto candidato.

English: Evaluate each candidate point.

### Complexity

O(n*m)

### My Notes
**Pattern:** Brute Force + Geometry

**Key Insight:**
Coordinates are bounded (0..50), making brute force feasible.
Evaluate the network quality at every possible coordinate and keep the best one.

**Algorithm**
1. Iterate through all coordinates (0..50, 0..50).
2. For each tower within radius:
   floor(qi / (1 + distance)).
3. Sum signal contributions.
4. Track the coordinate with maximum quality.

**Complexity**
- Time: O(51 × 51 × n) ≈ O(n)
- Space: O(1)

**Interview One-Liner**
Because coordinates are limited to a 51×51 grid, brute-force every location, compute its signal quality from all reachable towers, and choose the maximum.
### My Solution
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

Buffer Design

### Recognition

- Stateful API

### Idea

Mantener buffer entre llamadas.

English: Keep a buffer between calls.

### Complexity

O(n)

### My Notes

---