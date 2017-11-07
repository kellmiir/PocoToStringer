using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PocoToStringer
{
    public static class PocoToStringerConfiguration
    {
        private static Func<IPocoFormatter> s_formatterCreater;

        public static void SetPocoFormatter<T>() where T : IPocoFormatter, new() =>
            s_formatterCreater = (Func<IPocoFormatter>)(object)(FastActivatorImpl<T>.NewFunction);

        static PocoToStringerConfiguration() => SetPocoFormatter<DefaultPocoFormatter>();
        public static string ArraySeparator { get; set; } = ",";
        public static IPocoFormatter GetPocoFormatter() => s_formatterCreater();
        private static class FastActivatorImpl<T> where T : new()
        {
            private static readonly Expression<Func<T>> s_newExpression = () => new T();
            public static readonly Func<T> NewFunction = s_newExpression.Compile();
        }
    }
}
