using System;
using System.Threading;

namespace Vim.Format.Logging
{
    public class StdCancelable : IDisposable
    {
        private bool _disposed;

        readonly CancellationTokenSource _cancel
            = new CancellationTokenSource();

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _cancel?.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Cancel()
            => _cancel.Cancel();

        public virtual bool IsCancelRequested()
            => _cancel.IsCancellationRequested;
    }
}
