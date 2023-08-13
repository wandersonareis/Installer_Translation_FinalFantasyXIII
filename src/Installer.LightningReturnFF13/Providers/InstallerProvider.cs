using Game.Injector;
using Installer.Common.Framework;
using Installer.Common.localization;
using Installer.Common.Logger;
using Installer.Common.Models;
using Installer.Common.Service;
using Installer.LightningReturnFF13.Shared.Interfaces;
using Installer.Package;

namespace Installer.LightningReturnFF13.Providers;

public class InstallerProvider : ITranslationIntaller
{
    private readonly InstallerServiceProvider _installerServiceProvider;

    private readonly ILogger _logger;

    private readonly IGameFilesInserter _gameFilesInserter;

    private readonly IPackageReader _packageReader;

    private readonly IBackupProvider _backupProvider;

    public InstallerProvider(InstallerServiceProvider installerServiceProvider, IGameFilesInserter gameFilesInserter, IPackageReader packageReader, IBackupProvider backupProvider)
    {
        _installerServiceProvider = installerServiceProvider;
        _gameFilesInserter = gameFilesInserter;
        _packageReader = packageReader;
        _backupProvider = backupProvider;
        _logger = LogManager.GetLogger();
    }

    public async Task Install(LoadingHandler progress)
    {
        await _backupProvider.Backup(progress);
        string text = await _packageReader.ReadPackage(progress);
        if (string.IsNullOrEmpty(text))
        {
            throw new DirectoryNotFoundException(Localization.Localizer.Get("Exceptions.PackageDirectoryNotFounded"));
        }
        progress.Start();
        progress.TotalSteps = _installerServiceProvider.FilesListLrff13.Length;
        _gameFilesInserter.Initializer(text);
        progress.Title = string.Format(Localization.Localizer.Get("Messages.LoadingTitleTranslationInstalling"), Localization.Localizer.Get("Messages.LightningReturnFinalFantasy13"));
        _logger.Info(progress.Title);
        GameSysFiles[] filesListLrff = _installerServiceProvider.FilesListLrff13;
        foreach (GameSysFiles gameSysFiles in filesListLrff)
        {
            await _gameFilesInserter.Insert(gameSysFiles.FileList, gameSysFiles.WhiteFile, gameSysFiles.FileFolder, _installerServiceProvider.GameLocationInfo.SystemDirectory);
            progress.CurrentStep++;
        }
        foreach (GameSysFiles item in _installerServiceProvider.DlcFilesLrff13)
        {
            await _gameFilesInserter.Insert(item.FileList, item.WhiteFile, item.FileFolder, _installerServiceProvider.GameLocationInfo.DlcDirectory);
        }
        _gameFilesInserter.Dispose();
        progress.Finish();
    }
}