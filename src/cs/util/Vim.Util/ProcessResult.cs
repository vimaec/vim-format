using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Vim.Util
{
    public class ProcessResult
    {
        public const string ProgressPrefix = "Progress:";

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
        public static ProcessResult GetResult(this Process process, IProgress<string> progress = null, CancellationToken? ct = null)
        {
            var redirectStdOut = process.StartInfo.RedirectStandardOutput;
            var redirectStdErr = process.StartInfo.RedirectStandardError;

            var stdOutStringBuilder = new StringBuilder();
            var stdErrStringBuilder = new StringBuilder();

            void HandleProgressMessage(string msg)
            {
                if (progress == null)
                    return;

                var prefixStart = msg.IndexOf(ProcessResult.ProgressPrefix, StringComparison.Ordinal);
                if (prefixStart == -1)
                    return;

                var messageStart = prefixStart + ProcessResult.ProgressPrefix.Length;
                var count = msg.Length - messageStart;
                var progressMessage = msg.Substring(messageStart, count).Trim();

                if (string.IsNullOrEmpty(progressMessage))
                    return;

                progress.Report(progressMessage);
            }

            void HandleOutputDataReceived(object sender, DataReceivedEventArgs e)
            {
                if (e.Data == null) return;
                var msg = e.Data;
                stdOutStringBuilder.AppendLine(msg);
                Console.WriteLine($"[{process.ProcessName}:{process.Id}] {msg}");
                HandleProgressMessage(msg);
            }

            void HandleErrorDataReceived(object sender, DataReceivedEventArgs e)
            {
                if (e.Data == null) return;
                stdErrStringBuilder.AppendLine(e.Data);
                Console.Error.WriteLine($"[{process.ProcessName}:{process.Id}] {e.Data}");
            }

            try
            {
                if (redirectStdOut)
                {
                    process.OutputDataReceived += HandleOutputDataReceived;
                    process.BeginOutputReadLine();
                }

                if (redirectStdErr)
                {
                    process.ErrorDataReceived += HandleErrorDataReceived;
                    process.BeginErrorReadLine();
                }

                ct?.Register(() =>
                {
                    try
                    {
                        if (!process.HasExited)
                            process.Kill();
                    }
                    catch
                    {
                        // do nothing
                    }
                });

                process.WaitForExit();

                return new ProcessResult(process, stdOutStringBuilder.ToString(), stdErrStringBuilder.ToString());
            }
            finally
            {
                if (redirectStdOut) { process.OutputDataReceived -= HandleOutputDataReceived; }
                if (redirectStdErr) { process.ErrorDataReceived -= HandleErrorDataReceived;  }
            }
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
}
