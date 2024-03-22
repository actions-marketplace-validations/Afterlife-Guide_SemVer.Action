namespace SemVer.Json.Persistence;

public class FileWriter : IFileWriter
{
    public void WriteJson(string jsonString, string filePath)
    {
        File.WriteAllText(filePath, jsonString);
    }
}