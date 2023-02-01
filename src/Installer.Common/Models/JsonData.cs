using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Installer.Common.Models;

public record JsonData
{
    public void Deconstruct(out string translationId, out string appVersion)
    {
        translationId = TranslationId;
        appVersion = AppVersion;
    }

    [Required]
    [JsonPropertyName("TranslationID")]
    public string TranslationId { get; init; } = string.Empty;

    [Required] public string TranslationUrl { get; init; } = string.Empty;
    public string PackageChangelog { get; init; } = string.Empty;

    [Required] public string AppVersion { get; init; } = string.Empty;
    public string AppUrl { get; init; } = string.Empty;

    [Required] public string UpdateUrl { get; init; } = string.Empty;
    public string AppChangelog { get; init; } = string.Empty;

    [Required] public string Hash { get; init; } = string.Empty;
}