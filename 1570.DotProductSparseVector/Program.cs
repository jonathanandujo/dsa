// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
SparseVector v1 = new SparseVector(new int[] { 1, 0, 0, 2, 3 });
SparseVector v2 = new SparseVector(new int[] { 0, 3, 0, 4, 0 });
Console.WriteLine(v1.DotProduct(v2)); // return 8

SparseVectorArray va1 = new SparseVectorArray(new int[] { 1, 0, 0, 2, 3 });
SparseVectorArray va2 = new SparseVectorArray(new int[] { 0, 3, 0, 4, 0 });
Console.WriteLine(va1.DotProduct(va2)); // return 8


class SparseVector
{
    private Dictionary<int, int> _vector;

    public SparseVector(int[] nums)
    {
        _vector = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != 0)
            {
                _vector.Add(i, nums[i]);
            }
        }
    }

    // Return the dotProduct of two sparse vectors
    public int DotProduct(SparseVector vec)
    {
        int result = 0;
        foreach (var item in _vector)
        {
            if (vec._vector.ContainsKey(item.Key))
            {
                result += item.Value * vec._vector[item.Key];
            }
        }
        return result;
    }
}

class SparseVectorArray
{
    private int[] _indexes;
    private int[] _values;

    public SparseVectorArray(int[] nums)
    {
        List<int> indexes = new();
        List<int> values = new();
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != 0)
            {
                indexes.Add(i);
                values.Add(nums[i]);
            }
        }
        _indexes = indexes.ToArray();
        _values = values.ToArray();
    }

    // Return the dotProduct of two sparse vectors
    public int DotProduct(SparseVectorArray vec)
    {
        int result = 0;
        int i = 0, j = 0;
        while (i < _indexes.Length && j < vec._indexes.Length)
        {
            if (_indexes[i] == vec._indexes[j])
            {
                result += _values[i] * vec._values[j];
                i++;
                j++;
            }
            else if (_indexes[i] < vec._indexes[j])
            {
                i++;
            }
            else
            {
                j++;
            }
        }
        return result;
    }
}