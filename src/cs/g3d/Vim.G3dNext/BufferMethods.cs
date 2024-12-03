using System;
using System.IO;
using System.Linq;

namespace Vim.G3dNext
{
    public static class BufferMethods
    {
        public static bool SafeEquals<T>(T[] a, T[] b)
        {
            if(a == null && b == null) return true;
            if(a == null) return false;
            if(b == null) return false;
            if(a.Length != b.Length) return false;
            for(var i=0; i<a.Length; i++)
            {
                if (!a[i].Equals(b[i])) return false;
            }
            return true;
        }

        public static T[] MergeData<T>(T[] a, T[] b)
        {
            if(a == null && b == null) return null;  
            if(a == null) { return b.ToArray(); }
            if(b == null) { return a.ToArray(); }
            var result = new T[a.Length + b.Length];
            Array.Copy(a, result, a.Length);
            Array.Copy(b, 0, result, a.Length, b.Length);
            return result;
        }

        public static int[] MergeIndex(int[] a, int[] b, int offset)
        {
            if (a == null && b == null) return null;
            if (a == null && b == null) return null;
            if (a == null) { return b.ToArray(); }
            if (b == null) { return a.ToArray(); }
            var result = new int[a.Length + b.Length];
            Array.Copy(a, result, a.Length);
            for(var i=0; i<b.Length; i++)
            {
                result[i + a.Length] = b[i] >= 0
                    ? offset + b[i]
                    : -1;
            }
            return result;
        }

        public static void ValidateIndex<T>(int[] array, T[] into, string name)
        {
            if (array == null) return;
            var max = into?.Length -1 ?? int.MaxValue;
            for(var i=0; i <  array.Length; i++)
            {
                if (array[i] < -1 || array[i] > max)
                {
                    throw new InvalidDataException($"Invalid value {array[i]} in {name} buffer.");
                }
            }
        }
    }
}
