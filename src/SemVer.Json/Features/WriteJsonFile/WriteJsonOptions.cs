using CommandLine;

namespace SemVer.Json.Features.WriteJsonFile;

[Verb("write", HelpText = "Writes a SemVer JSON file, with the specified version numbers.")]
public class WriteJsonOptions
{
    [Option('p', "path",
        Required = true,
        Default = "semver.json",
        HelpText = "The path to the SemVer JSON file.")]
    public string Path { get; set; } = null!;
    
    [Option('m', "major",
        Required = true,
        HelpText = "The major version number.")]
    public string Major { get; set; } = null!;
    
    [Option('n', "minor",
        Required = true,
        HelpText = "The minor version number.")]
    public string Minor { get; set; } = null!;
    
    [Option('h', "patch",
        Required = true,
        HelpText = "The patch version number.")]
    public string Patch { get; set; } = null!;
    
    [Option('b', "build",
        Required = true,
        HelpText = "The build version number.")]
    public string Build { get; set; } = null!;
}