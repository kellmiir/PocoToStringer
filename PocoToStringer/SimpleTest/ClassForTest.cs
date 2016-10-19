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
        public int Test { get; set; } = 893;
        public string Test2 { get; set; } = "SomeString";
        public double Test4 { get; set; } = 4.1;
        public TestEnum TestEnum { get; set; } = TestEnum.FirstEnum;
        public IPAddress IpAddress { get; set; } = IPAddress.Loopback;
        public MyClass1 Test5 { get; set; } = new MyClass1();

        public override string ToString()
        {
            return $"Test: {Test}, Test2: {Test2}, Test4: {Test4}, TestEnum: {TestEnum}, IpAddress: {IpAddress}, Test5: {Test5} ";
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
