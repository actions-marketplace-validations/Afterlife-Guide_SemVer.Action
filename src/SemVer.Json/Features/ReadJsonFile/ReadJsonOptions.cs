using CommandLine;

namespace SemVer.Json.Features.ReadJsonFile;

[Verb("read", HelpText = "Reads a SemVer JSON file, and outputs the version numbers.")]
public class ReadJsonOptions
{
    [Option('p', "path",
        Required = true,
        Default = "semver.json",
        HelpText = "The path to the SemVer JSON file.")]
    public string Path { get; set; } = null!;
}