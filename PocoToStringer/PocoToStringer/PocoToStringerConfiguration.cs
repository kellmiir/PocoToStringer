using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//https://blogs.msdn.microsoft.com/seteplia/2017/02/01/dissecting-the-new-constraint-in-c-a-perfect-example-of-a-leaky-abstraction/
namespace PocoToStringer
{
    public static class FastActivator
    {
        public static T CreateInstance<T>() where T : new()
        {
            return FastActivatorImpl<T>.NewFunction();
        }

        private static class FastActivatorImpl<T> where T : new()
        {
            private static readonly Expression<Func<T>> NewExpression = () => new T();
            public static readonly Func<T> NewFunction = NewExpression.Compile();
        }
    }

    public class InternalFormatterCreater
    {
        public InternalFormatterCreater()
        {

        }

    }
    public static class PocoToStringerConfiguration
    {
        private static Func<IPocoFormatter> s_formatterCreater;

        public static void SetPocoFormatter<T>() where T : IPocoFormatter, new() =>
            s_formatterCreater = (dynamic)FastActivatorImpl<T>.NewFunction;

        static PocoToStringerConfiguration() => SetPocoFormatter<DefaultPocoFormatter>();

        public static IPocoFormatter GetPocoFormatter() => s_formatterCreater();
        private static class FastActivatorImpl<T> where T : new()
        {
            private static readonly Expression<Func<T>> NewExpression = () => new T();
            public static readonly Func<T> NewFunction = NewExpression.Compile();
        }
    }
}
