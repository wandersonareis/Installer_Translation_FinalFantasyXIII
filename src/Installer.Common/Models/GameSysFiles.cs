namespace Installer.Common.Models;

public record GameSysFiles
{
    public string FileList { get; init; } = "";
    public string WhiteFile { get; init; } = "";
    public string FileFolder { get; init; } = "";
    public string Language { get; init; } = "";

    public override string ToString()
    {
        return $"{FileList} {WhiteFile} {Language}";
    }
}