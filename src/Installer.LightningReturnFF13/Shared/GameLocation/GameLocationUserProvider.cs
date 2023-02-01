using Installer.Common.GameLocation;
using Installer.LightningReturnFF13.Shared.Interfaces;

namespace Installer.LightningReturnFF13.Shared.GameLocation;

public class GameLocationUserProvider : IUserGameLocation
{
    private readonly IFolderBrowserService _folderBrowserService;

    public GameLocationUserProvider(IFolderBrowserService folderBrowserService)
    {
        _folderBrowserService = folderBrowserService;
    }
    public bool Provider()
    {
        string folder = _folderBrowserService.DisplayFolderPicker();
        if (folder == "") return false;

        IGameLocationInfo gameLocation = new GameLocationInfo(folder);
        return gameLocation.IsValidGamePath();
    }
}