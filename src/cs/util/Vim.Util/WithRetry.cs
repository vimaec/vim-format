using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vim.Util.Logging;

namespace Vim.Util
{
    public static class WithRetry
    {
        public static T Run<T>(Func<T> func, string runName, ILogger logger, uint maxRetries, TimeSpan initialDelay = default)
        {
            var retries = 0;
            var delay = initialDelay == default ? TimeSpan.FromSeconds(1) : initialDelay;

            do
            {
                try
                {
                    logger.LogInformation($"[{nameof(WithRetry)}.{nameof(Run)} > {runName} ({retries}/{maxRetries})] Running...");
                    var result = func();
                    logger.LogInformation($"[{nameof(WithRetry)}.{nameof(Run)} > {runName} ({retries}/{maxRetries})] Success");
                    return result;
                }
                catch (Exception e)
                {
                    // Log the exception
                    logger.LogWarning($"[{nameof(WithRetry)}.{nameof(Run)} > {runName} ({retries}/{maxRetries})] Exception caught: {e}");
                }

                // Exponential back-off
                logger.LogInformation($"[{nameof(WithRetry)}.{nameof(Run)} > {runName} ({retries}/{maxRetries})] Sleeping {delay}");
                Thread.Sleep(delay);
                delay = TimeSpan.FromMilliseconds(delay.TotalMilliseconds * 2);
                retries++;
            } while (retries < maxRetries);

            // If all retries fail, rethrow the last exception
            logger.LogInformation($"[{nameof(WithRetry)}.{nameof(Run)} > {runName} ({retries}/{maxRetries})] Final attempt...");
            return func();
        }

        public static async Task<T> RunAsync<T>(Func<Task<T>> createTask, string runName, ILogger logger, uint maxRetries, TimeSpan initialDelay = default)
        {
            var retries = 0;
            var delay = initialDelay == default ? TimeSpan.FromSeconds(1) : initialDelay;

            do
            {
                try
                {
                    logger.LogInformation($"[{nameof(WithRetry)}.{nameof(RunAsync)} > {runName} ({retries}/{maxRetries})] Running...");
                    var result = await createTask();
                    logger.LogInformation($"[{nameof(WithRetry)}.{nameof(RunAsync)} > {runName} ({retries}/{maxRetries})] Success");
                    return result;
                }
                catch (Exception e)
                {
                    // Log the exception
                    logger.LogWarning($"[{nameof(WithRetry)}.{nameof(RunAsync)} > {runName} ({retries}/{maxRetries})] Exception caught: {e}");
                }

                // Exponential back-off
                logger.LogInformation($"[{nameof(WithRetry)}.{nameof(RunAsync)} > {runName} ({retries}/{maxRetries})] Delaying {delay}");
                await Task.Delay(delay);
                delay = TimeSpan.FromMilliseconds(delay.TotalMilliseconds * 2);
                retries++;
            } while (retries < maxRetries);

            // If all retries fail, rethrow the last exception
            logger.LogInformation($"[{nameof(WithRetry)}.{nameof(RunAsync)} > {runName} ({retries}/{maxRetries})] Final attempt...");
            return await createTask();
        }
    }
}
