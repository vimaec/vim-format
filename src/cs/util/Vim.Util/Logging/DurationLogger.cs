using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Vim.Util.Logging
{
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

    public static class DurationLoggerExtensions
    {
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
    }
}
