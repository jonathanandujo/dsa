public int WidthOfBinaryTree(TreeNode root) {
    if (root == null) return 0;
    int maxWidth = 0;
    var queue = new Queue<(TreeNode node, long index)>();
    queue.Enqueue((root, 0));
    while (queue.Count > 0) { //cycle through levels
        int levelSize = queue.Count;
        long left = queue.Peek().index;
        long right = left;
        for (int i = 0; i < levelSize; i++) {
            var (node, index) =  queue.Dequeue();
            right = index;
            if (node.left != null) queue.Enqueue((node.left, 2 * index + 1));
            if (node.right != null) queue.Enqueue((node.right, 2 * index + 2)); }
        maxWidth = Math.Max(maxWidth, (int)(right - left + 1)); }
    return maxWidth; }
public bool SearchMatrix(int[][] matrix, int target) { //binary search
    int rows = matrix.Length;
    int cols = matrix[0].Length;
    int low = 0;
    int high = rows*cols-1;
    while(low<=high) {
        int mid = (low+high) /2; // large is low+(high-low)/2 for overflow
        int row = mid / cols;
        int col = mid % cols;
        if(matrix[row][col] == target)
            return true;
        else if(matrix[row][col] < target)
            low = mid+1;
        else
            high = mid-1; }
    return false; }
public bool WordBreak(string s, IList<string> wordDict) {
    bool[] store = new bool[s.Length + 1];
    store[^1] = true;
    for (int i = s.Length - 1; i >= 0 ; i--) {
        foreach (string word in wordDict) {
            if (i + word.Length <= s.Length && s.Substring(i, word.Length) == word) {
                store[i] = store[i + word.Length];
                if (store[i])
                    break;}}}
    return store[0];}
public void Merge(int[] nums1, int m, int[] nums2, int n) {
    int currentIndex = m+n-1;
    while(currentIndex >= 0 && n > 0)
        nums1[currentIndex--] = m>0 && nums1[m-1] > nums2[n-1] ? nums1[--m] : nums2[--n]; }
public IList<IList<int>> SubsetsWithDup(int[] nums) {
    Array.Sort(nums); // for duplicates only
    List<IList<int>> result = new();
    Backtracking(result, nums, new List<int>(), 0);
    return result;
    void Backtracking(List<IList<int>> result, int[] nums, List<int> current, int index) {
        result.Add(new List<int>(current));
        for(int i = index; i<nums.Length;i++) {
            if (i > index && nums[i] == nums[i - 1]) continue; // for duplicates, skip
            current.Add(nums[i]);
            Backtracking(result, nums, current, i+1);
            current.Remove(nums[i]);}}}
public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) {
    if (root == null || root == p || root == q) return root; //base code
    TreeNode left = LowestCommonAncestor(root.left, p, q);
    TreeNode right = LowestCommonAncestor(root.right, p, q);   
    return left == null ? right : right == null ? left : root; }
    

    
public Node insert(Node head, int insertVal) { //insert into circular linked list
    if (head == null) {
        Node n = new Node(insertVal);
        n.next = n;
        return n; }
    Node dummy = new Node(0);
    dummy.next = head;
    Node prev = dummy.next;
    head = head.next;
    while (dummy.next != head) {
        if (prev.val <= insertVal && insertVal <= head.val) {
            prev.next = new Node(insertVal, head);
            return dummy.next; }
        if (prev.val > head.val) {
            if (prev.val <= insertVal || insertVal <= head.val) {
            prev.next = new Node(insertVal, head);
            return dummy.next;}}
        prev = head;
        head = head.next;}
    prev.next = new Node(insertVal, head);
    return dummy.next;}
public int NumSubseq(int[] nums, int target) {
    Array.Sort(nums); // Sort the array in non-decreasing order
    int mod = 1000000007; // Initialize variables
    int n = nums.Length;
    int ans = 0;
    int[] pow2 = new int[n];// Calculate powers of 2 modulo mod
    pow2[0] = 1;
    for (int i = 1; i < n; i++) {
        pow2[i] = (pow2[i - 1] * 2) % mod;}
    int left = 0, right = n - 1; // Use two pointers to find subsequences
    while (left <= right) {
        if (nums[left] + nums[right] <= target) { //If the sum of the minimum and maximum values is less than or equal
            ans = (ans + pow2[right - left]) % mod; //to target, add the number of subsequences of length (right - left)
            left++; }
        else { // Otherwise, move the right pointer to the left
            right--;}
    return ans;}}
public int calculate(String s) {
    int first = 0, last = 0, num = 0;
    char operator = '+';
    String operands = "+-/*";
    for (int i = 0; i < s.length(); ++i) {
        char ch = s.charAt(i);
        if (Character.isDigit(ch)) {
            num = num * 10 + ch - '0'; } 
        if (i == s.length() - 1 || operands.contains(ch + "")) {
        switch(operator) {
            case '+':
            first += last;
            last = num;
            break;
            case '-':
            first += last;
            last = -num;
            break;
            case '*':
                last *= num;
            break;
            case '/':
                last /= num;}
        operator = ch;
        num = 0;}}
    return first + last;}