using Installer.Common.Abstract;
using Installer.Common.Downloader;
using Installer.Common.GameLocation;
using Installer.Common.localization;
using Installer.Common.Models;

namespace Installer.Common.Service;

public class InstallerServiceProvider : InstallerBase
{
    public InstallerConfig InstallerConfig;
    public IGameLocationInfo GameLocationInfo;

    public readonly string ConfigFile;
    public readonly string ResourcesFile;
    public JsonData JsonData => DownloaderManager.Instance.GetApiJson(DataUriLrff13).GetAwaiter().GetResult();
    public string UriLrff13 => DataUriLrff13;

    public InstallerServiceProvider()
    {
        ConfigFile = Path.Combine(AppDirectory, "Config", "config.json");
        ResourcesFile = Path.Combine(AppDirectory, "Resources", PackageFileName);
        InstallerConfig = new InstallerConfig();
        GameLocationInfo = new GameLocationInfo(InstallerConfig.GameLocation);
    }

    public async Task<JsonData> GetJsonDataAsync() => await DownloaderManager.GetApiJsonAsync(DataUriLrff13);
    public string ExtractingPackageFiles(int fileCount, int total) => string.Format(Localization.Localizer.Get("Messages.ExtractingPackageFiles"),fileCount, total);
}