using System.Text.Json;

namespace Installer.Common;

public class JsonHelper
{
    public static JsonSerializerOptions? GetDefaultSerializerOptions(bool prettyPrint = false)
    {
        JsonSerializerOptions? options = new()
        {
            WriteIndented = prettyPrint,
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            PropertyNameCaseInsensitive = true,
        };

        return options;
    }

    public static T? Deserialize<T>(Stream stream)
    {
        using BinaryReader reader = new(stream);
        return JsonSerializer.Deserialize<T>(reader.ReadBytes((int)(stream.Length - stream.Position)), GetDefaultSerializerOptions());
    }

    public static T? DeserializeFromFile<T>(string path)
    {
        return Deserialize<T>(File.ReadAllText(path));
    }

    private static T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, GetDefaultSerializerOptions());
    }

    public static void Serialize<TValue>(Stream stream, TValue obj, bool prettyPrint = false)
    {
        using var writer = new BinaryWriter(stream);
        writer.Write(SerializeToUtf8Bytes(obj, prettyPrint));
    }

    public static string Serialize<TValue>(TValue obj, bool prettyPrint = false)
    {
        return JsonSerializer.Serialize(obj, GetDefaultSerializerOptions(prettyPrint));
    }
    public static void SerializeToFile<TValue>(TValue obj, string configFile, bool prettyPrint = true)
    {
        File.WriteAllText(configFile, JsonSerializer.Serialize(obj, GetDefaultSerializerOptions(prettyPrint)));
    }

    public static byte[] SerializeToUtf8Bytes<T>(T obj, bool prettyPrint = false)
    {
        return JsonSerializer.SerializeToUtf8Bytes(obj, GetDefaultSerializerOptions(prettyPrint));
    }
}