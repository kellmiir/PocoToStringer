using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace PocoToStringer
{
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
    internal static class MethodInfoHolder
    {
        internal static MethodInfo MethodAddString = typeof(CheckNullCallToString).GetMethod("AddString", new[] { typeof(object) });
        internal static MethodInfo MethodStringConcat = typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });
        internal static MethodInfo MethodGetStringFromDictionary = typeof(ToStringer<>).GetMethod("GetString");
        internal static MethodInfo MethodGetStringFormatterAdd = typeof(StringFormatter).GetMethod("Add", new[] { typeof(string), typeof(string) });
    }
}