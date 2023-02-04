using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Installer.Common.Models;

public record AutoUpdateTranslation
{
    [Required] public string Hash { get; init; } = string.Empty;
    public string PackageChangelog { get; init; } = string.Empty;
    
    [Required]
    [JsonPropertyName("TranslationID")]
    public string TranslationId { get; init; } = string.Empty;
    [Required] public string TranslationUrl { get; init; } = string.Empty;
}