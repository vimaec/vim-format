using System;
using System.Collections.Generic;
using System.Linq;

namespace Vim.Util
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Invokes the given action on the collection of items in parallel. Does not halt if an exception is caught during an item's action execution.
        /// If no exception is thrown for a given item, the corresponding result for that item is marked as a success, otherwise it is a failure.
        /// Optionally prints the success state of the item's action to the console to help with debugging parallel tests.
        /// </summary>
        public static (Result<bool> Result, T Item)[] InvokeInParallel<T>(
            this IEnumerable<T> items,
            Action<T> action,
            Func<T, string> itemToHint,
            bool writeResultsToConsole = false,
            int delayBetweenStartsInMs = 0)
        {
            var results = items
                .Select((item, i) => (item, delay: i * delayBetweenStartsInMs))
                .AsParallel()
                .Select(tuple =>
                {
                    var (item, delay) = tuple;
                    try
                    {
                        if (delay > 0)
                            System.Threading.Thread.Sleep(delay);

                        action(item);

                        return (Result<bool>.Success(true), item);
                    }
                    catch (Exception e)
                    {
                        return (Result<bool>.Failure(e), item);
                    }
                })
                .ToArray();

            if (writeResultsToConsole)
            {
                foreach (var (result, item) in results)
                {
                    Console.WriteLine($"{(result.IsSuccess ? "Success" : "Failure")}: {itemToHint(item)}");
                    if (!result.IsSuccess)
                        Console.WriteLine(result.ToString());
                }
            }

            return results;
        }
    }
}
