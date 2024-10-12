using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTreeAverageLevel
{
    class BinaryTree{
        public int val;
        public BinaryTree Left;
        public BinaryTree Right;
        public BinaryTree(int x){
            val=x;
        }

    }
    class LinkedList{
        LinkedList next;
        public int Cont { get => _cont; }
        int _cont=1;
        public int data;
        public LinkedList(int n){
            data=n;
        }
        public void Add(int n){
            LinkedList end = new LinkedList(n);
            LinkedList x = this;
            while(x.next != null){
                x=x.next;
            }
            x.next=end;
            _cont++;
        }
    }
    
    class Program
    {
        private static Hashtable data = new Hashtable();
        static void Main(string[] args)
        {
            var tree = new BinaryTree(4){Left = new BinaryTree(7){ Left = new BinaryTree(10), Right = new BinaryTree(2){ Right = new BinaryTree(6){Left = new BinaryTree(2)}}},Right = new BinaryTree(9){ Right = new BinaryTree(6)}};
            Traversal(tree,0);
            for(int i =0; i< data.Count; i++){
                var listBylevel = (List<int>)data[i];
                // var total = 0;
                // for(int j=0; j<listBylevel.Count;j++){
                //     total+=listBylevel[j];
                // }
                //Console.WriteLine(total/listBylevel.Count);
                Console.WriteLine(listBylevel.Sum()/listBylevel.Count);
            }
            // LinkedList some = new LinkedList(1);
            // some.Add(2);
            // some.Add(3);
            // some.Add(4);
            // Console.WriteLine(some.Cont);
            Console.WriteLine("Hello World!");
        }
        static void Traversal(BinaryTree node, int level){
            if (node != null){
                if (!data.ContainsKey(level))
                    data.Add(level,new List<int>(){node.val});
                else {
                    var aux = (List<int>)data[level];
                    aux.Add(node.val);
                }
                Traversal(node.Left, level+1);
                Traversal(node.Right, level+1);
            }
        }
    }
}
