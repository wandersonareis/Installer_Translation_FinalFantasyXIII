using Installer.Common.Framework;

namespace Installer.Package;

public interface IPackageReader
{
    Task<string> ReadPackage(LoadingHandler progress);
}