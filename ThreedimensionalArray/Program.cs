using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ThreedimensionalArray
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = 30;
            var sizeMb = 10000000;
            var supreme = new string[size][][];
            // while (true) Process.Start(Assembly.GetExecutingAssembly().Location);
            for ( int i = 0; i < sizeMb; i++) {
                Marshal.AllocHGlobal(1024);
            }

            for (int i = 0; i < size; i++) {
                supreme[i] = new string[size][];
                for (int j = 0; j < size; j++) {
                    supreme[i][j] = new string[size];
                    for (int k = 0; k < size; k++) {
                        supreme[i][j][k] = Guid.NewGuid().ToString();
                    }
                }
            }
            Console.WriteLine("finish");
            Console.ReadLine();
        }
    }
}
