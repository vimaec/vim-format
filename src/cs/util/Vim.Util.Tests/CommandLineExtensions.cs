using CommandLine;
using System.Collections.Generic;
using System.Linq;

namespace Vim.Util.Tests;

public static class CommandLineExtensions
{
    /// <summary>
    /// Returns the name of the Verb attribute on the given object.
    /// If no Verb attribute exists, returns null.
    /// </summary>
    public static string GetVerb<T>(this T obj)
    {
        var attributes = obj.GetType().GetCustomAttributes(false);
        foreach (var attr in attributes)
        {
            if (!(attr is VerbAttribute verbAttribute))
                continue;

            return verbAttribute.Name;
        }

        return null;
    }

    /// <summary>
    /// Returns a collection of strings defining the ordered values.
    /// </summary>
    public static IEnumerable<string> GetValueArgs<T>(this T obj)
    {
        var values = new List<(int, string)>();

        foreach (var prop in obj.GetType().GetProperties())
        {
            var value = prop.GetValue(obj)?.ToString() ?? "";

            foreach (var attr in prop.GetCustomAttributes(false))
            {
                if (!(attr is ValueAttribute valueAttribute))
                    continue;

                values.Add((valueAttribute.Index, $"\"{value}\""));
            }
        }

        return values.OrderBy(item => item.Item1).Select(item => item.Item2);
    }

    /// <summary>
    /// Returns a collection of strings defining the option flags and their values.
    /// </summary>
    public static IEnumerable<string> GetOptionArgs<T>(this T obj)
    {
        var options = new List<string>();

        foreach (var prop in obj.GetType().GetProperties())
        {
            var value = prop.GetValue(obj)?.ToString();
            if (value == null)
                continue;

            foreach (var attr in prop.GetCustomAttributes(false))
            {
                if (!(attr is OptionAttribute option))
                    continue;

                var opt = !string.IsNullOrEmpty(option.LongName)
                    ? $"--{option.LongName}"
                    : !string.IsNullOrEmpty(option.ShortName)
                        ? $"-{option.ShortName}"
                        : null;

                if (opt == null)
                    continue;

                options.Add($"{opt} \"{value}\"");
            }
        }

        return options.OrderBy(o => o);
    }

    /// <summary>
    /// Converts the given converter options into a collection of strings representing the command line input.
    /// </summary>
    public static IEnumerable<string> ToArgList<T>(this T obj)
    {
        var result = new List<string>();

        var verb = obj.GetVerb();
        if (!string.IsNullOrEmpty(verb))
            result.Add(verb);

        var args = obj.GetValueArgs();
        result.AddRange(args);

        var options = obj.GetOptionArgs();
        result.AddRange(options);

        return result;
    }
}
