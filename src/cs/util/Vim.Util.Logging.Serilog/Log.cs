using System;
using Serilog;
using SerilogLog = Serilog.Log;

namespace Vim.Util.Logging.Serilog
{
    public static class Log
    {
        public static ILogger Instance { get; private set; }
            = new NullLogger();

        public static void Information(string message)
            => Instance.LogInformation(message);

        public static void Debug(string message)
            => Instance.LogDebug(message);

        public static void Warning(string message)
            => Instance.LogWarning(message);

        public static void Warning(Exception e)
            => Warning(e.ToString());

        public static void Warning(string message, Exception e)
        {
            Warning($"WARNING: {message}");
            Warning(e);
        }

        public static void Error(string message)
            => Instance.LogError(message);

        public static void Error(Exception e)
            => Instance.LogException(e);

        public static void Error(string message, Exception e)
        {
            Error($"ERROR: {message}");
            Error(e);
        }

        public static DurationLogger Duration(string name)
            => Instance.LogDuration(name);

        public static ILogger Init(string name, string filepath, bool writeToConsole = true, bool addEvent = false)
        {
            var logger = CreateLogger(name, filepath, writeToConsole, addEvent);
            SerilogLog.Logger = logger.Logger;
            Instance = logger;
            return Instance;
        }

        public static SerilogLoggerAdapter CreateLogger(
            string name,
            string filePath = null,
            bool writeToConsole = true,
            bool addEvent = false,
            long? fileSizeLimitBytes = 1_000_000_000, // serilog default: 1GB
            bool rollOnFileSizeLimit = true, // serilog default: true
            int? retainedFileCountLimit = 31 // serilog default: 31
        )
        {
            var config = new LoggerConfiguration()
                .MinimumLevel.Debug();

            if (!string.IsNullOrEmpty(filePath))
            {
                IO.CreateFileDirectory(filePath);
                config = config.WriteTo.File(
                    filePath,
                    fileSizeLimitBytes: fileSizeLimitBytes,
                    rollOnFileSizeLimit: rollOnFileSizeLimit,
                    retainedFileCountLimit: retainedFileCountLimit
                );
            }

            if (writeToConsole)
                config.WriteTo.Console();

            if (addEvent)
                config.WriteTo.EventLog(name);

            return new SerilogLoggerAdapter(config.CreateLogger());
        }
    }
}
