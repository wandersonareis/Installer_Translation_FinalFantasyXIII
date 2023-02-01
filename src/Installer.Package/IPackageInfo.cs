using Installer.Common.Models;

namespace Installer.Package;

public interface IPackageInfo
{
    bool IsValid(JsonData json);
    void Validate();
    Task GetPackageTranslation();
}