using Installer.Common.Framework;
using Installer.Common.Framework.Extensions;

namespace Installer.Common.GameLocation;

public interface IGameLocationInfo
{
    string RootDirectory { get; }
    string SystemDirectory { get; }
    string MovieDirectory { get; }
    string DlcDirectory { get; }
    string AreasDirectory { get; }
    string BackupDirectory { get; }
    string ExecutableRelativePath { get; }
    string ExecutablePath { get; }
    void Validate();
    bool IsValidGamePath();
}

public class GameLocationInfo : IGameLocationInfo
{
    public string RootDirectory { get; }

    public string SystemDirectory { get; }
    public string MovieDirectory { get; }
    public string DlcDirectory { get; }
    public string AreasDirectory { get; }
    public string BackupDirectory { get; }
    public string ExecutableRelativePath => @"LRFF13.exe";

    private const string ResourceDirName = "weiss_data";

    public GameLocationInfo(string rootDirectory)
    {
        RootDirectory = rootDirectory;

        string resourcePath = Path.Combine(RootDirectory, ResourceDirName);
        SystemDirectory = Path.Combine(resourcePath, "sys");
        MovieDirectory = Path.Combine(resourcePath, "movie");
        AreasDirectory = Path.Combine(resourcePath, "zone");
        DlcDirectory = Path.Combine(resourcePath, "dlc");
        BackupDirectory = Path.Combine(RootDirectory, "Backup");
    }

    public string ExecutablePath => Path.Combine(RootDirectory, ExecutableRelativePath);

    public void Validate()
    {
        CustomExceptions.CheckDirectoryNotFoundException(SystemDirectory);
        CustomExceptions.CheckDirectoryNotFoundException(MovieDirectory);
        CustomExceptions.CheckDirectoryNotFoundException(AreasDirectory);
        CustomExceptions.CheckDirectoryNotFoundException(DlcDirectory);
        CustomExceptions.CheckGameFileNotFoundException(ExecutablePath);
    }

    public bool IsValidGamePath()
    {
        return RootDirectory.DirectoryIsExists() && SystemDirectory.DirectoryIsExists() &&
               MovieDirectory.DirectoryIsExists() && AreasDirectory.DirectoryIsExists() &&
               ExecutablePath.FileIsExists();
    }
}
