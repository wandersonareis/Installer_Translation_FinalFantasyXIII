using System.Runtime.Versioning;
using GameFinder.RegistryUtils;
using GameFinder.StoreHandlers.Steam;
using Installer.Common.Abstract;
using Installer.Common.localization;
using Installer.Common.Logger;
using Installer.Common.Service;

namespace Installer.Common.GameLocation;

public sealed class GameLocationSteamProvider : ISteamGameLocation
{
    private readonly InstallerServiceProvider _installerServiceProvider;
    private readonly IPersistenceRegisterProvider _persistenceRegisterProvider;
    private static readonly ILogger Logger = LogManager.GetLogger();

    public GameLocationSteamProvider(InstallerServiceProvider installerServiceProvider, IPersistenceRegisterProvider persistenceRegisterProvider)
    {
        _installerServiceProvider = installerServiceProvider;
        _persistenceRegisterProvider = persistenceRegisterProvider;
    }

    [SupportedOSPlatform("windows")]
    public bool Provider()
    {
        var handler = new SteamHandler(registry: new WindowsRegistry());
        SteamGame? game = handler.FindOneGameById(InstallerBase.SteamGameId, out string[] errors);

        if (game == null)
        {
            Logger.Error(Localization.Localizer.Get("Exceptions.SteamGameNotFounded"));
            return false;
        }

        if (errors.Any()) Logger.Error(string.Join(", ", errors));

        var result = new GameLocationInfo(game.Path);
        if (!result.IsValidGamePath())
        {
            Logger.Info(Localization.Localizer.Get("Exceptions.SteamGameNotFounded"));
            return false;
        }

        _installerServiceProvider.GameLocationInfo = result;
        _persistenceRegisterProvider.SetGamePath(result.RootDirectory);
        return true;
    }
}