using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Installer.Common.Models;

public record JsonData
{
    public void Deconstruct(out string translationId, out string appVersion)
    {
        translationId = UpdateTranslation.TranslationId;
        appVersion = UpdateApp.Version;
    }

    public void Deconstruct(out string translationId, out string translationUrl, out string translationChangelog)
    {
        translationId = UpdateTranslation.TranslationId;
        translationUrl = UpdateTranslation.TranslationUrl;
        translationChangelog = UpdateTranslation.PackageChangelog;
    }

    [Required]
    [JsonPropertyName("AutoUpdateApp")]
    public AutoUpdateApp UpdateApp { get; set; } = null!;

    [Required]
    [JsonPropertyName("UpdateTranslation")]
    public AutoUpdateTranslation UpdateTranslation { get; set; } = null!;
}