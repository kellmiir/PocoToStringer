# PocoToStringer [![NuGet Version](https://img.shields.io/nuget/v/PocoToStringer.svg?style=flat)](https://www.nuget.org/packages/PocoToStringer/)
  Create string presentation (as manual overriding method ToString) from simple objects

```csharp
    public class Test2Class
    {
        public int Integer { get; set; } = 234234;
        public double Double { get; set; } = 9.6;
    }
    public class TestClass
    {
        public string StringProperty { get; set; } = "I'am string";
        public IPAddress IpAddress { get; set; }=IPAddress.Loopback;
        public Test2Class Test2ClassProp { get; set; }= new Test2Class();
    }
```
  You can write only one row:
```csharp
ToStringer.GetString(new TestClass())
```
  and get a string like this -
"StringProperty: I'am string, IpAddress: 127.0.0.1, Test2ClassProp: Integer: 234234, Double: 9,6"
ToStringer.GetString use ConcurrentDictionary, so it have overhead. Especially in the case of multithreaded access.
You can use generic version of ToStringer he does not have such problems.
```csharp
ToStringer<TestClass>.GetString(new TestClass())
```
# Configuration
 You can change default string formatter:
 ```csharp
 PocoToStringerConfiguration.SetPocoFormatter<DefaultPocoFormatter>();
```
