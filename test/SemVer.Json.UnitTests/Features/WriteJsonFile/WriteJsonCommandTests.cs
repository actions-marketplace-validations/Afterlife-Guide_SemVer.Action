using SemVer.Json.Features.WriteJsonFile;
using SemVer.Json.Persistence;

namespace SemVer.Json.UnitTests.Features.WriteJsonFile;

public class WriteJsonCommandTests
{
    private readonly IFileWriter _mockFileWriter = Substitute.For<IFileWriter>();
    private readonly WriteJsonCommand _sut;

    public WriteJsonCommandTests()
    {
        _sut = new WriteJsonCommand(_mockFileWriter);
    }

    [Fact]
    public void WhenWritingJsonFile_ThenFileIsWrittenToCorrectPath()
    {
        // arrange
        var options = new WriteJsonOptions
        {
            Path = "test.json",
            Major = "1",
            Minor = "2",
            Patch = "3",
            Build = "4"
        };
        
        // act
        _sut.Write(options);
        
        // assert
        _mockFileWriter.Received(1).WriteJson(Arg.Any<string>(), Arg.Is<string>(s => s == options.Path));
    }
    
    [Fact]
    public Task WhenWritingJsonFile_ThenJsonStringIsCorrect()
    {
        // arrange
        var options = new WriteJsonOptions
        {
            Path = "test.json",
            Major = "1",
            Minor = "2",
            Patch = "3",
            Build = "4"
        };

        var actualJson = string.Empty;
        
        _mockFileWriter.WriteJson(Arg.Do<string>(s => actualJson = s), Arg.Any<string>());
        
        // act
        _sut.Write(options);
        
        // assert
        return VerifyJson(actualJson);
    }
}