using SerilogLogger = Serilog.Core.Logger;
using SerilogLogEventLevel = Serilog.Events.LogEventLevel;

namespace Vim.Format.Tests.Logging;

public class SerilogLoggerAdapter : ILogger
{
    private readonly SerilogLogger _logger;

    public SerilogLoggerAdapter(SerilogLogger logger)
        => _logger = logger;

    public void Log(string message = "", LogLevel level = LogLevel.Trace)
    {
        _logger.Write(level.ToSerilogLogEventLevel(), message);
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