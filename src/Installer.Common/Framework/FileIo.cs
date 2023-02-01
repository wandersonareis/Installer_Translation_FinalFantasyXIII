namespace Installer.Common.Framework;

public static class FileIo
{
    public static async Task CopyFileAsync(Stream source, Stream destination)
    {
        var buffer = new byte[1024 * 1024];
        int numRead;
        while ((numRead = await source.ReadAsync(buffer)) != 0)
        {
            await destination.WriteAsync(buffer.AsMemory(0, numRead));
        }
    }
    public static async Task CopyFileAsync(Stream source, Stream destination, LoadingHandler progress)
    {
        var buffer = new byte[1024 * 1024];
        int readBytes;
        while ((readBytes = await source.ReadAsync(buffer)) > 0)
        {
            await destination.WriteAsync(buffer.AsMemory(0, readBytes));
            progress.CurrentStep = (int)(source.Position * 100 / source.Length);
        }
    }
    internal static void MoveDirectory(string source, string des)
    {
        if (!Directory.Exists(des)) Directory.CreateDirectory(des);
        string[] files = Directory.GetFiles(source, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string file in files)
        {
            if (File.Exists(Path.Combine(des, Path.GetFileName(file)))) File.Delete(Path.Combine(des, Path.GetFileName(file)));
            File.Move(file, Path.Combine(des, Path.GetFileName(file)));
        }
        string[] folders = Directory.GetDirectories(source);
        foreach (string folder in folders)
        {
            MoveDirectory(folder, Path.Combine(des, Path.GetFileName(folder)));
        }
    }
}