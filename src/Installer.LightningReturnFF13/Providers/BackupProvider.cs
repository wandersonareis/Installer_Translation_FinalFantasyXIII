using Installer.Common.Framework;
using Installer.Common.Framework.Extensions;
using Installer.Common.localization;
using Installer.Common.Logger;
using Installer.Common.Models;
using Installer.Common.Service;
using Installer.LightningReturnFF13.Shared.Interfaces;

namespace Installer.LightningReturnFF13.Providers;

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
        string bckMsg = Localization.Localizer.Get("Messages.MakingBackup");

        if (!_installerServiceProvider.GameLocationInfo.BackupDirectory.DirectoryIsExists())
            Directory.CreateDirectory(_installerServiceProvider.GameLocationInfo.BackupDirectory);

        progress.Title = Localization.Localizer.Get("Messages.GameFilesBackupTitle");

        foreach (GameSysFiles item in _installerServiceProvider.FilesListLrff13)
        {
            string sourceFileList =
                Path.Combine(_installerServiceProvider.GameLocationInfo.SystemDirectory,
                    item.FileList);
            string sourceWhiteFile =
                Path.Combine(_installerServiceProvider.GameLocationInfo.SystemDirectory,
                    item.WhiteFile);
            string destinationFileList = Path.Combine(_installerServiceProvider.GameLocationInfo.BackupDirectory,
                item.FileList);
            string destinationWhiteFile = Path.Combine(_installerServiceProvider.GameLocationInfo.BackupDirectory,
                item.WhiteFile);

            if (!sourceFileList.FileIsExists() || !sourceWhiteFile.FileIsExists())
            {
                _logger.Warn(string.Format(Localization.Localizer.Get("Warning.GameLanguageFilesNotFounded"),
                    item,
                    item.Language));
                continue;
            }

            BckLog(Path.GetRelativePath(_installerServiceProvider.GameLocationInfo.RootDirectory, sourceFileList),
                Path.GetRelativePath(_installerServiceProvider.GameLocationInfo.RootDirectory, destinationFileList));
            BckLog(Path.GetRelativePath(_installerServiceProvider.GameLocationInfo.RootDirectory, sourceWhiteFile),
                Path.GetRelativePath(_installerServiceProvider.GameLocationInfo.RootDirectory, destinationWhiteFile));

            await sourceFileList.CopyToAsync(destinationFileList, progress);
            await sourceWhiteFile.CopyToAsync(destinationWhiteFile, progress);
        }

        progress.Finish();
        return;

        void BckLog(string source, string destination)
        {
            _logger.Info($"{bckMsg} - {source} to {destination}");
        }
    }
}