using Vim.Format.Logging;
using ILogger = Vim.Format.Logging.ILogger;
using SerilogLogger = Serilog.Core.Logger;
using SerilogLogEventLevel = Serilog.Events.LogEventLevel;

namespace Vim.Format.Tests
{
    public class SerilogLoggerAdapter : ILogger
    {
        public readonly SerilogLogger Logger;

        public SerilogLoggerAdapter(SerilogLogger logger)
            => Logger = logger;

        public ILogger Log(string message = "", LogLevel level = LogLevel.Trace)
        {
            Logger.Write(level.ToSerilogLogEventLevel(), message);
            return this;
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
