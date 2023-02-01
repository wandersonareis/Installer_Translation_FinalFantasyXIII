#pragma warning disable CS8618
namespace Installer.Common.Localizations;

public record LocalizedStrings
{
    public string BtnInstall { get; init; }
    public string BtnUpdate { get; init; }
    public string BtnUnistall { get; init; }
    public string AppUpdate { get; init; }
    public string AppVersion { get; init; }
    public string ExtractPackageInterrupted { get; init; }
    public string FreeSpace { get; init; }
    public string GameDirectoryNot { get; init; }
    public string GameFilesBackupTitle { get; init; }
    public string GameLocation { get; init; }
    public string GameLocationBySettings { get; init; }
    public string LightningReturnFinalFantasy13 { get; init; }
    public string HttpRequestBroken { get; init; }
    public string InjectingFiles { get; init; }
    public string InjectingFilesSucess { get; init; }
    public string InstallCanceled { get; init; }
    public string InstallTranslationComplete { get; init; }
    public string LocationException { get; init; }
    public string MakeBackup { get; init; }
    public string PackageFileNotFound { get; init; }
    public string PackageDirectoryNotFound { get; init; }
    public string ServerResponse { get; init; }
    public string SteamGameNotFounded { get; init; }
    public string TranslationFromServerDate { get; init; }
    public string TranslationId { get; init; }
    public string TranslationInstallingLoadingTitle { get; init; }
    public string TranslationLocalDate { get; init; }
    public string TranslationUpdateMessage { get; init; }
    public string TranslationUpdateMessageSnack { get; init; }
    public string UninstallComplete { get; init; }
    public string UninstallConfirm { get; init; }
    public string UninstallFailed { get; init; }
    public string UninstallFailedTitle { get; init; }
    public string UpdateAppMessage { get; init; }
    public string UpdateAppMessageSnack { get; init; }
    public string WithoutInstalledVersion { get; init; }
    public string WriteFailed { get; init; }
    public string WrongPackage { get; init; }

    public string ExtractingPackageFiles(int fileCount, int total) => string.Create(null, stackalloc char[60],
        $"Extraindo os arquivos da tradução. {fileCount}/{total}");
    // Add other localized strings as properties
}