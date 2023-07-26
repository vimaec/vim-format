using System;

namespace Vim.Util
{
    public sealed class Disposer : IDisposable
    {
        readonly Action OnDispose;

        public Disposer(Action onDispose)
            => OnDispose = onDispose;

        public void Dispose()
            => OnDispose();
    }
}
