using System;
using System.Collections.Generic;
using System.Text;

namespace PrintDistanceBetween2Nodes
{
    class Program
    {
        class node{
            public int val;
            public node left;
            public node right;
            public node(int value, node left = null, node right=null)
            {
                this.val=value;
                this.left= left;
                this.right =right;
            }
        }
        static void Main(string[] args)
        {
            var tree = new node(5, new node(4, new node(2), new node(3)), new node(6, new node(7), new node(8)));
            Console.WriteLine(PrintDistance(tree, 2, 3)); // 4 2
            Console.WriteLine(PrintDistance(tree, 8, 3)); // 2 4 5 6 7
            Console.WriteLine(PrintDistance(tree, 2, 7)); // 2 4 5 6 7 = 5 -1 =4

            var leetcode = new node(1, new node(2,null, new node(4)), new node(3));
            Console.WriteLine(PrintDistance(leetcode, 4, 3)); // 4 2
        }
        static string PrintDistance(node root, int a, int b){
            var common = lca(root, a, b);
            var path = new StringBuilder();
            if (common == null)
                return string.Empty;
            
            var pathFirst = new Stack<int>();
            var pathSecond = new Stack<int>();
            FillPath(common, a, pathFirst);
            FillPath(common, b, pathSecond);
            while(pathFirst.Count>0)
                path.Append(pathFirst.Pop());
            
            //reverse second path, we need to left the root, we already have
            while(pathSecond.Count>1)
                pathFirst.Push(pathSecond.Pop());

            while(pathFirst.Count>0)
                path.Append(pathFirst.Pop());

            return path.ToString();
        }

        private static bool FillPath(node root, int v, Stack<int> path){
            if(root == null)
                return false;
            path.Push(root.val);
            if(root.val == v)
                return true;
            
            if (FillPath(root.left, v, path))
                return true;
            
            while(path.Count > 0 && path.Peek() != root.val)
                path.Pop();

            return FillPath(root.right, v, path);
        }

        private static node lca(node root, int v1, int v2){
            if (root == null || root.val == v1 || root.val == v2)
                return root;
            
            node left = lca(root.left, v1, v2);
            node right = lca(root.right, v1, v2);

            return left == null ? right : right == null ? left : root;
        }
    }
}
