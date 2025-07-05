public class Solution
{
    public bool HasPath(int[][] maze, int[] start, int[] destination)
    {
        int m = maze.Length;
        int n = maze[0].Length;
        bool[,] visited = new bool[m, n];
        int[] dirX = new int[] { 0, 1, 0, -1 };
        int[] dirY = new int[] { -1, 0, 1, 0 };

        var queue = new Queue<int[]>();
        queue.Enqueue(start);
        visited[start[0], start[1]] = true;

        while (queue.Count > 0)
        {
            var curr = queue.Dequeue();
            if (curr[0] == destination[0] && curr[1] == destination[1])
                return true;

            for (int i = 0; i < 4; i++)
            {
                int r = curr[0];
                int c = curr[1];

                // Roll the ball until it hits a wall
                while (r >= 0 && r < m && c >= 0 && c < n && maze[r][c] == 0)
                {
                    r += dirX[i];
                    c += dirY[i];
                }

                // Step back to the last valid position
                r -= dirX[i];
                c -= dirY[i];

                if (!visited[r, c])
                {
                    visited[r, c] = true;
                    queue.Enqueue(new int[] { r, c });
                }
            }
        }

        return false;
    }
}
