using Game.Injector;
using Installer.Common;
using Installer.Common.GameLocation;
using Installer.Common.Service;
using Installer.LightningReturnFF13.Providers;
using Installer.LightningReturnFF13.Shared.Framework;
using Installer.LightningReturnFF13.Shared.GameLocation;
using Installer.LightningReturnFF13.Shared.Interfaces;
using Installer.Package;
using Microsoft.Extensions.DependencyInjection;

namespace Installer.LightningReturnFF13.Services;

public static class InstallerServices
{
    public static void AddLrff13Services(this IServiceCollection services)
    {
        services
            .AddSingleton<InstallerServiceProvider>()
            .AddTransient<IPersistenceRegisterProvider, RegistryPersistenceProvider>()
            .AddTransient<IFolderBrowserService, FolderPicker>()
            .AddTransient<ITranslationIntaller, InstallerProvider>()
            .AddTransient<IGameFilesInserter, GameFilesInserter>()
            .AddTransient<IUninstallerProvider, UninstallerProvider>()
            .AddTransient<IPackageInfo, PackageInfo>()
            .AddTransient<IPackageReader, PackageReader>()
            .AddTransient<IBackupProvider, BackupProvider>()
            .AddTransient<IInfoProvider, GameLocationProvider>()
            .AddTransient<ISteamGameLocation, GameLocationSteamProvider>()
            .AddTransient<IUserGameLocation, GameLocationUserProvider>();
    }
}