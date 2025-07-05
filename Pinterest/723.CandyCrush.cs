public class Solution
{
    private int m, n;

    public int[][] CandyCrush(int[][] board)
    {
        m = board.Length;
        n = board[0].Length;

        // Repeat until no more candies can be crushed
        while (!FindAndCrush(board))
        {
            Drop(board);
        }

        return board;
    }

    private bool FindAndCrush(int[][] board)
    {
        bool complete = true;

        // Check vertically adjacent candies
        for (int r = 1; r < m - 1; r++)
        {
            for (int c = 0; c < n; c++)
            {
                int val = Math.Abs(board[r][c]);
                if (val == 0) continue;

                if (Math.Abs(board[r - 1][c]) == val && Math.Abs(board[r + 1][c]) == val)
                {
                    board[r][c] = -val;
                    board[r - 1][c] = -Math.Abs(board[r - 1][c]);
                    board[r + 1][c] = -Math.Abs(board[r + 1][c]);
                    complete = false;
                }
            }
        }

        // Check horizontally adjacent candies
        for (int r = 0; r < m; r++)
        {
            for (int c = 1; c < n - 1; c++)
            {
                int val = Math.Abs(board[r][c]);
                if (val == 0) continue;

                if (Math.Abs(board[r][c - 1]) == val && Math.Abs(board[r][c + 1]) == val)
                {
                    board[r][c] = -val;
                    board[r][c - 1] = -Math.Abs(board[r][c - 1]);
                    board[r][c + 1] = -Math.Abs(board[r][c + 1]);
                    complete = false;
                }
            }
        }

        // Crush marked candies
        for (int r = 0; r < m; r++)
        {
            for (int c = 0; c < n; c++)
            {
                if (board[r][c] < 0)
                {
                    board[r][c] = 0;
                }
            }
        }

        return complete;
    }

    private void Drop(int[][] board)
    {
        for (int c = 0; c < n; c++)
        {
            int lowestZero = -1;

            for (int r = m - 1; r >= 0; r--)
            {
                if (board[r][c] == 0)
                {
                    lowestZero = Math.Max(lowestZero, r);
                }
                else if (lowestZero >= 0)
                {
                    // Swap current candy with the lowest zero
                    board[lowestZero][c] = board[r][c];
                    board[r][c] = 0;
                    lowestZero--;
                }
            }
        }
    }
}
