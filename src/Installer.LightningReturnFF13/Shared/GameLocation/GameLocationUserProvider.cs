using Installer.Common;
using Installer.Common.GameLocation;
using Installer.LightningReturnFF13.Shared.Interfaces;

namespace Installer.LightningReturnFF13.Shared.GameLocation;

public class GameLocationUserProvider : IUserGameLocation
{
    private readonly IFolderBrowserService _folderBrowserService;
    private readonly IPersistenceRegisterProvider _persistenceRegisterProvider;

    public GameLocationUserProvider(IFolderBrowserService folderBrowserService, IPersistenceRegisterProvider persistenceRegisterProvider)
    {
        _folderBrowserService = folderBrowserService;
        _persistenceRegisterProvider = persistenceRegisterProvider;
    }
    public bool Provider()
    {
        string folder = _folderBrowserService.DisplayFolderPicker();
        if (folder == "") return false;

        IGameLocationInfo gameLocation = new GameLocationInfo(folder);
        _persistenceRegisterProvider.SetGamePath(folder);
        return gameLocation.IsValidGamePath();
    }
}