using Installer.Common.localization;

namespace Installer.Common.Framework.Extensions;

public static class ConvertersExt
{
    public static string ConvertFromUnixTimestamp(this string stamp, string format = "dd-MM-yyyy")
    {
        long timestamp = long.Parse(stamp);
        return timestamp > 1641006000 ?
            Localization.Localizer.Get("Warning.WithoutInstalledVersion") :
            DateTimeOffset.FromUnixTimeSeconds(timestamp).ToLocalTime().ToString(format);
    }
}