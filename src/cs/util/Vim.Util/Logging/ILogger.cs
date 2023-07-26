using System;

namespace Vim.Util.Logging
{
    public interface ILogger
    {
        ILogger Log(string message = "", LogLevel level = LogLevel.Trace);
    }

    public static class LoggerExtensions
    {
        public static ILogger StartLog(this ILogger logger)
            => logger.Log($"Started logging {DateTime.Now}");

        public static void LogInformation(this ILogger logger, string message = "")
            => logger.Log(message, LogLevel.Information);

        public static void LogDebug(this ILogger logger, string message = "")
            => logger.Log(message, LogLevel.Debug);

        public static void LogError(this ILogger logger, string message = "")
            => logger.Log(message, LogLevel.Error);

        public static void LogError(this ILogger logger, Exception e)
            => logger.LogError(e.ToString());

        public static void LogException(this ILogger logger, Exception e)
            => logger.Log(e.ToString(), LogLevel.Error);

        public static void LogWarning(this ILogger logger, string message = "")
            => logger.Log(message, LogLevel.Warning);

        public static void LogWarning(this ILogger logger, Exception e)
            => logger.Log(e.ToString(), LogLevel.Warning);
    }
}
