using Microsoft.Extensions.Logging;
using SemVer.Json.Features.ReadJsonFile;
using SemVer.Json.Output;

namespace SemVer.Json.UnitTests.Features.ReadJsonFile;

public class ReadJsonCommandTests
{
    private readonly IOutputWriter _mockOutputWriter = Substitute.For<IOutputWriter>();
    private readonly ILogger<ReadJsonCommand> _mockLogger = Substitute.For<ILogger<ReadJsonCommand>>();
    private readonly ReadJsonCommand _sut;

    public ReadJsonCommandTests()
    {
        _sut = new ReadJsonCommand(_mockOutputWriter, _mockLogger);
    }

    [Theory]
    [InlineData("Data/semver-0-1-4-5.json", "0", "1", "4", "5")]
    [InlineData("Data/semver-6-0-44-3.json", "6", "0", "44", "3")]
    public void WhenReadingJsonFile_ThenShouldReturnCorrectVersionInformation(string file, string major, string minor, string patch, string build)
    {
        // arrange
        var options = new ReadJsonOptions
        {
            Path = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", file)
        };
        var expectedVersion = new SemVer
        {
            Major = major,
            Minor = minor,
            Patch = patch,
            Build = build
        };
        
        // act
        _sut.Read(options);
        
        // assert
        _mockOutputWriter.Received(1).OutputVersion(expectedVersion);
    }
    
    [Fact]
    public void WhenReadingMalformedJsonFile_ThenShouldReturnErrorCode()
    {
        // arrange
        var options = new ReadJsonOptions
        {
            Path = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "Data/malformed.json")
        };
        
        // act
        var result = _sut.Read(options);
        
        // assert
        result.Should().Be(1);
    }
    
    [Fact]
    public void WhenReadingAJsonFileWithAnIncorrectSchema_ThenShouldReturnErrorCode()
    {
        // arrange
        var options = new ReadJsonOptions
        {
            Path = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "Data/incorrect-schema.json")
        };
        
        // act
        var result = _sut.Read(options);
        
        // assert
        result.Should().Be(1);
    }
    
    [Fact]
    public void WhenReadingAJsonFileThatDoesNotExist_ThenShouldReturnErrorCode()
    {
        // arrange
        var options = new ReadJsonOptions
        {
            Path = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "Data/does-not-exist.json")
        };
        
        // act
        var result = _sut.Read(options);
        
        // assert
        result.Should().Be(1);
    }
}