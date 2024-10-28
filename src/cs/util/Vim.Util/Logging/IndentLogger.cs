using System;

namespace Vim.Util.Logging
{
    public class IndentLogger : ILogger, IDisposable
    {
        private const string _indentation = "  ";

        public readonly ILogger InnerLogger;
        private string _indentPrefix = string.Empty;
        private bool _isDisposed;

        public IndentLogger(ILogger innerLogger)
            => InnerLogger = innerLogger;

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            if (InnerLogger is IDisposable d)
                d.Dispose();
        }

        public IndentLogger Indent()
        {
            _indentPrefix += _indentation;
            return this;
        }

        public IndentLogger Outdent()
        {
            _indentPrefix = _indentPrefix.Substring(0, Math.Max(_indentPrefix.Length - _indentation.Length, 0));
            return this;
        }

        public Disposer IndentedLog(string message)
        {
            Log(message);
            Indent();
            return new Disposer(() => Outdent());
        }

        public ILogger Log(string message = "", LogLevel level = LogLevel.Trace)
        {
            InnerLogger.Log(_indentPrefix + message, level);
            return this;
        }
    }
}
