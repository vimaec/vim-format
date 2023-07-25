using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Vim.Util
{
    public class EnumWithDescription
    {
        public Enum Value { get; set; }

        public string Description { get; set; }
    }

    public static class EnumWithDescriptionExtensions
    {
        public static string Description(this Enum value)
        {
            var attributes = value.GetType().GetField(value.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes?.FirstOrDefault() as DescriptionAttribute)?.Description ?? value.ToString();
        }

        public static IEnumerable<EnumWithDescription> GetAllValuesAndDescriptions(Type t)
        {
            return !t.IsEnum
                ? throw new ArgumentException($"{nameof(t)} must be an enum type")
                : Enum.GetValues(t).Cast<Enum>()
                    .Select((e) => new EnumWithDescription { Value = e, Description = e.Description() }).ToList();
        }
    }
}
