using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PocoToStringer
{
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    internal sealed class ToStringerForDictionary<T> : IToStringer where T : class
    {
        private ToStringerInternal<T> toStringerInternal;
        public ToStringerForDictionary() => toStringerInternal = new ToStringerInternal<T>();
        public string GetString(object value) => toStringerInternal.GetString((T)value);

    }
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    internal class ToStringerInternal<T> where T : class
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Expression GetToStringExpression(MemberExpression memberExpression)
        {
            if (!memberExpression.Type.IsClass)
            {
                return Expression.Call(memberExpression, "ToString", null, null);
            }
            if (memberExpression.Type.Assembly.Location.IndexOf("Microsoft.Net", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return Expression.Call(MethodInfoHolder.MethodAddString, memberExpression);
            }
            return Expression.Condition(Expression.Equal(memberExpression.Expression, Expression.Constant(null)),
                Expression.Call(typeof(ToStringer<>).MakeGenericType(memberExpression.Type).GetMethod("GetString"), memberExpression), Expression.Constant(""));
           
        }


        private Func<T, IPocoFormatter, string> _func;
        public ToStringerInternal()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "T");
            ParameterExpression sum = Expression.Parameter(typeof(string), "stringForReturn");
            ParameterExpression stringFromatter = Expression.Parameter(typeof(IPocoFormatter), "stringFormatter");
            Expression block = Expression.Block(
                new[] { sum }, Expression.Block(typeof(T).GetProperties()
                    .Select(info => Expression.Property(parameter, info.Name))
                    .Select(
                        info => Expression.Call(stringFromatter, "Add", new Type[] { }, GetToStringExpression(info), Expression.Constant(info.Member.Name))
                    )), Expression.Call(stringFromatter, "ToString", null, null));
            if (block.CanReduce) block = block.Reduce();
            _func = Expression.Lambda<Func<T, IPocoFormatter, string>>(block, parameter, stringFromatter)
                .Compile();
        }
        public string GetString(T value) => _func(value, PocoToStringerConfiguration.GetPocoFormatter());
    }
}
