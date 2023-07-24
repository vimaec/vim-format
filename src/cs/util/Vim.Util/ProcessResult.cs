using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Vim.Util
{
    public class ProcessResult
    {
        public readonly Process Process;
        public readonly string StdOut;
        public readonly string StdErr;
        public int ExitCode => Process.ExitCode;

        public ProcessResult(Process process, string stdOut = null, string stdErr = null)
        {
            Process = process;
            StdOut = stdOut;
            StdErr = stdErr;
        }

        public override string ToString()
            => $@"Process: {Process.StartInfo.FileName}
ExitCode: {ExitCode}
Arguments: {Process.StartInfo.Arguments}
StdOut: {StdOut}
StdErr: {StdErr}";

        public bool IsSuccess(params int[] validExitCodes)
            => validExitCodes == null || validExitCodes.Length == 0
                ? ExitCode == 0
                : validExitCodes.Any(e => ExitCode == e);
    }

    public static class ProcessResultExtensions
    {
        /// <summary>
        /// Waits for the process to exit and returns a ProcessResult.
        /// </summary>
        public static ProcessResult GetResult(this Process process)
        {
            var stdOut = process.StartInfo.RedirectStandardOutput ? process.StandardOutput.ReadToEnd() : null;
            var stdErr = process.StartInfo.RedirectStandardError ? process.StandardError.ReadToEnd() : null;
            process.WaitForExit();
            return new ProcessResult(process, stdOut, stdErr);
        }

        /// <summary>
        /// Runs a process with the given startInfo and waits for it to exit before returning a ProcessResult.
        /// </summary>
        public static ProcessResult GetResult(this ProcessStartInfo startInfo)
        {
            var p = new Process { StartInfo = startInfo };
            p.Start();
            return p.GetResult();
        }
    }

    public static class ProcessExtensions
    {
        /// <summary>
        /// Waits asynchronously for the process to exit.
        /// </summary>
        // see: https://stackoverflow.com/a/50461641
        public static async Task WaitForExitAsync(
            this Process process,
            CancellationToken cancellationToken = default)
        {
            var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            void ProcessExited(object sender, EventArgs e)
                => tcs.TrySetResult(true);

            process.EnableRaisingEvents = true;
            process.Exited += ProcessExited;

            try
            {
                if (process.HasExited)
                {
                    return;
                }

                using (cancellationToken.Register(() => tcs.TrySetCanceled()))
                {
                    await tcs.Task.ConfigureAwait(false);
                }
            }
            finally
            {
                process.Exited -= ProcessExited;
            }
        }
    }
}
