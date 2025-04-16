using System;

namespace Vim.Format
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CascadeElementRemapAttribute : Attribute
    {
        public CascadeElementRemapAttribute()
        { }
    }
}
