namespace SemVer.Json;

public record SemVer
{
    public required string Major { get; set; }
    public required string Minor { get; set; }
    public required string Patch { get; set; }
    public required string Build { get; set; }

    public string ToVersion()
    {
        return $"{Major}.{Minor}.{Patch}.{Build}";
    }
}