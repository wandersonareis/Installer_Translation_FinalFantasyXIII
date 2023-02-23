using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Installer.Common.Models;

public record JsonData
{
    public void Deconstruct(out string translationId, out string translationChangelog, out string appVersion)
    {
        translationId = UpdateTranslation.TranslationId;
        translationChangelog = UpdateTranslation.TranslationChangelog;
        appVersion = AutoUpdateApp.Version;
    }

    public void Deconstruct(out string translationId, out string translationUrl, out string translationHash, out string translationChangelog)
    {
        translationId = UpdateTranslation.TranslationId;
        translationUrl = UpdateTranslation.TranslationUrl;
        translationHash = UpdateTranslation.Hash;
        translationChangelog = UpdateTranslation.TranslationChangelog;
    }

    [Required]
    [JsonPropertyName("AutoUpdateApp")]
    public AutoUpdateApp AutoUpdateApp { get; init; } = null!;

    [Required]
    [JsonPropertyName("UpdateTranslation")]
    public AutoUpdateTranslation UpdateTranslation { get; init; } = null!;
}

public record AutoUpdateTranslation(string Hash, string TranslationChangelog, string TranslationId, string TranslationUrl);
public record AutoUpdateApp(string Version, string Url, string Changelog, AutoUpdateAppMandatory Mandatory, AutoUpdateAppChecksum Checksum);
public record AutoUpdateAppMandatory(string MinVersion, int Mode, bool Value);
public record AutoUpdateAppChecksum(string Value, string HashingAlgorithm);