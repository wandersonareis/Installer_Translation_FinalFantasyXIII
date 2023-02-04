using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Installer.Common.Models;

public class AutoUpdateApp
{
    [Required]
    [JsonPropertyName("version")]
    public string Version { get; set; } = "0";

    [Required] [JsonPropertyName("url")] public string Url { get; set; } = "Default";

    [JsonPropertyName("changelog")] public string Changelog { get; set; } = "Default";

    [JsonPropertyName("mandatory")] public AutoUpdateAppMandatory Mandatorys { get; set; } = null!;

    [JsonPropertyName("checksum")] public AutoUpdateAppChecksum Checksum { get; set; } = null!;
}