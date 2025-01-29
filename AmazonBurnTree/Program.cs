/*
Q2) Burn the binary tree starting from the target node




Given a binary tree and target node. By giving the fire to the target node and fire starts to spread in a complete tree. The task is to print the sequence of the burning nodes of a binary tree.
Rules for burning the nodes : 

* Fire will spread constantly to the connected nodes only.
* Every node takes the same time to burn.
* A node burns only once.

Examples: DFS
14 21 24 10 15 12 22 23 13

Input : 
                       12
                     /     \
                   13       10
                          /     \
                       14       15
                      /   \     /  \
                     21   24   22   23
target node = 14
Output :
14
21, 24, 10
15, 12
22, 23, 13

--14 21 24 10 15 22 23 12 13
Explanation : First node 14 burns then it gives fire to it's 
neighbors(21, 24, 10) and so on. This process continues until 
the whole tree burns.

Input :
                       12
                     /     \
                  19        82
                 /        /     \
               41       15       95
                 \     /         /  \
                  2   21        7   16
target node = 41
Output :
41
2, 19
12
82
15, 95
21, 7, 16

}






Input : 
                       12
                     /     \
                   13       X
                          /     \
                       X       15
                      /   \     /  \
                     X   X   22   23
                     
                     
                    
                    */

namespace AmazonBurnTree;                    
public static class Program
{
    static void Main()
    {
        Node root = new Node(12);
        root.left = new Node(13);
        root.right = new Node(10);
        root.right.left = new Node(14);
        root.right.right = new Node(15);
        root.right.left.left = new Node(21);
        root.right.left.right = new Node(24);
        root.right.right.left = new Node(22);
        root.right.right.right = new Node(23);

        int target = 14;
        burnTree(root, target);
    }
    // A Tree node
    public class Node {
        public int key;
        public Node left;
        public Node right;
        
        Node() {}
        public Node(int key) { this.key = key; }
        Node(int key, Node left, Node right)
        {
            this.key = key;
            this.left = left;
            this.right = right;
        }
    }
        //Input fuction
        
    static void burnTree(Node root, int target)
    {
        if (root == null)
            return;
        Queue<Node> queue = new();
        HashSet<int> burned = new();
        Dictionary<Node, Node> parentMap = new(); //Mistake 1: forgot to add the parents
        
        //search for the target //Mistake 2: not added findTarget because lack of time
        Node targetNode = findTargetNode(root, target, null, parentMap);
        if (targetNode == null)
            return;
        //enqueue node, then enqueue neighbours
        queue.Enqueue(targetNode); //14
        //burn nodes chaos
        int levelCounter = queue.Count; //1  output: 14- 21 24 10 -15 12 - 22 23 - 13
        while(levelCounter > 0)
        {
            Node element = queue.Dequeue();
            if(burned.Add(element.key)) // avoiding reenqueue burned elements
            {
                Console.Write($"{element.key} ");//Mistake 3:, must write only if was not burned
                if(element.left != null)
                    queue.Enqueue(element.left);
                if(element.right != null)
                    queue.Enqueue(element.right);
                if (parentMap.ContainsKey(element)) //Mistake 4: not added previously
                    queue.Enqueue(parentMap[element]);
            }
            levelCounter--;
            
            if(levelCounter == 0)
            {
                Console.WriteLine();
                levelCounter = queue.Count; // for burning next level
            }
        }
    }

    static Node findTargetNode(Node root, int target, Node parent, Dictionary<Node, Node> parentMap) //Mistake 5: not time for this function
    {
        if (root == null)
            return null;

        if (parent != null)
            parentMap[root] = parent;

        if (root.key == target)
            return root;

        Node left = findTargetNode(root.left, target, root, parentMap);
        if (left != null)
            return left;

        return findTargetNode(root.right, target, root, parentMap);
    }
}

                   /*  
                         12
                     /     \
                   13       10
                          /     \
                       14       15
                      /   \     /  \
                     21   24   22   23
                     
                     Output :
14
21, 24, 10
15, 12
22, 23, 13

*/