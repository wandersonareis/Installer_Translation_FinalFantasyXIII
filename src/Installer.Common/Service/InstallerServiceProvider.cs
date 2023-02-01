using Installer.Common.Abstract;
using Installer.Common.Downloader;
using Installer.Common.GameLocation;
using Installer.Common.Models;

namespace Installer.Common.Service;

public class InstallerServiceProvider : InstallerBase
{
    public InstallerConfig InstallerConfig;
    public IGameLocationInfo GameLocationInfo;

    public readonly string ConfigFile;
    public readonly string ResourcesFile;
    private readonly string _appUpdate;

    public InstallerServiceProvider()
    {
        ConfigFile = Path.Combine(AppDirectory, "Config", "config.json");
        ResourcesFile = Path.Combine(AppDirectory, "Resources", PackageFileName);
        _appUpdate = Path.Combine(AppDirectory, InstallerUpdateName);

        InstallerConfig = new InstallerConfig();
        GameLocationInfo = new GameLocationInfo(InstallerConfig.GameLocation);
    }

    public async Task<JsonData> GetServerData() => await DownloaderManager.Instance.GetApiJson(DataUriLrff13);

    //public async ValueTask CheckApp(JsonData json)
    //{
    //    await _appUpdate.DeleteEvenWhenUsedAsync();
    //    await DownloaderManager.Instance.DoUpdate(json.UpdateUrl, _appUpdate);
    //    Thread.Sleep(1000);
    //    Cli.Wrap(_appUpdate).ExecuteAsync().GetAwaiter();
    //    MediaTypeNames.Application.Exit();
    //}
}