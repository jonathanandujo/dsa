public class SparseMatrix
{
    public int rows;
    public int cols;
    public Dictionary<(int, int), int> data;
    public SparseMatrix(int[][] matrix)
    {
        rows = matrix.Length;
        cols = matrix[0].Length;
        data = new();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (matrix[i][j] != 0)
                {
                    data[(i, j)] = matrix[i][j];
                }
            }
        }
    }

    public void Print()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (data.ContainsKey((i, j)))
                {
                    Console.Write(data[(i, j)] + "\t");
                }
                else
                {
                    Console.Write("0\t");
                }
            }
            Console.WriteLine();
        }
    }

    public void Add(SparseMatrix matrix)
    {
        foreach (var kvp in matrix.data)
        {
            if (data.ContainsKey(kvp.Key))
            {
                data[kvp.Key] += kvp.Value;
            }
            else
            {
                data[kvp.Key] = kvp.Value;
            }
        }
    }

    public void Multiply(SparseMatrix matrix)
    {
        if (cols != matrix.rows)
        {
            throw new InvalidOperationException("Matrix dimensions do not match for multiplication.");
        }

        var result = new SparseMatrix(new int[matrix.rows][]);
        for (int i = 0; i < matrix.rows; i++)
        {
            result.data[(i, 0)] = 0; // Initialize row
            for (int j = 0; j < matrix.cols; j++)
            {
                int sum = 0;
                for (int k = 0; k < cols; k++)
                {
                    sum += (data.ContainsKey((i, k)) ? data[(i, k)] : 0) * (matrix.data.ContainsKey((k, j)) ? matrix.data[(k, j)] : 0);
                }
                if (sum != 0)
                {
                    result.data[(i, j)] = sum;
                }
            }
        }
        this.data = result.data;
    }
}


public class Program
{
    public static void Main()
    {
        // Example usage of SparseMatrix
        int[][] matrix =
        [
            new int[] { 0, 0, 3, 0 },
            new int[] { 4, 0, 0, 0 },
            new int[] { 0, 5, 0, 6 }
        ];
        int[][] matrix2 =
        [
            new int[] { 0, 0, 1, 0 },
            new int[] { 0, 2, 0, 0 },
            new int[] { 0, 0, 0, 3 }
        ];
        SparseMatrix sparseMatrix = new SparseMatrix(matrix);
        sparseMatrix.Print();
        SparseMatrix sparseMatrix2 = new SparseMatrix(matrix2);
        sparseMatrix2.Print();
        sparseMatrix.Add(sparseMatrix2);
        sparseMatrix.Print();
        
        Console.WriteLine("\n=== Running SparseMatrix Test Cases ===\n");
        RunSparseMatrixTests();
        
        Console.WriteLine("\n=== Running SparseMatrix Multiplication Test Cases ===\n");
        RunMultiplicationTests();
    }
    
    

    public static void RunSparseMatrixTests()
    {
        var testCases = new List<(string testName, int[][] matrix1, int[][] matrix2, int[][] expected)>
        {
            // Test Case 1: Basic addition
            (
                "Basic Addition Test",
                new int[][]
                {
                    new int[] { 1, 0, 3 },
                    new int[] { 0, 2, 0 }
                },
                new int[][]
                {
                    new int[] { 0, 1, 0 },
                    new int[] { 2, 0, 1 }
                },
                new int[][]
                {
                    new int[] { 1, 1, 3 },
                    new int[] { 2, 2, 1 }
                }
            ),
            
            // Test Case 2: Zero matrix addition
            (
                "Zero Matrix Addition",
                new int[][]
                {
                    new int[] { 1, 2 },
                    new int[] { 3, 4 }
                },
                new int[][]
                {
                    new int[] { 0, 0 },
                    new int[] { 0, 0 }
                },
                new int[][]
                {
                    new int[] { 1, 2 },
                    new int[] { 3, 4 }
                }
            ),
            
            // Test Case 3: Sparse matrices with no overlap
            (
                "Non-overlapping Sparse Matrices",
                new int[][]
                {
                    new int[] { 1, 0, 0 },
                    new int[] { 0, 0, 0 },
                    new int[] { 0, 0, 3 }
                },
                new int[][]
                {
                    new int[] { 0, 2, 0 },
                    new int[] { 0, 0, 4 },
                    new int[] { 0, 0, 0 }
                },
                new int[][]
                {
                    new int[] { 1, 2, 0 },
                    new int[] { 0, 0, 4 },
                    new int[] { 0, 0, 3 }
                }
            ),
            
            // Test Case 4: Single element matrices
            (
                "Single Element Matrices",
                new int[][]
                {
                    new int[] { 5 }
                },
                new int[][]
                {
                    new int[] { 3 }
                },
                new int[][]
                {
                    new int[] { 8 }
                }
            ),
            
            // Test Case 5: Larger sparse matrix
            (
                "Larger Sparse Matrix",
                new int[][]
                {
                    new int[] { 0, 0, 0, 5 },
                    new int[] { 2, 0, 0, 0 },
                    new int[] { 0, 0, 7, 0 },
                    new int[] { 0, 1, 0, 0 }
                },
                new int[][]
                {
                    new int[] { 1, 0, 0, 0 },
                    new int[] { 0, 3, 0, 0 },
                    new int[] { 0, 0, 0, 4 },
                    new int[] { 0, 0, 0, 2 }
                },
                new int[][]
                {
                    new int[] { 1, 0, 0, 5 },
                    new int[] { 2, 3, 0, 0 },
                    new int[] { 0, 0, 7, 4 },
                    new int[] { 0, 1, 0, 2 }
                }
            )
        };

        int testNumber = 1;
        int passedTests = 0;

        foreach (var (testName, matrix1, matrix2, expected) in testCases)
        {
            Console.WriteLine($"Running Test {testNumber}: {testName}");

            try
            {
                // Create sparse matrices
                SparseMatrix sm1 = new SparseMatrix(matrix1);
                SparseMatrix sm2 = new SparseMatrix(matrix2);

                // Perform addition
                sm1.Add(sm2);

                // Verify result
                bool testPassed = VerifyMatrix(sm1, expected);

                if (testPassed)
                {
                    Console.WriteLine($"✓ Test {testNumber} PASSED");
                    passedTests++;
                }
                else
                {
                    Console.WriteLine($"✗ Test {testNumber} FAILED");
                    Console.WriteLine("Expected:");
                    PrintMatrix(expected);
                    Console.WriteLine("Actual:");
                    sm1.Print();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Test {testNumber} FAILED with exception: {ex.Message}");
            }

            Console.WriteLine();
            testNumber++;
        }

        Console.WriteLine($"=== Test Summary ===");
        Console.WriteLine($"Passed: {passedTests}/{testCases.Count}");
        Console.WriteLine($"Success Rate: {(double)passedTests / testCases.Count * 100:F1}%");

        // Additional edge case tests
        RunEdgeCaseTests();
    }
    
    public static void RunEdgeCaseTests()
    {
        Console.WriteLine("\n=== Running Edge Case Tests ===");
        
        // Test 1: Constructor with all zeros
        try
        {
            int[][] allZeros = {
                new int[] { 0, 0, 0 },
                new int[] { 0, 0, 0 }
            };
            SparseMatrix sm = new SparseMatrix(allZeros);
            Console.WriteLine("✓ All-zero matrix construction test PASSED");
            
            // Verify no elements are stored
            if (sm.data.Count == 0)
            {
                Console.WriteLine("✓ All-zero matrix storage optimization PASSED");
            }
            else
            {
                Console.WriteLine("✗ All-zero matrix storage optimization FAILED");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ All-zero matrix test FAILED: {ex.Message}");
        }
        
        // Test 2: Adding matrix to itself
        try
        {
            int[][] selfMatrix = {
                new int[] { 1, 0, 2 },
                new int[] { 0, 3, 0 }
            };
            SparseMatrix sm = new SparseMatrix(selfMatrix);
            int originalCount = sm.data.Count;
            
            sm.Add(sm); // Add to itself
            
            // Check if values doubled correctly
            bool selfAdditionCorrect = true;
            foreach (var kvp in sm.data)
            {
                int expectedValue = selfMatrix[kvp.Key.Item1][kvp.Key.Item2] * 2;
                if (kvp.Value != expectedValue)
                {
                    selfAdditionCorrect = false;
                    break;
                }
            }
            
            if (selfAdditionCorrect)
            {
                Console.WriteLine("✓ Self-addition test PASSED");
            }
            else
            {
                Console.WriteLine("✗ Self-addition test FAILED");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Self-addition test FAILED: {ex.Message}");
        }
        
        Console.WriteLine();
    }
    
    public static void RunMultiplicationTests()
    {
        var multiplicationTestCases = new List<(string testName, int[][] matrix1, int[][] matrix2, int[][] expected)>
        {
            // Test Case 1: Basic 2x2 multiplication
            (
                "Basic 2x2 Matrix Multiplication",
                new int[] []
                {
                    new int[] { 1, 2 },
                    new int[] { 3, 4 }
                },
                new int[] []
                {
                    new int[] { 2, 0 },
                    new int[] { 1, 2 }
                },
                new int[] []
                {
                    new int[] { 4, 4 },
                    new int[] { 10, 8 }
                }
            ),
            
            // Test Case 2: Sparse matrix multiplication
            (
                "Sparse Matrix Multiplication",
                new int[] []
                {
                    new int[] { 1, 0, 3 },
                    new int[] { 0, 2, 0 }
                },
                new int[] []
                {
                    new int[] { 2, 0 },
                    new int[] { 0, 1 },
                    new int[] { 1, 0 }
                },
                new int[] []
                {
                    new int[] { 5, 0 },
                    new int[] { 0, 2 }
                }
            ),
            
            // Test Case 3: Identity matrix multiplication
            (
                "Identity Matrix Multiplication",
                new int[] []
                {
                    new int[] { 3, 5 },
                    new int[] { 1, 2 }
                },
                new int[] []
                {
                    new int[] { 1, 0 },
                    new int[] { 0, 1 }
                },
                new int[] []
                {
                    new int[] { 3, 5 },
                    new int[] { 1, 2 }
                }
            ),
            
            // Test Case 4: Zero matrix multiplication
            (
                "Zero Matrix Multiplication",
                new int[] []
                {
                    new int[] { 1, 2 },
                    new int[] { 3, 4 }
                },
                new int[] []
                {
                    new int[] { 0, 0 },
                    new int[] { 0, 0 }
                },
                new int[] []
                {
                    new int[] { 0, 0 },
                    new int[] { 0, 0 }
                }
            ),
            
            // Test Case 5: Single element multiplication
            (
                "Single Element Matrix Multiplication",
                new int[] []
                {
                    new int[] { 5 }
                },
                new int[] []
                {
                    new int[] { 3 }
                },
                new int[] []
                {
                    new int[] { 15 }
                }
            ),
            
            // Test Case 6: Rectangular matrix multiplication (3x2) * (2x4)
            (
                "Rectangular Matrix Multiplication",
                new int[] []
                {
                    new int[] { 1, 2 },
                    new int[] { 0, 3 },
                    new int[] { 4, 0 }
                },
                new int[] []
                {
                    new int[] { 2, 1, 0, 3 },
                    new int[] { 0, 2, 1, 0 }
                },
                new int[] []
                {
                    new int[] { 2, 5, 2, 3 },
                    new int[] { 0, 6, 3, 0 },
                    new int[] { 8, 4, 0, 12 }
                }
            ),
            
            // Test Case 7: Very sparse matrix multiplication
            (
                "Very Sparse Matrix Multiplication",
                new int[] []
                {
                    new int[] { 0, 0, 5 },
                    new int[] { 0, 0, 0 },
                    new int[] { 2, 0, 0 }
                },
                new int[] []
                {
                    new int[] { 0, 3 },
                    new int[] { 0, 0 },
                    new int[] { 1, 0 }
                },
                new int[] []
                {
                    new int[] { 5, 0 },
                    new int[] { 0, 0 },
                    new int[] { 0, 6 }
                }
            )
        };

        int testNumber = 1;
        int passedTests = 0;

        foreach (var (testName, matrix1, matrix2, expected) in multiplicationTestCases)
        {
            Console.WriteLine($"Running Multiplication Test {testNumber}: {testName}");

            try
            {
                // Create sparse matrices
                SparseMatrix sm1 = new SparseMatrix(matrix1);
                SparseMatrix sm2 = new SparseMatrix(matrix2);
                
                Console.WriteLine("Matrix 1:");
                sm1.Print();
                Console.WriteLine("Matrix 2:");
                sm2.Print();

                // Perform multiplication
                sm1.Multiply(sm2);
                
                Console.WriteLine("Result:");
                sm1.Print();

                // Verify result
                bool testPassed = VerifyMatrix(sm1, expected);

                if (testPassed)
                {
                    Console.WriteLine($"✓ Multiplication Test {testNumber} PASSED");
                    passedTests++;
                }
                else
                {
                    Console.WriteLine($"✗ Multiplication Test {testNumber} FAILED");
                    Console.WriteLine("Expected:");
                    PrintMatrix(expected);
                    Console.WriteLine("Actual:");
                    sm1.Print();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Multiplication Test {testNumber} FAILED with exception: {ex.Message}");
            }

            Console.WriteLine();
            testNumber++;
        }

        Console.WriteLine($"=== Multiplication Test Summary ===");
        Console.WriteLine($"Passed: {passedTests}/{multiplicationTestCases.Count}");
        Console.WriteLine($"Success Rate: {(double)passedTests / multiplicationTestCases.Count * 100:F1}%");

        // Test dimension mismatch error
        RunMultiplicationErrorTests();
    }
    
    public static void RunMultiplicationErrorTests()
    {
        Console.WriteLine("\n=== Running Multiplication Error Tests ===");
        
        // Test dimension mismatch
        try
        {
            int[][] incompatibleMatrix1 = {
                new int[] { 1, 2, 3 },
                new int[] { 4, 5, 6 }
            }; // 2x3 matrix
            
            int[][] incompatibleMatrix2 = {
                new int[] { 1, 2 },
                new int[] { 3, 4 }
            }; // 2x2 matrix (should be 3xN for compatibility)
            
            SparseMatrix sm1 = new SparseMatrix(incompatibleMatrix1);
            SparseMatrix sm2 = new SparseMatrix(incompatibleMatrix2);
            
            sm1.Multiply(sm2); // This should throw an exception
            Console.WriteLine("✗ Dimension mismatch test FAILED - Exception not thrown");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"✓ Dimension mismatch test PASSED - Exception caught: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Dimension mismatch test FAILED - Wrong exception type: {ex.Message}");
        }
        
        // Test valid multiplication after dimension check
        try
        {
            int[][] compatibleMatrix1 = {
                new int[] { 1, 2 },
                new int[] { 3, 4 }
            }; // 2x2 matrix
            
            int[][] compatibleMatrix2 = {
                new int[] { 5, 6 },
                new int[] { 7, 8 }
            }; // 2x2 matrix
            
            SparseMatrix sm1 = new SparseMatrix(compatibleMatrix1);
            SparseMatrix sm2 = new SparseMatrix(compatibleMatrix2);
            
            sm1.Multiply(sm2);
            Console.WriteLine("✓ Compatible dimensions test PASSED");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Compatible dimensions test FAILED: {ex.Message}");
        }
    }
    
    public static bool VerifyMatrix(SparseMatrix sparseMatrix, int[][] expected)
    {
        if (sparseMatrix.rows != expected.Length || sparseMatrix.cols != expected[0].Length)
        {
            return false;
        }
        
        for (int i = 0; i < sparseMatrix.rows; i++)
        {
            for (int j = 0; j < sparseMatrix.cols; j++)
            {
                int actualValue = sparseMatrix.data.ContainsKey((i, j)) ? sparseMatrix.data[(i, j)] : 0;
                if (actualValue != expected[i][j])
                {
                    return false;
                }
            }
        }
        
        return true;
    }
    
    public static void PrintMatrix(int[][] matrix)
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                Console.Write(matrix[i][j] + "\t");
            }
            Console.WriteLine();
        }
    }
}


