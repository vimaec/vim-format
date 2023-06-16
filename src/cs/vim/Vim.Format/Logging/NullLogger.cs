namespace Vim.Format.Logging
{
    public class NullLogger : ILogger
    {
        public ILogger Log(string message = "", LogLevel level = LogLevel.Trace)
            => this;
    }
}
