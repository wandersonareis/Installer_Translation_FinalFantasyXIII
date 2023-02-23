namespace Installer.Common;

public interface IPersistenceRegisterProvider
{
    string GetGamePath();

    long GetInstalledTranslation();

    void SetGamePath(string path);

    void SetInstalledTranslation(string id);
}