using System;
using System.Diagnostics;
using System.IO;

namespace Vim.Util.Tests;

/// <summary>
/// A utility class to automate command line processes.
/// </summary>
public abstract class ShellProcess
{
    public const string DefaultConfig =
#if DEBUG
        "Debug";
#else
        "Release";
#endif

    public abstract string GetExePath(string projectConfig);

    public Process StartProcess<T>(T options, string workingDirectory = null, string projectConfig = DefaultConfig)
    {
        var argStr = string.Join(" ", options.ToArgList());
        return StartProcess(argStr, workingDirectory, projectConfig);
    }

    public Process StartProcess(string argStr = null, string workingDirectory = null, string projectConfig = DefaultConfig)
    {
        var exePath = GetExePath(projectConfig);
        if (!File.Exists(exePath))
            throw new Exception($"Executable not found: {exePath}");

        var startInfo = new ProcessStartInfo { FileName = exePath, Arguments = argStr ?? "", UseShellExecute = true };
        if (workingDirectory != null)
            startInfo.WorkingDirectory = workingDirectory;

        var process = new Process { StartInfo = startInfo };

        process.Start();

        return process;
    }
}
