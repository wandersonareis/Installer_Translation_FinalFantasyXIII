using Installer.Common.Framework;
using Installer.Common.Framework.Extensions;
using Installer.Common.localization;
using Installer.Common.Logger;
using Installer.Common.Service;
using Installer.LightningReturnFF13.Shared.Interfaces;

namespace Installer.LightningReturnFF13.Shared.Classes;

public class BackupProvider : IBackupProvider
{
    private readonly InstallerServiceProvider _installerServiceProvider;
    private readonly ILogger _logger;

    public BackupProvider(InstallerServiceProvider installerServiceProvider)
    {
        _installerServiceProvider = installerServiceProvider;
        _logger = LogManager.GetLogger();
    }

    public async Task Backup(LoadingHandler progress)
    {
        progress.IsLoading = true;
        progress.TotalSteps = 100;
        _logger.Info(Localization.Localizer.Get("Messages.MakingBackup"));

        if (!_installerServiceProvider.GameLocationInfo.BackupDirectory.DirectoryIsExists())
            Directory.CreateDirectory(_installerServiceProvider.GameLocationInfo.BackupDirectory);

        progress.Title = Localization.Localizer.Get("Messages.GameFilesBackupTitle");
        for (int i = 0; i < _installerServiceProvider.FilesListLrff13.Count; i++)
        {
            string sourceFileList =
                Path.Combine(_installerServiceProvider.GameLocationInfo.SystemDirectory,
                    _installerServiceProvider.FilesListLrff13[i].FileList);
            string sourceWhiteFile =
                Path.Combine(_installerServiceProvider.GameLocationInfo.SystemDirectory,
                    _installerServiceProvider.FilesListLrff13[i].WhiteFile);
            string destinationFileList = Path.Combine(_installerServiceProvider.GameLocationInfo.BackupDirectory,
                _installerServiceProvider.FilesListLrff13[i].FileList);
            string destinationWhiteFile = Path.Combine(_installerServiceProvider.GameLocationInfo.BackupDirectory,
                _installerServiceProvider.FilesListLrff13[i].WhiteFile);

            if (!sourceFileList.FileIsExists() || !sourceWhiteFile.FileIsExists())
            {
                _logger.Warn(string.Format(Localization.Localizer.Get("Warning.GameLanguageFilesNotFounded"),
                    _installerServiceProvider.FilesListLrff13[i],
                    _installerServiceProvider.FilesListLrff13[i].Language));
                continue;
            }

            await sourceFileList.CopyToAsync(destinationFileList, progress);
            await sourceWhiteFile.CopyToAsync(destinationWhiteFile, progress);
        }

        progress.Finish();
    }
}