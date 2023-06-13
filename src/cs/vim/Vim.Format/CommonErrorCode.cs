// ReSharper disable once CheckNamespace
namespace Vim.Format
{
    /// <summary>
    /// Common error codes which follow the HResult convention.
    /// </summary>
    public enum CommonErrorCode
    {
        Success = HResult.Success,
        Failure = HResult.Failure,
        UsageError = HResult.UsageError,
        FileNotFound = HResult.FileNotFound,
    }
}
