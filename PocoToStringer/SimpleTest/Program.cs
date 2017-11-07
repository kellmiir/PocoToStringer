using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocoToStringer;

namespace SimpleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Test();
            Console.ReadKey();
        }

        public void Test()
        {

            PocoToStringerConfiguration.SetPocoFormatter<DefaultPocoFormatter>();
            PocoToStringerConfiguration.ArraySeparator = "|";
            var testclass = new MyClass() { };

            Stopwatch sw = new Stopwatch();
            Console.WriteLine(ToStringer<MyClass>.GetString(testclass));
            Console.WriteLine(ToStringer.GetString(testclass));
            Console.WriteLine(testclass.ToString());
            sw.Start();
            for (int i = 0; i < 2000000; i++)
            {
                var y = ToStringer<MyClass>.GetString(testclass);
                Console.WriteLine(y);
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            sw.Reset();
            sw.Start();
            for (int i = 0; i < 2000000; i++)
            {
                var y = ToStringer.GetString(testclass);
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            sw.Reset();
            sw.Start();
            for (int i = 0; i < 2000000; i++)
            {
                var y = testclass.ToString();
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            Console.ReadKey();
        }
    }
}
