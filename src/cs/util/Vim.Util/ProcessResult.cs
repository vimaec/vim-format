using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
            var redirectStdOut = process.StartInfo.RedirectStandardOutput;
            var redirectStdErr = process.StartInfo.RedirectStandardError;

            var stdOutStringBuilder = new StringBuilder();
            var stdErrStringBuilder = new StringBuilder();

            void HandleOutputDataReceived(object sender, DataReceivedEventArgs e)
            {
                if (e.Data == null) return;
                stdOutStringBuilder.AppendLine(e.Data);
                Console.WriteLine($"[{process.ProcessName}:{process.Id}] {e.Data}");
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
