using System;
using NUnit.Framework;

namespace CustomSortString
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static string CustomSortString(string order, string str) {
            //for quick order-number
            var storage = new int[26];
            for (int i = 0; i < order.Length; i++)
                storage[order[i]-'a'] = i+1;
            
            var strArr = str.ToCharArray();
            for (int i = 1; i < strArr.Length; i++) {
                int nVal = storage[strArr[i]-'a'];
                char c = strArr[i];
                int j = i-1;
                
                while (j >=0 && storage[strArr[j]-'a'] > nVal) {
                    strArr[j+1] = strArr[j];
                    j--;
                }
                strArr[j+1] = c;
            }
            return new string(strArr);
        }
    }

    class ProgramTest 
    {
        [TestCase("cba","abcd")]
        public void ShouldReturnSorted(string order, string str) {
            var result = Program.CustomSortString(order, str);
            Assert.AreEqual("dcba",result);
        }
    }
}
