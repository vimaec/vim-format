using System;

namespace Vim.Util
{
    public sealed class Disposer : IDisposable
    {
        private bool _isDisposed = false;

        readonly Action OnDispose;

        public Disposer(Action onDispose)
            => OnDispose = onDispose;

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            OnDispose();
        }
    }
}
