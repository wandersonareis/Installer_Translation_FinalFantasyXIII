namespace Installer.Package;

public interface IPackageInfo
{
    bool IsValid(string id, string hash);
    string ReadFileId();
    Task GetPackageTranslation();
    void Validate();
}