using System;

namespace Vim.Format
{
    public class TableNameAttribute : Attribute
    {
        public string Name { get; }
        public TableNameAttribute(string name)
            => Name = name;
    }
}
