using System;

namespace Meta
{
    /**

Structure:
sdsdsdsdsd
  


Tell him hashMap approach first.

later, tell him 

class Tuple {
     int[] arrayOfIndexes; //-- stores are the indexes, which contains non-zeross
     int[] arrayOfValues; // -- stores the values
  }

We will only store non-zeroes

see here, type the two arrays here

type in below one dude, it will answer him

0->1
1->2

indexes1 -> 0,1,5,7
values1  -> 1,2,1,2

indexes2 -> 0,5,6,6
values1  -> 1,4,1,1
 


*/
    public class SparseVector
    {
        public class ValueObject
        {
            public int[] Index { get; set; }
            public int[] Value { get; set; }
        }

        public static int DotProduct(ValueObject vector1, ValueObject vector2)
        {
            int first = 0, second = 0;
            int product = 0;

            while (first < vector1.Index.Length && second < vector2.Index.Length)
            {
                if (vector1.Index[first] == vector2.Index[second])
                {
                    product += vector1.Value[first] * vector2.Value[second];
                    first++;
                    second++;
                }
                else if (vector1.Index[first] < vector2.Index[second])
                {
                    first++;
                }
                else
                {
                    second++;
                }
            }

            return product;
        }

        public static void Main(string[] args)
        {
            int[] index1 = { 0, 1, 3, 4 };
            int[] value1 = { 1, 2, 4, 0 };

            int[] index2 = { 0, 1, 2, 4 };
            int[] value2 = { 1, 12, 4, 1 };

            ValueObject valueObject1 = new ValueObject
            {
                Index = index1,
                Value = value1
            };

            ValueObject valueObject2 = new ValueObject
            {
                Index = index2,
                Value = value2
            };

            Console.WriteLine(DotProduct(valueObject1, valueObject2));
        }
    }
}

