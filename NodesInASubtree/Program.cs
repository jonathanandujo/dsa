using System;
using System.Collections.Generic;

namespace NodesInASubtree
{
    class Program
    {
        // Tree Node 
        class Node {
            public int val;
            public List<Node> children;
        
            public Node() {
            val = 0;
            children = new List<Node>();
            }
        
            public Node(int _val) {
            val = _val;
            children = new List<Node>();
            }
        
            public Node(int _val, List<Node> _children) {
            val = _val;
            children = _children;
            }
        }

        class Query {
            public int u; 
            public char c; 
            public Query(int u, char c) {
            this.u = u; 
            this.c = c;
            }
        }
        
        static void Main(string[] args) {
            // Tests
            string s_1 = "aba";
            Node root_1 = new Node(1); 
            root_1.children.Add(new Node(2)); 
            root_1.children.Add(new Node(3)); 
            List<Query> queries_1 = new List<Query>();
            queries_1.Add(new Query(1, 'a'));
            int[] output_1 = countOfNodes(root_1, queries_1, s_1); 
            foreach ( int i in output_1) // 2
            Console.Write($"{i} ");
            Console.WriteLine();
            
            string s_2 = "abaacab";
            Node root_2 = new Node(1); 
            root_2.children.Add(new Node(2)); 
            root_2.children.Add(new Node(3)); 
            root_2.children.Add(new Node(7)); 
            root_2.children[0].children.Add(new Node(4));
            root_2.children[0].children.Add(new Node(5)); 
            root_2.children[1].children.Add(new Node(6));
            List<Query> queries_2 = new List<Query>();
            queries_2.Add(new Query(1, 'a'));
            queries_2.Add(new Query(2, 'b'));
            queries_2.Add(new Query(3, 'a'));
            int[] output_2 = countOfNodes(root_2, queries_2, s_2); 
            foreach ( int i in output_2) // 4 1 2
            Console.Write($"{i} ");
        }
        
        private static Dictionary<char, int> dfs(Node node, string s, Dictionary<int, Dictionary<char, int>> countMap) {
            Dictionary<char, int> charCountMap = new Dictionary<char, int>();
            charCountMap[s[node.val - 1]] = 1;
            
            foreach (Node child in node.children) {
                foreach (KeyValuePair<char,int> entry in dfs(child, s, countMap)) {
                    charCountMap[entry.Key] = charCountMap.GetValueOrDefault(entry.Key,0) + entry.Value;
                }
            }
            
            countMap[node.val] = charCountMap;
            return charCountMap;
        }

        private static int[] countOfNodes(Node root, List<Query> queries, String s) {
            var result = new int[queries.Count];
            var countMap = new Dictionary<int, Dictionary<char, int>>();
            dfs(root, s, countMap);
            
            for(int i =0; i< queries.Count; i++){
                result[i] = countMap[queries[i].u].GetValueOrDefault(queries[i].c,0);
            }
            return result;
        }
    }
}
