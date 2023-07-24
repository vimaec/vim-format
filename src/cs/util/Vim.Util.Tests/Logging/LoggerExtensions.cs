namespace Vim.Util.Tests.Logging;

public static class LoggerExtensions
{
    public static void LogInformation(this ILogger logger, string message = "")
        => logger.Log(message, LogLevel.Information);
}
