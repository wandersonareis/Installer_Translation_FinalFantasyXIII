using Installer.Common.GameLocation;
using Installer.Common.localization;
using Installer.Common.Logger;
using Installer.Common.Service;
using Installer.LightningReturnFF13.Shared.Interfaces;

namespace Installer.LightningReturnFF13.Shared.GameLocation;

public class GameLocationProvider : IInfoProvider
{
    private readonly InstallerServiceProvider _installerServiceProvider;
    private readonly ISteamGameLocation _steamGameLocation;
    private readonly IUserGameLocation _userGameLocation;
    private readonly ILogger _logger = LogManager.GetLogger();

    public GameLocationProvider(InstallerServiceProvider installerServiceProvider, ISteamGameLocation steamGameLocation, IUserGameLocation userGameLocation)
    {
        _installerServiceProvider = installerServiceProvider;
        _steamGameLocation = steamGameLocation;
        _userGameLocation = userGameLocation;
    }
    public bool Provider()
    {
        IGameLocationInfo result = new GameLocationInfo(_installerServiceProvider.PersistenceRegister.GetGamePath());
        if (result.IsValidGamePath())
        {
            _logger.Info(Localization.Localizer.Get("Messages.GameLocationBySettings"));
            _installerServiceProvider.GameLocationInfo = result;
            return true;
        }

        return _steamGameLocation.Provider() || _userGameLocation.Provider();
    }
}