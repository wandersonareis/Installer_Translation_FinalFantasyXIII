using Installer.Common.Framework;

namespace Installer.LightningReturnFF13.Shared.Interfaces;

public interface IBackupProvider
{
    Task Backup(LoadingHandler progress);
}