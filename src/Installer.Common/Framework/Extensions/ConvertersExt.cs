using Installer.Common.localization;

namespace Installer.Common.Framework.Extensions;

public static class ConvertersExt
{
    public static string ConvertFromUnixTimestamp(this string timestamp, string format = "dd-MM-yyyy") =>
        long.Parse(timestamp).ConvertFromUnixTimestamp(format);

    public static string ConvertFromUnixTimestamp(this long timestamp, string format = "dd-MM-yyyy") =>
        timestamp < 1641006000 ?
            Localization.Localizer.Get("Warning.WithoutInstalledVersion") :
            DateTimeOffset.FromUnixTimeSeconds(timestamp).ToLocalTime().ToString(format);
}