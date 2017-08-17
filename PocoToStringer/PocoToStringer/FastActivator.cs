using System;
using System.Linq.Expressions;

namespace PocoToStringer
{
    //https://blogs.msdn.microsoft.com/seteplia/2017/02/01/dissecting-the-new-constraint-in-c-a-perfect-example-of-a-leaky-abstraction/
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
}