using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Vim.Util
{
    /// <summary>
    /// A collection of extension functions and utilities
    /// </summary>
    public static class Util
    {
        public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> tuple, out T1 key, out T2 value)
        {
            key = tuple.Key;
            value = tuple.Value;
        }

        /// <summary>
        /// Returns a shallow clone of the given object's properties.
        /// </summary>
        public static T ShallowClone<T>(T obj) where T : class, new()
        {
            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var clonedObj = new T();

            foreach (var property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    var value = property.GetValue(obj);
                    property.SetValue(clonedObj, value);
                }
            }

            return clonedObj;
        }

        /// <summary>
        /// Returns the result of the getValue function. If getValue throws an exception, the onExceptionAction is invoked and the defaultValue is returned.
        /// </summary>
        public static T TryGetValue<T>(Func<T> getValue, T defaultValue, Action<Exception> onExceptionAction = null)
        {
            try
            {
                return getValue();
            }
            catch (Exception e)
            {
                onExceptionAction?.Invoke(e);
            }

            return defaultValue;
        }
    }
}
