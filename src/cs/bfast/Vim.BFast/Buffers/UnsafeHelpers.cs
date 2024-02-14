﻿using System.Collections.Generic;

namespace Vim.BFastLib
{
    public static class UnsafeHelpers
    {
        /// <summary>
        /// Returns an enumeration of chunks of the given size from the given enumeration.
        /// </summary>
        public static IEnumerator<(T[], int)> Chunkify<T>(IEnumerable<T> source, int chunkSize = 1048576)
        {
            var chunk = new T[chunkSize];
            var index = 0;

            foreach (var item in source)
            {
                chunk[index++] = item;

                if (index == chunkSize)
                {
                    yield return (chunk, index);
                    index = 0;
                }
            }

            if (index > 0)
            {
                yield return (chunk, index);
            }
        }
    }
}
