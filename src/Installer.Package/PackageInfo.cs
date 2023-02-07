using System.Security.Cryptography;
using Installer.Common.Downloader;
using Installer.Common.Framework;
using Installer.Common.Framework.Extensions;
using Installer.Common.Service;

namespace Installer.Package;

public class PackageInfo : IPackageInfo
{
    private readonly InstallerServiceProvider _installerServiceProvider;
    private readonly string _package;

    public PackageInfo(InstallerServiceProvider installerServiceProvider)
    {
        _installerServiceProvider = installerServiceProvider;
        _package = _installerServiceProvider.ResourcesFile;
    }
    public bool IsValid(string id, string hash)
    {
        return _package.FileIsExists() && CompareFileId(id) && CompareToFileHash(hash);
    }

    public void Validate()
    {
        Exceptions.CheckPackageFileNotFoundException(_package);
        Exceptions.WrongMagicException(_installerServiceProvider.ResourcesFile);
    }

    public async Task GetPackageTranslation()
    {
        (string translationId, string translationUrl, string translationHash, _) = await _installerServiceProvider.GetJsonDataAsync();

        if (IsValid(id: translationId, hash: translationHash))
        {
            _installerServiceProvider.PersistenceRegister.SetInstalledTranslation(ReadFileId());
            return;
        }

        _ = Directory.CreateDirectory(@".\Resources");
        await DownloaderManager.DoUpdateAsync(translationUrl, _installerServiceProvider.ResourcesFile);

        _installerServiceProvider.PersistenceRegister.SetInstalledTranslation(translationId);
    }
    public string ReadFileId()
    {
        using Stream stream = TryOpen();
        using BinaryReader br = new(stream);
        br.BaseStream.Position = 8;

        return br.ReadInt64().ToString();
    }

    private Stream TryOpen()
    {
        Exceptions.CheckPackageFileNotFoundException(_package);
        return new FileStream(_package, FileMode.Open, FileAccess.Read);
    }
    private bool CompareFileId(string value)
    {
        if (!_package.FileIsExists()) return false;

        using Stream stream = TryOpen();

        return stream.Length >= 9367000 && ReadFileId().Equals(value, StringComparison.OrdinalIgnoreCase);
    }
    private bool CompareToFileHash(string hashSource)
    {
        if (!_package.FileIsExists()) return false;

        using Stream stream = TryOpen();

        using var sha256 = SHA256.Create();
        {
            byte[] bytes = sha256.ComputeHash(stream);
            string result = BitConverter.ToString(bytes).Replace("-", "");

            return result.Equals(hashSource, StringComparison.Ordinal);
        }
    }
}