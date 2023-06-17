using System;
using System.Collections.Generic;
using System.Linq;

namespace Vim.Format.Utils
{
    public static class PipeSeparatedStrings
    {
        /// <summary>
        /// Joins the given strings with the pipe character "|".<br/>
        /// <list type="bullet">
        /// <item>Pipe characters among the part strings will be escaped with the backslash character "\" in the resulting joined string.</item>
        /// <item>Backslash characters among the part strings will be escaped with an additional backslash character "\" in the resulting joined string.</item>
        /// </list>
        /// </summary>
        public static string Join(params string[] parts)
            => parts == null || parts.Length == 0 ? null : string.Join("|", parts.Select(p
                => p?.Replace(@"\", @"\\").Replace("|", @"\|")));

        /// <summary>
        /// Splits the given pipe-separated string into its items.<br/>
        /// Escaped pipes or backslashes in the joined string will be un-escaped in the resulting items.<br/>
        /// Returns null if the input string is null.
        /// </summary>
        public static string[] Split(string joined)
        {
            if (joined == null)
                return null;

            if (joined == "")
                return new[] { "" };

            var parts = new List<string>();
            var tail = 0;
            int head;
            var backSlashCounter = 0;
            var separatorCounter = 0;
            for (head = 0; head < joined.Length; ++head)
            {
                var c = joined[head];
                switch (c)
                {
                    case '\\':
                        backSlashCounter++;
                        break;
                    case '|':
                        {
                            if (backSlashCounter % 2 == 0)
                            {
                                // even number of backslashes encountered; this pipe is a separator.
                                parts.Add(joined.Substring(tail, head - tail));
                                tail = head + 1;
                                separatorCounter++;
                            }
                            else
                            {
                                // odd number of backslashes encountered; this pipe is escaped.
                            }
                            backSlashCounter = 0;
                            break;
                        }
                    default:
                        backSlashCounter = 0;
                        break;
                }
            }
            if (tail != head)
                parts.Add(joined.Substring(tail, head - tail));

            if (separatorCounter == parts.Count)
                parts.Add("");

            // Unescape the parts.
            for (var j = 0; j < parts.Count; ++j)
            {
                parts[j] = parts[j].Replace(@"\|", "|").Replace(@"\\", @"\");
            }

            return parts.ToArray();
        }

        /// <summary>
        /// A lazy PipeSeparatedStrings parser that delays allocating string until needed.
        /// </summary>
        public class Parser
        {
            private readonly int[] _indices;
            private readonly bool[] _escape;
            private string _value;
            private int _count;

            public int GetCount() => _count;

            public Parser(int max)
            {
                _indices = new int[max];
                _escape = new bool[max];
            }

            public void Parse(string value)
            {
                _value = value;

                if (_value == null)
                {
                    _count = 0;
                    return;
                }

                _count = 1;
                if (_value.Length == 0)
                    return;

                var backSlashCounter = 0;
                for (var i = 0; i < _value.Length; ++i)
                {
                    var c = _value[i];
                    switch (c)
                    {
                        case '\\':
                            backSlashCounter++;
                            _escape[_count - 1] = true;
                            break;
                        case '|':
                            {
                                if (backSlashCounter % 2 == 0)
                                {
                                    // even number of backslashes encountered; this pipe is a separator.
                                    _indices[_count++] = i;
                                        
                                    // ignore all remaining pipes if max provided was too small.
                                    if (_count >= _indices.Length)
                                        return;
                                }
                                else
                                {
                                    // odd number of backslashes encountered; this pipe is escaped.
                                }
                                backSlashCounter = 0;
                                break;
                            }
                        default:
                            backSlashCounter = 0;
                            break;
                    }
                }
            }

            public string GetValue(int i)
            {
                if (i < 0 || i >= _indices.Length)
                    throw new ArgumentOutOfRangeException($"index must be in range [0, max-1]");
                if (i >= _count) return null; 

                var start = i == 0
                    ? 0
                    : _indices[i] + 1;

                var end = i + 1 >= _count
                    ? _value.Length
                    : _indices[i + 1];

                var result = _value.Substring(start, end - start);

                // The substring and replace could be merged but for my purpose opting out is good enough.
                if (_escape[i])
                    result = result.Replace(@"\|", "|").Replace(@"\\", @"\");

                return result;
            }

            public IEnumerable<string> GetValues()
            {
                if (_count == 0) return null;
                return Enumerable
                    .Range(0, _count)
                    .Select(i => GetValue(i));
            }
        }
    }
}
