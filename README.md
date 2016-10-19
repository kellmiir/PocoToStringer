# PocoToStringer
  Create string presentation (as manual overriding method ToString) from simple objects

```csharp
 public class Iest2Class
    {
        public int Integer { get; set; } = 234234;
        public double Double { get; set; } = 9.6;
    }
    public class TestClass
    {
        public string StringProperty { get; set; } = "I'am string";
        public IPAddress IpAddress { get; set; }=IPAddress.Loopback;
        public Iest2Class Iest2ClassProp { get; set; }= new Iest2Class();
    }
```
  You can write only one row:
```csharp
ToStringerHolder.GetString(new TestClass())
```
  and get a string like this -
"StringProperty: I'am string, IpAddress: 127.0.0.1, Iest2ClassProp: Integer: 234234, Double: 9,6"

## Speed
  You can get test from [here](https://github.com/kellmiir/PocoToStringer/tree/master/PocoToStringer/SimpleTest).
  There’s a more complicated example on my computer [which can be viewed here](https://github.com/kellmiir/PocoToStringer/blob/master/PocoToStringer/SimpleTest/ClassForTest.cs) that performs 10 000 000 iterations during 16368 milliseconds while getting the same string using overriding method ToString took 17383 milliseconds
