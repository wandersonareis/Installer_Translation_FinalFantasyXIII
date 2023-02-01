using System.Runtime.Versioning;
using GameFinder.RegistryUtils;
using GameFinder.StoreHandlers.Steam;
using Installer.Common.Abstract;
using Installer.Common.Localizations;
using Installer.Common.Logger;
using Installer.Common.Service;

namespace Installer.Common.GameLocation;

public sealed class GameLocationSteamProvider : ISteamGameLocation
{
    private readonly InstallerServiceProvider _installerServiceProvider;
    private static readonly ILogger Logger = LogManager.GetLogger();

    public GameLocationSteamProvider(InstallerServiceProvider installerServiceProvider)
    {
        _installerServiceProvider = installerServiceProvider;
    }
    [SupportedOSPlatform("windows")]
    public bool Provider()
    {
        var handler = new SteamHandler(registry: new WindowsRegistry());
        SteamGame? game = handler.FindOneGameById(InstallerBase.SteamGameId, out string[] errors);

        if (game == null)
        {
            Logger.Error(Localization.Instance.SteamGameNotFounded);
            return false;
        }

        if (errors.Any()) Logger.Error(string.Join(", ", errors));

        var result = new GameLocationInfo(game.Path);
        if (!result.IsValidGamePath())
        {
            Logger.Info(Localization.Instance.SteamGameNotFounded);
            return false;
        }

        _installerServiceProvider.GameLocationInfo = result;
        _installerServiceProvider.InstallerConfig.GameLocation = result.RootDirectory;
        JsonHelper.SerializeToFile(_installerServiceProvider.InstallerConfig, _installerServiceProvider.ConfigFile);
        return true;
    }
}