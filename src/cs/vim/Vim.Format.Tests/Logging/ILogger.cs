namespace Vim.Format.Tests.Logging;

public interface ILogger
{
    void Log(string message = "", LogLevel level = LogLevel.Trace);
}