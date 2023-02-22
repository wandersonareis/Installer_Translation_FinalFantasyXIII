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

    public readonly string ResourcesFile;
    public string UriLrff13 => DataUriLrff13;
    public JsonData JsonDataSync => GetJsonDataAsync().GetAwaiter().GetResult();

    public InstallerServiceProvider(IPersistenceRegisterProvider persistenceRegister)
    {
        PersistenceRegister = persistenceRegister;
        GameLocationInfo = new GameLocationInfo(persistenceRegister.GetGamePath());
        ResourcesFile = Path.Combine(AppDirectory, "Resources", PackageFileName);
    }

    public async Task<JsonData> GetJsonDataAsync() => await DownloaderManager.GetApiJsonAsync(DataUriLrff13);

    public string ExtractingPackageFiles(int fileCount, int total) => string.Format(Localization.Localizer.Get("Messages.ExtractingPackageFiles"), fileCount, total);
}