using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace PocoToStringer
{
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public static class ToStringerHolder
    {
        private static ConcurrentDictionary<Type, IToStringer> dictionary;

        static ToStringerHolder()
        {
            dictionary = new ConcurrentDictionary<Type, IToStringer>();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetString(object value)
        {
            IToStringer toStringer;
            dictionary.TryGetValue(value.GetType(), out toStringer);
            if (toStringer != null)
            {
                return toStringer.GetString(value);
            }
            var genericType = typeof(ToStringer<>).MakeGenericType(value.GetType());
            var instance = (IToStringer)Activator.CreateInstance(genericType);
            dictionary.TryAdd(value.GetType(), instance);
            return instance.GetString(value);
        }
    }
}