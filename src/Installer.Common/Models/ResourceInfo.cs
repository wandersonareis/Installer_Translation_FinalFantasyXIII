namespace Installer.Common.Models;

public record ResourceInfo
{
    public string FullPath { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}