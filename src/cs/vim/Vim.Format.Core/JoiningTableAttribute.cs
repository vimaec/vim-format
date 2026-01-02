using System;

namespace Vim.Format
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JoiningTableAttribute : Attribute
    {
        public JoiningTableAttribute()
        { }
    }
}
