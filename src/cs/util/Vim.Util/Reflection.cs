using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Vim.Util
{
    public static class Reflection
    {
        public static IEnumerable<Type> GetAllSubclassesOf(this Assembly asm, Type t)
            => asm.GetTypes().Where(x => x.IsSubclassOf(t));

        /// <summary>
        /// Returns all instance fields, public and private.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable<FieldInfo> GetAllFields(this Type self)
            => self.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// Ignores the property or the field when they are being enumerated from a given object.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
        public class IgnoreInReflectedListAttribute : Attribute { }

        /// <summary>
        /// Converts fields to a dictionary of string/object pair.
        /// </summary>
        public static IDictionary<string, object> FieldsToDictionary(this object self)
            => self.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(fi => !fi.GetCustomAttributes<IgnoreInReflectedListAttribute>().Any())
                .ToDictionary(fi => fi.Name, fi => fi.GetValue(self));

        /// <summary>
        /// Converts properties to a dictionary of string/object pair.
        /// </summary>
        public static IDictionary<string, object> PropertiesToDictionary(this object self)
            => self.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(pi => !pi.GetCustomAttributes<IgnoreInReflectedListAttribute>().Any())
                .ToDictionary(pi => pi.Name, fi => fi.GetValue(self));

        /// <summary>
        /// Converts properties to an enumerable of strings.
        /// </summary>
        public static IEnumerable<string> PropertiesToStrings(this object self)
            => self.PropertiesToDictionary().Select(kv => $"{kv.Key}: {kv.Value}");
    }
}
