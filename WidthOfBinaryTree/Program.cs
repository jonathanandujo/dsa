// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
TreeNode root = new TreeNode(1,new TreeNode(3, new TreeNode(5), new TreeNode(3)),new TreeNode(2, null, new TreeNode(9)));
Console.WriteLine(WidthOfBinaryTree(root));

int WidthOfBinaryTreeLC(TreeNode root)
{
    if (root == null) return 0;

        int maxWidth = 0;
        var queue = new Queue<(TreeNode node, long index)>();
        queue.Enqueue((root, 0));

        while (queue.Count > 0) {
            int levelSize = queue.Count;
            long left = queue.Peek().index;
            long right = left;

            for (int i = 0; i < levelSize; i++) {
                var (node, index) =  queue.Dequeue();
                right = index;

                if (node.left != null) queue.Enqueue((node.left, 2 * index + 1));
                if (node.right != null) queue.Enqueue((node.right, 2 * index + 2));
            }

            maxWidth = Math.Max(maxWidth, (int)(right - left + 1));
        }

        return maxWidth;
}

int WidthOfBinaryTree(TreeNode root)
{
    int left = 0;
    int right = 0;
    Stack<(TreeNode node, int level)> store = new();
    store.Push((root, 0));
    while (store.Count > 0)
    {
        (TreeNode node, int level) = store.Pop();
        if (node.left != null)
            store.Push((node.left, level - 1));
        if (node.right != null)
            store.Push((node.right, level + 1));
        left = Math.Min(left, level);
        right = Math.Max(right, level);
    }
    return right - left;
}

public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}