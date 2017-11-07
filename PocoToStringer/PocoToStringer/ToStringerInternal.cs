using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

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
        public string Unwrap<TEnumerable>(TEnumerable enumerable) where TEnumerable:IEnumerable 
        {
            StringBuilder sb = new StringBuilder(200);
            sb.Append("[");
            foreach (var val in enumerable)
            {
                sb.Append(PocoToStringerConfiguration.ArraySeparator);
                if (val.GetType().IsClass)
                {
                    sb.Append(ToStringer.GetString(val));
                }
                else sb.Append(val);
            }
            sb.Remove(1, 1).Append("]");
            return sb.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Expression GetToStringExpression(MemberExpression memberExpression)
        {
            var type = memberExpression.Type;
            if (type.Name!="String" && type.FindInterfaces((type1, criteria) =>type1.Name.Contains("IEnumerable"),memberExpression).Length>0)
            {

              return  Expression.Call(Expression.Constant(this),GetType().GetMethod("Unwrap").MakeGenericMethod(type), memberExpression);

            }
            if (!type.IsClass)
            {
                return Expression.Call(memberExpression, "ToString", null, null);
            }
            if (type.Assembly.Location.IndexOf("Microsoft.Net", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return Expression.Call(MethodInfoHolder.MethodAddString, memberExpression);
            }
            return Expression.Condition(Expression.Equal(memberExpression.Expression, Expression.Constant(null)),
                Expression.Call(typeof(ToStringer<>).MakeGenericType(type).GetMethod("GetString"), memberExpression), Expression.Constant(""));
           
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
