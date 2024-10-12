using System;

namespace DistanceBetween2NodesTree
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
            Console.WriteLine(FindDistance(tree, 4, 2)); // 4 2 = 2-1=1
            Console.WriteLine(FindDistance(tree, 4, 20)); // not possible -1
            Console.WriteLine(FindDistance(tree, 2, 7)); // 2 4 5 6 7 = 5 -1 =4
        }
        static int FindDistance(node root, int a, int b){
            var common = lca(root, a, b);
            if (common != null)
                return Findlevel(common,a,0) + Findlevel(common,b,0);
            return -1;
        }

        private static int Findlevel(node root, int v, int level){
            if(root == null)
                return -1;
            if(root.val == v)
                return level;
            
            int left = Findlevel(root.left, v, level+1);
            if(left == -1)
                return Findlevel(root.right, v, level+1);
            
            return left;
        }

        private static node lca(node root, int v1, int v2){
            if(root == null || root.val == v1 || root.val == v2)
                return root;
            
            node left = lca(root.left, v1, v2);
            node right = lca(root.right, v1, v2);
            
            if (left != null && right != null)
            {
                return root;
            }
            else if (left == null && right == null)
            {
                return null;
            }
            else if (left != null)
            {
                return lca(root.left, v1, v2);
            }
            else
            {
                return lca(root.right, v1, v2);
            }
        }
    }
}
