namespace Installer.Common;

public interface IPersistenceRegisterProvider
{
    string GetGamePath();

    string GetInstalledTranslation();

    void SetGamePath(string path);

    void SetInstalledTranslation(string id);

    string GetDateFromInstalledTranslation();

}