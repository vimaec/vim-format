namespace Vim.Util.Tests.Logging;

public interface ILogger
{
    void Log(string message = "", LogLevel level = LogLevel.Trace);
}
