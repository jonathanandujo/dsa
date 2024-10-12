using System;
using System.Text;

namespace SmallestStringFromLeafBT
{
    class Program
    {

        public class TreeNode {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }
        static void Main(string[] args)
        {
            var root = new TreeNode(0, new TreeNode(1, new TreeNode(3), new TreeNode(4)),new TreeNode(2, new TreeNode(3), new TreeNode(4)));
            Console.WriteLine(SmallestFromLeaf(root));
        }

        private static string result = "";
        public static string SmallestFromLeaf(TreeNode root) {
            dfs(root, new StringBuilder());
            return result;
        }
        
        private static void dfs(TreeNode root, StringBuilder s){
            if (root == null)
                return;
            s.Append((char)('a'+root.val));
            
            if(root.left == null && root.right == null) {
                string tmp = Reverse(s.ToString());
                if (string.IsNullOrEmpty(result))
                    result = tmp;
                else if(string.Compare(tmp,result)<0)
                    result=tmp;
            }
            
            dfs(root.left, s);
            dfs(root.right, s);
            s.Remove(s.Length-1,1);
        }
        
        private static string Reverse(string s){
            var storage = new StringBuilder();
            for(int i=s.Length-1; i>=0; i--)
                storage.Append(s[i]);
            return storage.ToString();
        }
    }
}
