using System;
using NUnit.Framework;

namespace PalindromeArray
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(IsPalindrome("testingnitset".ToCharArray()));
            Console.WriteLine(IsPalindrome("testing a simple input".ToCharArray()));
            Console.WriteLine(IsPalindrome("123454321".ToCharArray()));
            Console.WriteLine(IsPalindrome("1234554321".ToCharArray()));
        }
        public static bool IsPalindrome(char[] s){
            for(int i =0;i<s.Length/2;i++)
                if(s[i] != s[^(i+1)])
                    return false;

            return true;
        }
    }

    [TestFixture]
    class ProgramTests
    {
        [TestCase("testingnitset")]
        [TestCase("123454321")]
        [TestCase("1234554321")]
        public void Should_Return_True(string s){
            var result = Program.IsPalindrome(s.ToCharArray());
            Assert.AreEqual(true, result);
        }

        [TestCase("testing a simple input")]
        public void Should_Return_False(string s){
            var result = Program.IsPalindrome(s.ToCharArray());
            Assert.AreEqual(false,result);
        }

        [TestCase]
        public void Can_Be_Used_By_Testing_NUnit(){
            Assert.True(true);
            Assert.True(true);
            Assert.True(true);
        }
    }
}
