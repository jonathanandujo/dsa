using System;
using System.Collections;
using System.Diagnostics;

namespace Facebook
{
    class Program
    {
        private static ArrayList arr = new ArrayList();
        private static Random rdn = new Random();
        static void fillList(int numbers){
            for (int i=0;i<numbers;i++)
            arr.Add(rdn.Next(-10,10));
            // arr.Add(4);
            // arr.Add(-3);
            // arr.Add(5);
            // arr.Add(-2);
            // arr.Add(-1);
            // arr.Add(2);
            // arr.Add(6);
            // arr.Add(-2);
        }
        //int[] arr = new int[]{4,-3,5,-2,-1,2,6,-2};

        static void example1(){
            Stopwatch sw = new Stopwatch();
            fillList(100000);    
            var suma = 0;
            var maxs=0;
            sw.Start();
            for(int i=0; i< arr.Count; i++){
                suma=0;
                // for(int j=i; j< arr.Count; j++){
                //     suma+=(int)arr[j];
                //     if(suma>maxs)
                //         maxs=suma;
                // }
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedTicks);
            Console.WriteLine(maxs);
            suma = 0;
            maxs=0;
            sw.Restart();
            for(int i=0; i< arr.Count; i++){
                suma += (int)arr[i];
                if (suma > maxs)
                maxs= suma;
                else if(suma < 0)
                suma=0;
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedTicks);
            Console.WriteLine(maxs);
        }

        static void recursion(){
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int numb = 42;
            Console.WriteLine(fibbona(numb));
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            // Console.WriteLine(factorialif(numb));
            // sw.Stop();
            // Console.WriteLine(sw.ElapsedMilliseconds);
            // sw.Restart();
            // Console.WriteLine(factorial(numb));
            // sw.Stop();
            // Console.WriteLine(sw.ElapsedMilliseconds);
        }
        static int fibbona(int number){
            if (number > 1)
                number = fibbona(number -1) + fibbona(number - 2);
            else
                number = 1;
            return number;
        }
        static int factorial(int number){
            var result = 1;
            if (number > 0)
                result = number * factorial(number-1);
            return result;
        }

        static int factorialif(int number){
            var result = 0;
            if (number > 0)
                result = number * factorialif(number-1);
            if(number == 1)
                result =1;
            return result;
        }

        
        static void Main(string[] args)
        {
            testLinkedList();
        }
        static void testLinkedList(){
            var a = new CListaLigada();
            a.Add(1);
            a.Add(2);
            a.Add(3);
            a.Add(5);
            a.Add(4);
            a.Transversa();
            Console.WriteLine(a.Find(4));
            Console.WriteLine(a.FindIndex(-1));
            Console.WriteLine(a.FindPreview(11));
            a.Delete(7);
            a.Transversa();
            Console.WriteLine(a.GetByIndex(4));
        }
    }
    class CNodo {
        private int dato;
        private CNodo siguiente=null;
        public int Dato{get => dato; set => dato=value;}
        internal CNodo Siguiente {get=> siguiente; set=> siguiente=value;}
        public override string ToString(){
            return string.Format("[{0}]",dato);
        }
    }

    class CListaLigada{
        private CNodo header;
        private CNodo worker;
        private CNodo worker2;
        public CListaLigada(){
            header = new CNodo();
            header.Siguiente = null;
        }
        public void Add(int d) {
            worker=header;
            while(worker.Siguiente != null){
                worker= worker.Siguiente;
            }
            CNodo tmp = new CNodo();
            tmp.Dato=d;
            worker.Siguiente=tmp;
        }
        public void Add(int d, int where){
            worker = Find(where);
            if (worker == null)
                return;
            
            CNodo tmp = new CNodo();
            tmp.Dato=d;
            tmp.Siguiente = worker.Siguiente;
            worker.Siguiente = tmp;
        }
        public void AddStart(int d){
            CNodo tmp = new CNodo();
            tmp.Dato = d;
            tmp.Siguiente = header.Siguiente;
            header.Siguiente=tmp;
        }
        public CNodo GetByIndex(int index){
            int tmpIndex = -1;
            var tmpNode = header;
            while (tmpNode.Siguiente != null){
                tmpNode = tmpNode.Siguiente;
                tmpIndex++;

                if (tmpIndex == index)
                    return tmpNode;
            }
            return null;
        }
        public void Transversa()
        {
            worker = header;
            while(worker.Siguiente != null){
                worker=worker.Siguiente;

                int d = worker.Dato;
                Console.Write("{0}, ",d);
            }
            Console.WriteLine();
        }
        public void Clear(){
            header.Siguiente = null;
        }
        public bool HasValue(){
            if (header.Siguiente != null)
                return true;
            return false;
        }
        public void Delete(int d){
            if (HasValue()){
                CNodo last = FindPreview(d);
                last.Siguiente = last.Siguiente?.Siguiente;
            }
        }
        public CNodo FindIndex(int index){
            if(HasValue()){
                worker=header;
                var internalIndex = 0;
                while(worker.Siguiente != null){
                    worker=worker.Siguiente;
                    if(internalIndex == index)
                        return worker;
                    internalIndex++;
                }
            }
            return null;
        }
        public CNodo Find(int d){
            if(HasValue()){
                worker2 = header;

                while(worker2.Siguiente!=null){
                    worker2 = worker2.Siguiente;
                    if(worker2.Dato == d)
                        return worker2;
                }
            }
            return null;
        }
        public CNodo FindPreview(int d){
            worker2 = header;
            while(worker2.Siguiente != null && worker2.Siguiente.Dato != d)
            {
                worker2 = worker2.Siguiente;
            }
            return worker2;
        }
    }

}