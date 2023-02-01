namespace Installer.Common.Framework.Extensions;

public static class FileIoExt
{
    public static async Task CopyToAsync(this string source, string destination)
    {
        if (destination.FileIsExists()) return;

        await using FileStream fsIn = File.OpenRead(source);
        await using Stream fsOut = File.Create(destination);
        await FileIo.CopyFileAsync(fsIn, fsOut);
    }
    public static async Task CopyToAsync(this string source, string destination, LoadingHandler progress)
    {
        if (destination.FileIsExists()) return;
        progress.CurrentStep = 0;

        await using FileStream fsIn = File.OpenRead(source);
        await using Stream fsOut = File.Create(destination);
        await FileIo.CopyFileAsync(fsIn, fsOut, progress);
        Thread.Sleep(300);
    }
}