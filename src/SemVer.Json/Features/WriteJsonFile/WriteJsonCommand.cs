using System.Text.Json;
using SemVer.Json.Persistence;

namespace SemVer.Json.Features.WriteJsonFile;

public class WriteJsonCommand
{
    private readonly IFileWriter _fileWriter;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public WriteJsonCommand(IFileWriter fileWriter)
    {
        _fileWriter = fileWriter;
    }

    public int Write(WriteJsonOptions options)
    {
        var semVer = new SemVer
        {
            Major = options.Major,
            Minor = options.Minor,
            Patch = options.Patch,
            Build = options.Build
        };
        var json = JsonSerializer.Serialize(semVer, _jsonSerializerOptions);
        _fileWriter.WriteJson(json, options.Path);

        return 0;
    }
}