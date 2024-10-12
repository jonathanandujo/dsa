using System;

namespace learning.Classes{
    public delegate void MyDelegate(string p);
    public class Delegatest{
        public void test(){
            var del = new MyDelegate(HandlerString);
            del("test");
        }
        private static void some(){

        }
        static void HandlerString(string p){
            Console.WriteLine(p);
            }
    }
}