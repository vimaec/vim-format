using System;

namespace Vim.Format.Utils
{
    /// <summary>
    /// Combines a Cancelation event observer (CancellationToken) and a cancelation requester (CancelTokenSource). 
    /// Intentionally merging two very closely related concerns that rarely have advantage in being separated. 
    /// https://stackoverflow.com/questions/14215784/why-cancellationtoken-is-separate-from-cancellationtokensource
    /// https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken?view=netstandard-2.1
    /// https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource?view=netstandard-2.1
    /// </summary>
    public interface ICancelable
    {
        bool IsCancelRequested();
        void Cancel();
    }

    public class CancelException : Exception
    { }

    public static class Cancelable
    {
        public static T ThrowIfCanceled<T>(this T cancelable) where T : ICancelable
        {
            if (cancelable?.IsCancelRequested() == true)
                throw new CancelException();
            return cancelable;
        }
    }
}
