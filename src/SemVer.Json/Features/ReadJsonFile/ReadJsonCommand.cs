using System.Text.Json;
using Microsoft.Extensions.Logging;
using SemVer.Json.Output;

namespace SemVer.Json.Features.ReadJsonFile;

public class ReadJsonCommand(IOutputWriter outputWriter, ILogger<ReadJsonCommand> logger)
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    public int Read(ReadJsonOptions opts)
    {
        string json;
        try
        {
            json = File.ReadAllText(opts.Path);
            logger.LogInformation("Read JSON file: {Path}", opts.Path);
            logger.LogInformation("JSON content: {Json}", json);
        }
        catch (FileNotFoundException ex)
        {
            logger.LogError(ex, "Error reading JSON file: {Message}", ex.Message);
            return 1;
        }
        
        try
        {
            var semVer = JsonSerializer.Deserialize<SemVer>(json, _jsonSerializerOptions);
            outputWriter.OutputVersion(semVer!);
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "Error deserializing JSON file: {Message}", ex.Message);
            return 1;
        }
        return 0;
    }
}