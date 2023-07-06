using System.Globalization;

namespace Vim.Format.Logging
{
    /// <summary>
    /// The default implementation of Cancelable Logger supports logging progress
    /// and using a CancellationSourceToken for cancellation.  If no logger is
    /// provided, the default will be assigned as a logging output
    /// </summary>
    public class StdCancelableProgressLogger : StdCancelable, ICancelableProgressLogger
    {
        private readonly ILogger _logger;

        public StdCancelableProgressLogger(ILogger logger = null)
            => _logger = logger ?? new StdLogger();

        public virtual void Report(double x)
            => _logger?.Log(x.ToString(CultureInfo.InvariantCulture));

        public ILogger Log(string message = "", LogLevel logLevel = LogLevel.None)
        {
            _logger?.Log(message, logLevel);
            return this;
        }
    }
}
