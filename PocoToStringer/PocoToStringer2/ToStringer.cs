using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace PocoToStringer
{
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public static class ToStringer<T> where T : class
    {
        private static ToStringerInternal<T> s_toStringerInternal;

        static ToStringer()
        {
            var genericType = typeof(ToStringerInternal<>).MakeGenericType(typeof(T));
            s_toStringerInternal = (ToStringerInternal<T>)Activator.CreateInstance(genericType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetString(T value) => s_toStringerInternal.GetString(value);
    }

    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public static class ToStringer
    {
        private static ConcurrentDictionary<Type, IToStringer> dictionary;
        static ToStringer() => dictionary = new ConcurrentDictionary<Type, IToStringer>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetString(object value)
        {
            IToStringer toStringer;
            dictionary.TryGetValue(value.GetType(), out toStringer);
            if (toStringer != null)
            {
                return toStringer.GetString(value);
            }
            var genericType = typeof(ToStringerForDictionary<>).MakeGenericType(value.GetType());
            var instance = (IToStringer)Activator.CreateInstance(genericType);
            dictionary.TryAdd(value.GetType(), instance);
            return instance.GetString(value);
        }
    }
}