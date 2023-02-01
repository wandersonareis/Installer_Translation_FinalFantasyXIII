using Installer.Common.Localizations;

namespace Installer.Common.Framework.Extensions;

public static class ConvertersExt
{
    public static string ConvertFromUnixTimestamp(this string timestamp, string format = "dd-MM-yyyy")
    {
        return timestamp == "0" ? Localization.Instance.WithoutInstalledVersion : DateTimeOffset.FromUnixTimeSeconds(long.Parse(timestamp)).ToLocalTime().ToString(format);
    }
}