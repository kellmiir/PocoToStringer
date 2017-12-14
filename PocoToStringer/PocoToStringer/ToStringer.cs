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
        public static string GetString(T value) => value != null ? value.GetType().Name == "String" ? value.ToString() : s_toStringerInternal.GetString(value) : String.Empty;
    }

    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public static class ToStringer
    {
        private static ConcurrentDictionary<Type, IToStringer> dictionary;
        static ToStringer() => dictionary = new ConcurrentDictionary<Type, IToStringer>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetString(object value)
        {
            var valueType = value.GetType();
            if (valueType.Name == "String") return value.ToString();
            dictionary.TryGetValue(valueType, out var toStringer);
            if (toStringer != null)
            {
                return toStringer.GetString(value);
            }
            var genericType = typeof(ToStringerForDictionary<>).MakeGenericType(valueType);
            var instance = (IToStringer)Activator.CreateInstance(genericType);
            dictionary.TryAdd(valueType, instance);
            return instance.GetString(value);
        }
    }
}