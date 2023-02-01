namespace Installer.Common.Framework.Extensions;

public static class DriveExt
{
    public static long GetDriveFreeSpace(this string? unit)
    {
        DriveInfo drive = Array.Find(DriveInfo.GetDrives(), d => d.Name == unit) ??
                           throw new InvalidOperationException();
        return drive.TotalFreeSpace;
    }

    public static string SizeSuffix(this long value, int decimalPlaces = 2)
    {
        string[] sizeSuffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException(nameof(decimalPlaces)); }
        switch (value)
        {
            case < 0:
                return "-" + (-value).SizeSuffix();
            case 0:
                return string.Format("{0:n" + decimalPlaces + "} B", 0);
        }

        var mag = (int)Math.Log(value, 1024);

        decimal adjustedSize = (decimal)value / (1L << mag * 10);

        if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
        {
            mag += 1;
            adjustedSize /= 1024;
        }

        return string.Format("{0:n" + decimalPlaces + "} {1}",
            adjustedSize,
            sizeSuffixes[mag]);
    }
}