namespace Installer.Common.Framework.Extensions;

public static class FileExt
{
    public static bool FileIsExists(this string path)
    {
        if (string.IsNullOrEmpty(path)) return false;
        var fi = new FileInfo(path);
        return fi is { Exists: true, Length: > 0 };
    }

    public static bool DirectoryIsExists(this string directoryPath) => Directory.Exists(directoryPath);

    public static Task FileDelete(this string path)
    {
        CustomExceptions.CheckArgumentNullOrEmpty(path, nameof(path));

        return FileDeleteInternalAsync(path);
    }

    public static async Task FileDeleteAsync(this string path)
    {
        CustomExceptions.CheckArgumentNullOrEmpty(path, nameof(path));

        await FileDeleteInternalAsync(path);
    }

    public static async Task DirectoryDeleteAsync(this string path, bool recursive = true)
    {
        CustomExceptions.CheckArgumentNullOrEmpty(path, nameof(path));

        await DirectoryDeleteInternalAsync(path, recursive);
    }

    public static Task DirectoryDelete(this string path, bool recursive = true)
    {
        CustomExceptions.CheckArgumentNullOrEmpty(path, nameof(path));

        return DirectoryDeleteInternalAsync(path, recursive);
    }

    public static Task MoveAsync(this string sourceFileName, string destFileName, bool overwrite = true) =>
        Task.Run(() => { File.Move(sourceFileName, destFileName, overwrite); });

    public static Task CopyAsync(this string sourceFileName, string destFileName, bool overwrite = true) =>
        Task.Run(() => { File.Copy(sourceFileName, destFileName, overwrite); });

    public static void DeleteEvenWhenUsed(this string path, bool recursive = true)
    {
        if (path.DirectoryIsExists())
        {
            Retry(() => path.DirectoryDelete(recursive));
        }

        if (path.FileIsExists())
        {
            Retry(() => path.FileDelete());
        }
    }

    public static async Task DeleteEvenWhenUsedAsync(this string path, bool recursive = true)
    {
        if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path), "DeleteEvenWhenUsedAsync");

        if (Directory.Exists(path))
        {
            await RetryAsync(() => DirectoryDeleteInternalAsync(path, recursive));
        }

        if (File.Exists(path))
        {
            await RetryAsync(() => FileDeleteInternalAsync(path));
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

    private static async Task RetryAsync(Func<Task> action, int retryNum = 15, int delay = 400)
    {
        for (var i = 0; i < retryNum; i++)
        {
            try
            {
                await action();
                return;
            }
            catch (Exception)
            {
                await Task.Delay(delay);
            }
        }

        await action();
    }

    private static async Task FileDeleteInternalAsync(string path)
    {
        if (File.Exists(path))
        {
            await Task.Run(() => { File.Delete(path); });
        }
    }

    private static async Task DirectoryDeleteInternalAsync(string path, bool recursive)
    {
        if (Directory.Exists(path))
        {
            await Task.Run(() => { Directory.Delete(path, recursive); });
        }
    }
}
