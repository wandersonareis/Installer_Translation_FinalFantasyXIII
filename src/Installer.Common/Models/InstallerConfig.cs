namespace Installer.Common.Models;

public record InstallerConfig
{
    public string GameLocation { get; set; } = string.Empty;
    public string TranslationId { get; set; } = "0";
}