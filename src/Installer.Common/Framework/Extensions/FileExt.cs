namespace Installer.Common.Framework.Extensions;

public static class FileExt
{
    public static bool FileIsExists(this string path)
    {
        var fi = new FileInfo(path);
        return fi.Exists && fi.Length > 0;
    }
    public static bool DirectoryIsExists(this string directoryPath)
    {
        return Directory.Exists(directoryPath);
    }

    public static void DeleteEvenWhenUsed(this string path, bool recursive = true)
    {
        if (path.DirectoryIsExists())
        {
            Retry(() => Directory.Delete(path, recursive));
        }
        if (path.FileIsExists())
        {
            Retry(() => File.Delete(path));
        }
    }
    public static async Task DeleteEvenWhenUsedAsync(this string path, bool recursive = true)
    {
        if (path.DirectoryIsExists())
        {
            await RetryAsync(() => Directory.Delete(path, recursive));
        }
        else if (path.FileIsExists())
        {
            await RetryAsync(() => File.Delete(path));
        }
    }

    private static void Retry(Action action, int retryNum = 15, int delay = 400)
    {
        for (var i = 0; i < retryNum; i++)
        {
            try
            {
                action();
                return;
            }
            catch (Exception)
            {
                Thread.Sleep(delay);
            }
        }
        action();
    }

    private static async Task RetryAsync(Action action, int retryNum = 15, int delay = 400)
    {
        for (var i = 0; i < retryNum; i++)
        {
            try
            {
                action();
                return;
            }
            catch (Exception)
            {
                await Task.Delay(delay);
            }
        }
        action();
    }
}
