using System;
using System.Collections.Generic;

namespace BinaryTreePath
{
    class Program
    {
        public class TreeNode{
            public int value {get;set;}
            public TreeNode left {get;set;}
            public TreeNode right {get;set;}
            public TreeNode(int v){
                value = v;
            }
        }
        static void Main(string[] args)
        {
            /*

             5
            / \
           4   6
          / \   \
         2   8   9
            / \
           1   7

            */
            var root = new TreeNode(5);
            root.left = new TreeNode(4) {left = new TreeNode(2), right = new TreeNode(8){left = new TreeNode(1), right = new TreeNode(7)}};
            root.right = new TreeNode(6){ right = new TreeNode(9)};
            //list to return
            var result = new List<int>();
            Find(root,result,1,2);
            foreach(int e in result){
                Console.WriteLine(e);
            }
        }
        static bool Find(TreeNode parent, List<int> path, int start, int end){
            //doesn't have values
            if (parent == null)
                return false;
                
            if(parent.value == start || parent.value == end){
                //found first
                //if(path.Count==0)
                    path.Add(parent.value);
                return true;
            }
            else{
                if (Find(parent.left, path, start,end)){
                    //reverse adding
                    // if(path.Count==0)
                    //     path.Add(parent.left.value);
                    path.Add(parent.value);
                    if(Find(parent.right,path,start, end)){
                        return false;
                    }
                    return true;
                }
                else{
                    if (Find(parent.right, path, start, end)){
                        path.Add(parent.value);
                        return true;
                    }
                    return false;
                }
            }
        }
    }
}
