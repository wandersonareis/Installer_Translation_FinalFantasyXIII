namespace Installer.Common.Framework;

public static class LineBreakToHtml
{
    public static string ConvertLineBreaksToHtml(this string text) => text.Replace("\r\n", "<br>").Replace("\n", "<br>");
}