namespace Vim.Format.Logging
{
    public interface ILogger
    {
        ILogger Log(string message = "", LogLevel level = LogLevel.Trace);
    }
}
