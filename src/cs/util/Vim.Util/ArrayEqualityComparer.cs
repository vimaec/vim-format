using System;
using System.Collections.Generic;

namespace Vim.Util
{
    public class ArrayEqualityComparer<T> : IEqualityComparer<T[]> where T : IEquatable<T>
    {
        public bool Equals(T[] x, T[] y)
        {
            if (x == y) return true;
            if (x == null || y == null) return false;
            if (x.Length != y.Length) return false;
            for (var i = 0; i < x.Length; ++i)
            {
                if (!x[i].Equals(y[i]))
                    return false;
            }

            return true;
        }

        public int GetHashCode(T[] obj) => obj.GetHashCode();
    }
}
