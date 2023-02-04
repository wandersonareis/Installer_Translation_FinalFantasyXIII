using System.Text.Json.Serialization;

namespace Installer.Common.Models;

public record AutoUpdateAppChecksum
{
    [JsonPropertyName("value")] public string Value { get; set; } = "Default";
    [JsonPropertyName("hashingAlgorithm")] public string HashingAlgorithm { get; set; } = "Default";
}