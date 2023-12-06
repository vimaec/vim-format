using System.Collections.Generic;
using System.Text;

namespace Vim.BFast.Core
{
    public static class BFastStrings
    {
        /// <summary>
        /// Converts a collection of strings, into a null-separated byte[] array 
        /// </summary>
        public static byte[] Pack(IEnumerable<string> strings)
        {
            var r = new List<byte>();
            foreach (var name in strings)
            {
                var bytes = Encoding.UTF8.GetBytes(name);
                r.AddRange(bytes);
                r.Add(0);
            }
            return r.ToArray();
        }


        /// <summary>
        /// Converts a byte[] array encoding a collection of strings separate by NULL into an array of string   
        /// </summary>
        public static string[] Unpack(byte[] bytes)
        {
            var r = new List<string>();
            if (bytes.Length == 0)
                return r.ToArray();
            var prev = 0;
            for (var i = 0; i < bytes.Length; ++i)
            {
                if (bytes[i] == 0)
                {
                    r.Add(Encoding.UTF8.GetString(bytes, prev, i - prev));
                    prev = i + 1;
                }
            }
            if (prev < bytes.Length)
                r.Add(Encoding.UTF8.GetString(bytes, prev, bytes.Length - prev));
            return r.ToArray();
        }
    }
}
