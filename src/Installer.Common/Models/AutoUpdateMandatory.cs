using System.Text.Json.Serialization;

namespace Installer.Common.Models;

public record AutoUpdateAppMandatory
{
    [JsonPropertyName("minVersion")] public string MinVersion { get; set; } = "1.0";
    [JsonPropertyName("mode")] public int Mode { get; set; } = 1;
    [JsonPropertyName("value")] public bool Value { get; set; } = true;
}