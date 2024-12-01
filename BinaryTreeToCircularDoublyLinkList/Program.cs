// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// Build the tree
Tree tree = new Tree();
tree.root = new Node(10);
tree.root.left = new Node(12);
tree.root.right = new Node(15);
tree.root.left.left = new Node(25);
tree.root.left.right = new Node(30);
tree.root.right.left = new Node(36);

Node head = tree.BTreeToCList(tree.root);
tree.display(head);


class Node
{
    public int val;
    public Node? left, right;
    public Node(int val)
    {
        this.val = val;
        left = right = null;
    }
}

class Tree {
    public Node? root;
    public Tree()
    {
        root = null;
    }

    public virtual Node concatenate(Node leftList, Node rigthList)
    {
        if (leftList == null)
            return rigthList;
        if (rigthList == null)
            return leftList;

        Node? leftLast = leftList.left;
        Node? rightLast = rigthList.left;

        leftLast.right = rigthList;
        rigthList.left = leftLast;

        leftList.left = rightLast;
        rightLast.right = leftList;

        return leftList;
    }

    public virtual Node BTreeToCList(Node root)
    {
        if (root == null)
            return null;

        Node left = BTreeToCList(root.left);
        Node right = BTreeToCList(root.right);

        root.left = root.right = root;

        return concatenate(concatenate(left, root), right);
    }

    public virtual void display(Node head)
    {
        Console.WriteLine("Circular Linked List is : ");
        Node itr = head;
        do
        {
            Console.Write(itr.val + " ");
            itr = itr.right;
        } while (itr != head);
    }
}