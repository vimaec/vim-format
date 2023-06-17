using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Vim.Format.Utils;

namespace Vim.Format.Logging
{
    public static class LoggerExtensions
    {
        public class Frame
        {
            public string MethodName { get; set; }
            public string FileName { get; set; }
            public int LineNumber { get; set; }

            public static Frame GetFrame(int depth)
            {
                var f = new StackTrace().GetFrame(depth + 1);
                return new Frame
                {
                    MethodName = f.GetMethod().Name,
                    FileName = f.GetFileName(),
                    LineNumber = f.GetFileLineNumber(),
                };
            }
        }

        public static ILogger LogFrame(this ILogger logger, int frameDepth = 1)
            => logger.Log($"Current frame {Frame.GetFrame(frameDepth)}");

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

        public static ILogger Log(this ILogger logger, IEnumerable<string> messages, LogLevel level = LogLevel.Trace)
        {
            foreach (var message in messages)
            {
                logger.Log(message, level);
            }
            return logger;
        }

        public static void TryLog(this ILogger logger, Action f, string message = null)
        {
            try
            {
                if (message != null)
                    logger.Log(message);
                f();
            }
            catch (Exception e)
            {
                logger.LogException(e);
            }
        }

        public sealed class DurationLogger : IDisposable
        {
            public readonly ILogger Logger;
            public string Name;
            public readonly Stopwatch Stopwatch = Stopwatch.StartNew();
            private readonly Func<long, string> _msToString;

            public DurationLogger(ILogger logger, string name = "", Func<long, string> msToString = null)
            {
                Logger = logger;
                Name = name;
                logger.Log($"[BEGIN] {Name}");
                _msToString = msToString ?? new Func<long, string>((t) => $"msec elapsed {t}");
            }

            public void Dispose()
                => Logger.Log($"[END] {Name}; {_msToString(Stopwatch.ElapsedMilliseconds)}");
        }

        public static DurationLogger LogDuration(this ILogger logger, string name, Func<long, string> msToString = null)
            => new DurationLogger(logger, name, msToString);

        public static DurationLogger LogScopeDuration(
            this ILogger logger,
            string extra = null,
            [CallerMemberName] string name = null,
            [CallerFilePath] string sourceFilePath = null,
            Func<long, string> msToString = null)
        {
            var fileNameNoExtension = string.IsNullOrEmpty(sourceFilePath)
                ? null
                : Path.GetFileNameWithoutExtension(sourceFilePath);

            var items = new[] { fileNameNoExtension, name, extra }.Where(s => !string.IsNullOrEmpty(s));
            return logger.LogDuration(string.Join(" > ", items), msToString);
        }

        public static ILogger StartLog(this ILogger logger)
            => logger.Log($"Started logging {DateTime.Now}");

        public static T LogFunctionDuration<T>(this ILogger logger, string msg, Func<T> f)
        {
            using (logger.LogDuration(msg))
            {
                try
                {
                    return f();
                }
                catch (Exception e)
                {
                    logger.LogException(e);
                    throw;
                }
            }
        }

        public static void LogActionDuration(this ILogger logger, string msg, Action action)
        {
            using (logger.LogDuration(msg))
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    logger.LogException(e);
                    throw;
                }
            }
        }

        public static T LogOpenFile<T>(this ILogger logger, string filePath, Func<string, T> openFunction)
        {
            using (logger.LogDuration($"Opening file {filePath}"))
                return openFunction(filePath);
        }

        public static string LogWriteFile(this ILogger logger, string filePath, Action<string> writeFunction)
        {
            using (logger.LogDuration($"Writing file {filePath}"))
                writeFunction(filePath);
            return filePath;
        }

        public static T LogProgress<T>(this T logger, string message, double? percentage = null) where T : ICancelableProgressLogger
        {
            logger?.ThrowIfCanceled().Log(message);
            if (percentage != null)
                logger?.Report((double)percentage);
            return logger;
        }
    }
}
