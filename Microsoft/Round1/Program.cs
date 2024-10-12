using System;
using System.Text;
using NUnit.Framework;

namespace Round1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ConvertJson("Jan, 5; Feb, 10; June, 3; Jan, 100"));
        }

        //Runtime O(N) Space O(N)
        public static string ConvertJson(string s) {
            var dates = s.Split(";");
            int total = 0;
            StringBuilder result = new StringBuilder();
            result.Append("{\"Data\": [");
            foreach(string date in dates) {
                var aux = date.Split(",");
                int value = Convert.ToInt32(aux[1]);
                result.Append("{\"");
                result.Append($"{aux[0].Trim()}\":{value}");
                result.Append("},");
                total += value;
            }

            if(dates.Length>0)
                result.Length = result.Length-1;

            result.Append("],\"Total\":");
            result.Append(total);
            result.Append("}");

            return result.ToString();
        }
    }

    class ProgramTest
    {
        [TestCase("Jan, 5; Feb, 10; June, 3; Jan, 100")]
        public void ShouldPass(string s){
            var result = "{\"Data\": [{\"Jan\":5},{\"Feb\":10},{\"June\":3},{\"Jan\":100}],\"Total\":118}";
            Assert.AreEqual(result,Program.ConvertJson(s));
        }

    }
}
