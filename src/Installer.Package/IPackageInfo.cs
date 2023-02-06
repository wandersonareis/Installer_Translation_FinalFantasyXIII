namespace Installer.Package;

public interface IPackageInfo
{
    bool IsValid(string id, string hash);
    void Validate();
    Task GetPackageTranslation();
}