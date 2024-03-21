namespace SemVer.Json.Output;

public interface IOutputWriter
{
    void OutputVersion(SemVer semVer);
}