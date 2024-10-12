using System;
using BinaryTree;

namespace Binarytree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinTree binaryTree = new BinTree();
            binaryTree.Add(1);
            binaryTree.Add(2);
            binaryTree.Add(7);
            binaryTree.Add(3);
            binaryTree.Add(10);
            binaryTree.Add(5);
            binaryTree.Add(8);
            binaryTree.Add(9);
            binaryTree.Add(4);
            binaryTree.Add(21);
            binaryTree.Add(-2);
            binaryTree.Add(11);
            binaryTree.Add(6);
            binaryTree.Add(22);
            
            Node node = binaryTree.Find(5);
            
            Console.WriteLine($"Depth:");
            binaryTree.Depth();
            Console.WriteLine();
            
            Console.WriteLine("PreOrder Traversal:");
            binaryTree.TraversePreOrder(binaryTree.Root);
            Console.WriteLine();
            
            Console.WriteLine("InOrder Traversal:");
            binaryTree.TraverseInOrder(binaryTree.Root);
            Console.WriteLine();
            
            Console.WriteLine("PostOrder Traversal:");
            binaryTree.TraversePostOrder(binaryTree.Root);
            Console.WriteLine();

            Console.WriteLine("Max Width:");
            binaryTree.MaxWidth();
            Console.WriteLine();
            
            binaryTree.Remove(7);
            binaryTree.Remove(8);
            
            Console.WriteLine("PreOrder Traversal After Removing Operation:");
            binaryTree.TraversePreOrder(binaryTree.Root);
            Console.WriteLine();
            
            Console.ReadLine();
        }
    }
}
