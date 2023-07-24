using Serilog;
using Vim.Format.Utils;

namespace Vim.Format.Tests.Logging;

public static class Log
{
    public static SerilogLoggerAdapter CreateLogger(
        string name, string filePath = null, bool writeToConsole = true, bool addEvent = false)
    {
        var config = new LoggerConfiguration()
                    .MinimumLevel.Debug();

        if (!string.IsNullOrEmpty(filePath))
        {
            Util.CreateFileDirectory(filePath);
            config = config.WriteTo.File(filePath);
        }

        if (writeToConsole)
            config.WriteTo.Console();

        if (addEvent)
            config.WriteTo.EventLog(name);

        return new SerilogLoggerAdapter(config.CreateLogger());
    }
}