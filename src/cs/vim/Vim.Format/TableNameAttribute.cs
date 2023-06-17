using System;

namespace Vim.DataFormat
{
    public class TableNameAttribute : Attribute
    {
        public string Name { get; }
        public TableNameAttribute(string name)
            => Name = name;
    }
}
