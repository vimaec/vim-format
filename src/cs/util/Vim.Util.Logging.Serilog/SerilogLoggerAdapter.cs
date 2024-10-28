using System;
using SerilogLogger = Serilog.Core.Logger;
using SerilogLogEventLevel = Serilog.Events.LogEventLevel;

namespace Vim.Util.Logging.Serilog
{
    public class SerilogLoggerAdapter : ILogger, IDisposable
    {
        public readonly SerilogLogger Logger;
        private bool _isDisposed;

        public SerilogLoggerAdapter(SerilogLogger logger)
            => Logger = logger;

        public ILogger Log(string message = "", LogLevel level = LogLevel.Trace)
        {
            Logger.Write(level.ToSerilogLogEventLevel(), message);
            return this;
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            Logger.Dispose();
        }
    }

    public static class SerilogExtensions
    {
        public static SerilogLogEventLevel ToSerilogLogEventLevel(this LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return SerilogLogEventLevel.Debug;
                case LogLevel.Warning:
                    return SerilogLogEventLevel.Warning;
                case LogLevel.Error:
                    return SerilogLogEventLevel.Error;
                case LogLevel.Critical:
                    return SerilogLogEventLevel.Fatal;
                case LogLevel.Trace:
                case LogLevel.Information:
                case LogLevel.None:
                default:
                    return SerilogLogEventLevel.Information;
            }
        }
    }
}
