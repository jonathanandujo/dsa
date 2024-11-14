// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var root = new TreeNode(4);
root.left = new TreeNode(2);
root.right = new TreeNode(5);
root.left.left = new TreeNode(1);
root.left.right = new TreeNode(3);
var target = 3.714286;
Console.WriteLine(ClosestValue(root, target));

root = new TreeNode(1);
target = 4.428571;
Console.WriteLine(ClosestValue(root, target));


static int ClosestValue(TreeNode root, double target) {
    var closest = root.val;
    while (root != null) {
        if (Math.Abs(target - root.val) < Math.Abs(target - closest)) {
            closest = root.val;
        }
        root = target < root.val ? root.left : root.right;
    }
    return closest;
}



class TreeNode {
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null) {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}