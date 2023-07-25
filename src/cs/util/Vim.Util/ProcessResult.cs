using System.Diagnostics;
using System.Linq;

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
}
