using System;

namespace BinaryTreeMaxDeepth
{
      class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var tree = new TreeNode(3){left= new TreeNode(9), right = new TreeNode(20){left = new TreeNode(15), right= new TreeNode(7)}};
            var tree2 = new TreeNode(1){right = new TreeNode(2)};
            Console.WriteLine(MaxDepth(tree));
            Console.WriteLine(MaxDepth(tree2));
        }

        static int MaxDepth(TreeNode root) {
            if(root == null)
                return 0;
            
            if(root.left == null && root.right == null)
                return 1;
            
            return Math.Max(MaxDepth(root.right)+1,MaxDepth(root.left)+1);
        }

    }
}
