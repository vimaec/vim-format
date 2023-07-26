using System.Collections;
using System.Collections.Generic;

namespace Vim.Util.Logging
{
    public class LogRecord
    {
        public readonly string Message;
        public readonly LogLevel LogLevel;
        public LogRecord(string message, LogLevel logLevel)
            => (Message, LogLevel) = (message, logLevel);
    }

    public class RecordLogger : ILogger, IEnumerable<LogRecord>
    {
        public List<LogRecord> LogRecords { get; } = new List<LogRecord>();

        public ILogger Log(string message = "", LogLevel level = LogLevel.Trace)
        {
            LogRecords.Add(new LogRecord(message, level));
            return this;
        }

        public IEnumerator<LogRecord> GetEnumerator()
            => LogRecords.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
