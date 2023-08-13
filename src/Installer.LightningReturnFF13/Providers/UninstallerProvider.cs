using System.Diagnostics;
using Installer.Common.Framework;
using Installer.Common.Framework.Extensions;
using Installer.Common.localization;
using Installer.Common.Logger;
using Installer.Common.Models;
using Installer.Common.Service;
using Installer.LightningReturnFF13.Shared.Interfaces;
using MudBlazor;

namespace Installer.LightningReturnFF13.Providers;

public class UninstallerProvider : IUninstallerProvider
{
    private readonly InstallerServiceProvider _installerServiceProvider;
    private readonly ILogger _logger;
    private readonly IDialogService _dialogService;

    public UninstallerProvider(IDialogService dialogService, InstallerServiceProvider installerServiceProvider)
    {
        _dialogService = dialogService;
        _installerServiceProvider = installerServiceProvider;
        _logger = LogManager.GetLogger();
    }
    public async ValueTask TranslationUninstall()
    {
        if (!_installerServiceProvider.GameLocationInfo.BackupDirectory.DirectoryIsExists())
            throw new ServiceException(Localization.Localizer.Get("Messages.UninstallFailed"));

        _logger.Info("Restaurando backup");

        foreach (GameSysFiles queueDataFile in _installerServiceProvider.FilesListLrff13)
        {
            string backupFileList = Path.Combine(_installerServiceProvider.GameLocationInfo.BackupDirectory, queueDataFile.FileList);
            string backupWhiteFile = Path.Combine(_installerServiceProvider.GameLocationInfo.BackupDirectory, queueDataFile.WhiteFile);

            if (!backupFileList.FileIsExists() && !backupWhiteFile.FileIsExists())
            {
                await _dialogService.ShowMessageBox(
                    new StackFrame(1).GetMethod()?.DeclaringType?.Name,
                    string.Format(Localization.Localizer.Get("Warning.GameLanguageFilesNotFounded"), queueDataFile, queueDataFile.Language),
                    yesText: "Ok!");
                continue;
            }

            _installerServiceProvider.MoveFiles(backupFileList, Path.Combine(_installerServiceProvider.GameLocationInfo.SystemDirectory, queueDataFile.FileList));
            _installerServiceProvider.MoveFiles(backupWhiteFile, Path.Combine(_installerServiceProvider.GameLocationInfo.SystemDirectory, queueDataFile.WhiteFile));
        }

        await _installerServiceProvider.GameLocationInfo.BackupDirectory.DeleteEvenWhenUsedAsync();
    }
}