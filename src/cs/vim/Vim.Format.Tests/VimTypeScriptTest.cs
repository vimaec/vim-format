using NUnit.Framework;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CliWrap;
using Vim.Util.Tests;
using Vim.Util.Logging;

namespace Vim.Format.Tests;

[TestFixture]
public static class VimTypeScriptTest
{
    public static readonly string VimTypeScriptRepoPath = Path.Combine(VimFormatRepoPaths.SrcDir, "ts");

    public record NpmCommand(Command CliCommand, StringBuilder StdOut, StringBuilder StdErr)
    {
        public CommandTask<CommandResult> ExecuteAsync()
            => CliCommand.ExecuteAsync();

        public void WriteOutputToLogger(ILogger logger)
        {
            logger.LogInformation(@$"StdOut:
{StdOut}");
            logger.LogInformation(@$"StdErr:
{StdErr}");
        }

        public static NpmCommand Create(string args)
        {
            var stdOutBuffer = new StringBuilder();
            var stdErrBuffer = new StringBuilder();

            var cliCommand = Cli.Wrap("npm")
                .WithArguments(args)
                .WithWorkingDirectory(VimTypeScriptRepoPath)
                .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
                .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErrBuffer))
                .WithValidation(CommandResultValidation.None);

            return new(cliCommand, stdOutBuffer, stdErrBuffer);
        }
    }

    [Test]
    public static async Task RunNpmTest()
    {
        var ctx = new CallerTestContext();
        ctx.PrepareDirectory();
        var logger = ctx.CreateLogger();

        // npm install
        using (var _ = logger.LogDuration($"Running 'npm install' in {VimTypeScriptRepoPath}"))
        {
            var cmd = NpmCommand.Create("install");
            var result = await cmd.ExecuteAsync();
            cmd.WriteOutputToLogger(logger);
            Assert.AreEqual(0, result.ExitCode, "'npm install' exit code was not 0.");
        }

        // npm run test
        using (var _ = logger.LogDuration($"Running 'npm test' in {VimTypeScriptRepoPath}"))
        {
            var cmd = NpmCommand.Create("test");
            var result = await cmd.ExecuteAsync();
            cmd.WriteOutputToLogger(logger);
            Assert.AreEqual(0, result.ExitCode, "'npm test' exit code was not 0.");
        }
    }
}
