using System.Text;

namespace SemVer.Json.Output;

public class GitHubActionOutputWriter : IOutputWriter
{
    private const string GitHubOutput = "GITHUB_OUTPUT";
    public void OutputVersion(SemVer semVer)
    {
        var gitHubOutputFile = Environment.GetEnvironmentVariable(GitHubOutput);
        if (string.IsNullOrEmpty(gitHubOutputFile)) return;
        
        using var textWriter = new StreamWriter(gitHubOutputFile, append: true, Encoding.UTF8);
        WriteVariableToGitHubAction(textWriter, "major", semVer.Major);
        WriteVariableToGitHubAction(textWriter, "minor", semVer.Minor);
        WriteVariableToGitHubAction(textWriter, "patch", semVer.Patch);
        WriteVariableToGitHubAction(textWriter, "build", semVer.Build);
        WriteVariableToGitHubAction(textWriter, "version", semVer.ToVersion());
    }

    private static void WriteVariableToGitHubAction(TextWriter textWriter, string name, string value)
    {
        var output = $"{name}={value}";
        textWriter.WriteLine(output);
    }
}