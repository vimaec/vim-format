using System.Collections.Generic;
using Vim.BFast;

namespace Vim.Format.api_v2
{
    public class VimEntityTable
    {
        private readonly string[] _stringTable;

        public Dictionary<string, NamedBuffer<int>> IndexColumns { get; } = new Dictionary<string, NamedBuffer<int>>();
        public Dictionary<string, NamedBuffer<int>> StringColumns { get; } = new Dictionary<string, NamedBuffer<int>>();
        public Dictionary<string, INamedBuffer> DataColumns { get; } = new Dictionary<string, INamedBuffer>();
    }
}
