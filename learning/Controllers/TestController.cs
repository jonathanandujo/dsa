using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using learning.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace learning.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("bitarray")]
        public ActionResult bitarray()
        {
            BitArray myarray = new BitArray(new byte[] {1,2,3,4,5,6,40,255});
            BitArray myarray2 = (BitArray)myarray.Clone();
            myarray.SetAll(true);

            return Ok(myarray.Count);
        }

        [HttpGet("hashtable")]
        public ActionResult hashtable()
        {
            Hashtable result = new Hashtable();
            result.Add("some",1);
            result.Add(1,"some");
            foreach (string ll in result.Keys){
                Console.WriteLine(ll);
            }
            return Ok(result["some"]);
        }

        [HttpGet("queue")]
        public ActionResult queue()
        {
            Queue result = new Queue();
            result.Enqueue("s");
            result.Enqueue("o");
            result.Enqueue("m");
            return Ok(result.Peek());
        }

        [HttpGet("sortedList")]
        public ActionResult sortedList()
        {
            SortedList result = new SortedList();
            result.Add("a","1");
            result.Add("2","a");
            return Ok(result.ContainsValue("1"));
        }

        [HttpGet("stack")]
        public ActionResult stack()
        {
            Stack result = new Stack();
            result.Push("S");
            result.Push("O");
            result.Push("M");
            result.Push("E");
            result.Peek();
            return Ok(result);
        }

        [HttpGet("arrayList")]
        public ActionResult arrayList()
        {
            ArrayList result = new ArrayList();
            result.Add(1);
            result.Add(2);
            result.Add(3);
            result.Add(4);
            result.Add("some");
            return Ok(result.Contains("some"));
        }

        [HttpGet("list")]
        public ActionResult list()
        {
            List<int> result = new List<int>();
            result.Add(1);
            result.Add(2);
            result.Add(3);
            result.Add(4);
            result.Add(4);
            result.Remove(4);
            result.Reverse();
            result.Sort();
            result.Insert(0,150);
            return Ok(result);
        }

        [HttpGet("sortedSet")]
        public ActionResult sortedSet()
        {
            SortedSet<int> result = new SortedSet<int>();
            result.Add(1);
            result.Add(2);
            result.Add(3);
            result.Add(4);
            return Ok(result.Reverse());
        }

        [HttpGet("delegatest")]
        public ActionResult delegatest()
        {
            var a = new Delegatest();
            a.test();
            var result = 0;
            return Ok(result);
        }

        [HttpGet("perf")]
        public ActionResult perf()
        {
            int[] arr = new int[]{4,-3,5,-2,-1,2,6,-2};
            
            /*for(int i=0; i<= arr.Length; i++){
                for(int j=i; j<= arr.Length; j++){

                }
            }*/
            var suma = 0;
            var maxs=0;
            for(int i=0; i< arr.Length; i++){
                suma += arr[i];
                if (suma > maxs)
                maxs= suma;
                else
                suma=0;
            }
            return Ok(maxs);
        }
    }
}

