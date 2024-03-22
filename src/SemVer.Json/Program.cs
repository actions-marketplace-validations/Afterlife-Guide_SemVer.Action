using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SemVer.Json;
using SemVer.Json.Features.ReadJsonFile;
using SemVer.Json.Features.WriteJsonFile;
using SemVer.Json.Output;
using SemVer.Json.Persistence;

using var host = CreateHostBuilder(args).Build();
using var scope = host.Services.CreateScope();

var services = scope.ServiceProvider;
try
{
    var exit = services.GetRequiredService<App>().Run(args);
    Environment.Exit(exit);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    Environment.Exit(1);
}

return;

IHostBuilder CreateHostBuilder(string[] strings)
{
    return Host.CreateDefaultBuilder()
        .ConfigureServices((_, s) =>
        {
            s.AddSingleton<App>();
            s.AddSingleton<ReadJsonCommand>();
            s.AddSingleton<WriteJsonCommand>();
            s.AddSingleton<IOutputWriter, GitHubActionOutputWriter>();
            s.AddSingleton<IFileWriter, FileWriter>();
        });
}