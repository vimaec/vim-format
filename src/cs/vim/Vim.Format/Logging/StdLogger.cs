using System;
using System.Diagnostics;

namespace Vim.Format.Logging
{
    public class StdLogger : ILogger
    {
        public Stopwatch Stopwatch = Stopwatch.StartNew();

        private readonly bool _writeToConsole;
        private readonly bool _writeToDebug;

        public StdLogger(bool writeToConsole = true, bool writeToDebug = true)
        {
            _writeToConsole = writeToConsole;
            _writeToDebug = writeToDebug;
            this.StartLog();
        }

        public ILogger Log(string message = "", LogLevel level = LogLevel.None)
        {
            var timeStamp = Stopwatch.Elapsed.ToString(@"hh\:mm\:ss\.ff");
            var msg = $"{timeStamp} - {message}";
            if (_writeToConsole) Console.WriteLine(msg);
            if (_writeToDebug) Debug.WriteLine(msg);
            return this;
        }
    }
}
