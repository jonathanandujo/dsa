public class Solution
{
    class SparseMatrix
    {
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public List<int> Values = new List<int>();
        public List<int> ColIndex = new List<int>();
        public List<int> RowIndex = new List<int>();

        // Compressed Sparse Row (CSR)
        public SparseMatrix(int[][] matrix)
        {
            Rows = matrix.Length;
            Cols = matrix[0].Length;
            RowIndex.Add(0);

            for (int row = 0; row < Rows; ++row)
            {
                for (int col = 0; col < Cols; ++col)
                {
                    if (matrix[row][col] != 0)
                    {
                        Values.Add(matrix[row][col]);
                        ColIndex.Add(col);
                    }
                }
                RowIndex.Add(Values.Count);
            }
        }

        // Compressed Sparse Column (CSC)
        public SparseMatrix(int[][] matrix, bool colWise)
        {
            Rows = matrix.Length;
            Cols = matrix[0].Length;
            ColIndex.Add(0);

            for (int col = 0; col < Cols; ++col)
            {
                for (int row = 0; row < Rows; ++row)
                {
                    if (matrix[row][col] != 0)
                    {
                        Values.Add(matrix[row][col]);
                        RowIndex.Add(row);
                    }
                }
                ColIndex.Add(Values.Count);
            }
        }
    }

    public int[][] Multiply(int[][] mat1, int[][] mat2)
    {
        var A = new SparseMatrix(mat1);
        var B = new SparseMatrix(mat2, true);

        int[][] result = new int[mat1.Length][];
        for (int i = 0; i < result.Length; i++)
            result[i] = new int[mat2[0].Length];

        for (int row = 0; row < result.Length; ++row)
        {
            for (int col = 0; col < result[0].Length; ++col)
            {
                int aStart = A.RowIndex[row];
                int aEnd = A.RowIndex[row + 1];
                int bStart = B.ColIndex[col];
                int bEnd = B.ColIndex[col + 1];

                while (aStart < aEnd && bStart < bEnd)
                {
                    int aCol = A.ColIndex[aStart];
                    int bRow = B.RowIndex[bStart];

                    if (aCol < bRow)
                    {
                        aStart++;
                    }
                    else if (aCol > bRow)
                    {
                        bStart++;
                    }
                    else
                    {
                        result[row][col] += A.Values[aStart] * B.Values[bStart];
                        aStart++;
                        bStart++;
                    }
                }
            }
        }

        return result;
    }
}
