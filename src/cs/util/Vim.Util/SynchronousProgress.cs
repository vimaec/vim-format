using System;

namespace Vim.Util
{
    /// <summary>
    /// A synchronous Progress implementation.
    /// <br/><br/>
    /// NOTE: this avoids having the progress callback scheduled on a separate thread
    /// and solves the issue of having progress reports appear out-of-order.
    /// <br/><br/>
    /// SEE: https://stackoverflow.com/a/39744807
    /// </summary>
    public class SynchronousProgress<T> : IProgress<T>
    {
        private readonly Action<T> _callback;

        public SynchronousProgress(Action<T> callback)
        {
            _callback = callback;
        }

        void IProgress<T>.Report(T data)
        {
            _callback(data);
        }
    }
}
