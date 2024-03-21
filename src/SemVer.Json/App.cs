using CommandLine;
using Microsoft.Extensions.Configuration;
using SemVer.Json.Features.ReadJsonFile;
using SemVer.Json.Features.WriteJsonFile;

namespace SemVer.Json;

public class App
{
    private readonly ReadJsonCommand _readJsonCommand;
    private readonly WriteJsonCommand _writeJsonCommand;

    public App(IConfiguration configuration, ReadJsonCommand readJsonCommand, WriteJsonCommand writeJsonCommand)
    {
        _readJsonCommand = readJsonCommand;
        _writeJsonCommand = writeJsonCommand;
    }

    public void Run(IEnumerable<string> args)
    {
        Parser.Default.ParseArguments<ReadJsonOptions, WriteJsonOptions>(args)
            .MapResult(
                (ReadJsonOptions opts) => _readJsonCommand.Read(opts),
                (WriteJsonOptions opts) => _writeJsonCommand.Write(opts),
                _ => 1
            );
    }
}