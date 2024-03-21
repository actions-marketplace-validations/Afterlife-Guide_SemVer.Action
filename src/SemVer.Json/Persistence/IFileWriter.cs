namespace SemVer.Json.Persistence;

public interface IFileWriter
{
    public void WriteJson(string jsonString, string filePath);
}