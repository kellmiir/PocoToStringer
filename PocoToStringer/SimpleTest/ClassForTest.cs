using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTest
{
    public enum TestEnum
    {
        FirstEnum,
        SecondEnum
    }
    public class MyClass
    {
        public  List<int> List { get; set; }= new List<int>{1,2,3,4};
        public  List<string> Listst { get; set; }= new List<string>{"fsdfsd","fdsfdsf","sdfdsf","dsfdsfds"};
        public  List<MyClass1> ListClass { get; set; }= new List<MyClass1>{new MyClass1(), new MyClass1(), new MyClass1()};
        public int Test { get; set; } = 893;
        public string Test2 { get; set; } = "SomeString";
        public double Test4 { get; set; } = 4.1;
        public TestEnum TestEnum { get; set; } = TestEnum.FirstEnum;
        public IPAddress IpAddress { get; set; } = IPAddress.Loopback;
        public MyClass1 Test5 { get; set; } = null;

        private string ListToString<T>(List<T> list)
        {
            StringBuilder sb = new StringBuilder(100);
            sb.Append("[");

            foreach (var val in list)
            {
                sb.Append(",");
              
               sb.Append(val);

            }
            sb.Remove(1, 1);
            sb.Append("]");

            return sb.ToString();
        }
        public override string ToString()
        {
            return $"{nameof(List)}: {ListToString(List)}, {nameof(Listst)}: {ListToString(Listst)}, {nameof(ListClass)}: {ListToString(ListClass)}, {nameof(Test)}: {Test}, {nameof(Test2)}: {Test2}, {nameof(Test4)}: {Test4}, {nameof(TestEnum)}: {TestEnum}, {nameof(IpAddress)}: {IpAddress}, {nameof(Test5)}: {Test5}";
        }
    }
    public class MyClass1
    {
        public int Test { get; set; } = 893;
        public string Test2 { get; set; } = "I am String";
        public string Test3 { get; set; } = "FDFDSFSDFSDFSDF";
        public int Test4 { get; set; } = 4;

        public override string ToString()
        {
            return $"Test: {Test}, Test2: {Test2}, Test3: {Test3}, Test4: {Test4}";
        }
    }
}
