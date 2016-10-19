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


            var testclass = new MyClass() {};

            //ToStringerHolder.GetString(testclass);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine(ToStringerHolder.GetString(testclass));
            Console.WriteLine(testclass.ToString());
            for (int yi = 0; yi < 10; yi++)
            {
                for (int i = 0; i < 1000000; i++)
                {
                    //   var y = testclass.ToString();
                    var y = ToStringerHolder.GetString(testclass);
                    // Console.WriteLine(y);
                }
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            sw.Reset();
            sw.Start();
            for (int yi = 0; yi < 10; yi++)
            {
                for (int i = 0; i < 1000000; i++)
                {
                    //  var y = t.GetString(testclass);
                    var y = testclass.ToString();
                    // Console.WriteLine(y);
                }
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed.TotalMilliseconds);
        }
    }
}
