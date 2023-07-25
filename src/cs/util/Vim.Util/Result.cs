using System;

namespace Vim.Util
{
    /// <summary>
    /// A generic result class which contains a value upon success, and an exception upon failure.
    /// </summary>
    public class Result<T>
    {
        public readonly T Value;
        public readonly Exception Exception;

        public bool IsSuccess
            => Exception == null;

        private Result(T value, Exception exception = null)
        {
            Value = value;
            Exception = exception;
        }

        public override string ToString()
            => IsSuccess ? "Success" : Exception?.ToString() ?? "Failure";

        public static Result<T> Success(T value)
            => new Result<T>(value);

        public static Result<T> Failure(Exception exception)
            => new Result<T>(default, exception);
    }
}
