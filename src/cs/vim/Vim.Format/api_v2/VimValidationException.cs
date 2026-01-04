using System;

namespace Vim.Format
{
    public class VimValidationException : Exception
    {
        public VimValidationException(string message) : base(message) { }
    }
}