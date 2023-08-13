using Installer.Common.Abstract;
using Installer.Common.Downloader;
using Installer.Common.GameLocation;
using Installer.Common.localization;
using Installer.Common.Models;

namespace Installer.Common.Service;

public class InstallerServiceProvider : InstallerBase
{
    public IGameLocationInfo GameLocationInfo;
    public IPersistenceRegisterProvider PersistenceRegister;

    public string ResourcesFileProvider
    {
        get
        {
            Directory.CreateDirectory("./Resources");
            return Directory.GetFiles(ResourcesDirectory, $"*{PackageRelatieFileName}")
                .FirstOrDefault(Path.Combine(ResourcesDirectory, PackageRelatieFileName));
        }
    }

    public string UriLrff13 => DataUriLrff13;
    public JsonData JsonDataSync => GetJsonDataAsync().GetAwaiter().GetResult();

    public InstallerServiceProvider(IPersistenceRegisterProvider persistenceRegister)
    {
        PersistenceRegister = persistenceRegister;
        GameLocationInfo = new GameLocationInfo(persistenceRegister.GetGamePath());
    }

    public GameSysFiles[] FilesListLrff13 => FilesLrff13;
    public IEnumerable<GameSysFiles> DlcFilesLrff13 => DlcLrff13
        .Select(i =>
        {
            string fileList = Path.Combine(GameLocationInfo.DlcDirectory, i.FileList);
            string whiteFile = Path.Combine(GameLocationInfo.DlcDirectory, i.WhiteFile);

            return new GameSysFiles
            {
                FileList = fileList,
                WhiteFile = whiteFile,
                FileFolder = "white_imga"
            };
        })
        .Where(x => File.Exists(x.FileList) && File.Exists(x.WhiteFile))
        .Select(file =>
        {
            string fileList = Path.GetRelativePath(GameLocationInfo.DlcDirectory, file.FileList);
            string whiteFile = Path.GetRelativePath(GameLocationInfo.DlcDirectory, file.WhiteFile);

            return new GameSysFiles
            {
                FileList = fileList,
                WhiteFile = whiteFile,
                FileFolder = file.FileFolder
            };
        });

    public async Task<JsonData> GetJsonDataAsync() => await DownloaderManager.GetApiJsonAsync(DataUriLrff13);
}