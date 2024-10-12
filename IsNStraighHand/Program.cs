using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace IsNStraighHand
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(IsNStraightHand(new int[]{2,1},2));
            Console.WriteLine(IsNStraightHand(new int[]{1,2,3,6,2,3,4,7,8},3));
            Console.WriteLine(IsNStraightHand(new int[]{1,2,3,4,5},4));
        }
        public static bool IsNStraightHand(int[] hand, int groupSize) {
            if (hand.Length % groupSize != 0) return false;
            var storage = new SortedDictionary<int,int>();
            foreach(int i in hand)
                if(storage.ContainsKey(i))
                    storage[i]++;
                else
                    storage.Add(i, 1);
            
            for(int i = 0; i < hand.Length/groupSize; i++) {
                var first = storage.First().Key;
                for(int j = 0; j < groupSize; j++) {
                    if(storage.ContainsKey(first)) {
                        storage[first]--;
                        if(storage[first] == 0)
                            storage.Remove(first);
                        first += 1;
                    }
                    else {
                        return false;
                    }
                }
            }
            
            return true;
        }
    }
    class ProgramTest{
        [TestCase(new int[]{1,2,3,6,2,3,4,7,8},3)]
        [TestCase(new int[]{1,2},2)]
        [TestCase(new int[]{2,1},2)]
        public void Should_Return_True(int[] hand, int groupSize){
            var result = Program.IsNStraightHand(hand,groupSize);
            Assert.AreEqual(true,result);
        }

        [TestCase(new int[]{1,2,3,4,5},4)]
        public void Should_Return_False(int[] hand, int groupSize){
            var result = Program.IsNStraightHand(hand,groupSize);
            Assert.AreEqual(false,result);
        }
    }
}
