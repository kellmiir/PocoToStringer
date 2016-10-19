using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace PocoToStringer
{
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    internal static class CheckNullCallToString
    {
        private static Func<object, string> _toStringFunc;
        static CheckNullCallToString()
        {
            ParameterExpression propertyInfo = Expression.Parameter(typeof(object), "pi");
            var toStringExpression = Expression.Call(propertyInfo, "ToString", null, null);
            _toStringFunc = Expression.Lambda<Func<object, string>>(toStringExpression, propertyInfo).Compile();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string AddString(object toAdd) => toAdd == null ? String.Empty : _toStringFunc(toAdd);
    }
}