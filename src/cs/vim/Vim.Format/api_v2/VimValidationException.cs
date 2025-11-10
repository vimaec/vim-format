using System;

namespace Vim.Format.api_v2
{
    public class VimValidationException : Exception
    {
        public VimValidationException(string message) : base(message) { }
    }
}