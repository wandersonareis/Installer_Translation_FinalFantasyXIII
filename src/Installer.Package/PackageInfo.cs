using System.Security.Cryptography;
using Installer.Common.Downloader;
using Installer.Common.Framework;
using Installer.Common.Framework.Extensions;
using Installer.Common.Service;

namespace Installer.Package;

public class PackageInfo : IPackageInfo {
    private readonly InstallerServiceProvider _installerServiceProvider;
    private readonly string _package;

    public bool IsExists { get; set; }
    public bool IsValidMagic { get; set; }
    public bool IsValidId { get; set; }
    public bool IsValidHash { get; set; }

    public PackageInfo(InstallerServiceProvider installerServiceProvider) {
        _installerServiceProvider = installerServiceProvider;
        _package = _installerServiceProvider.ResourcesFile;
    }

    public async ValueTask Check() {
        (long translationId, _, string translationHash, _) = await _installerServiceProvider.GetJsonDataAsync();
        IsExists = _package.FileIsExists();
        IsValidMagic = CompareMagic();
        IsValidId = CompareFileId(translationId);
        IsValidHash = CompareToFileHash(translationHash);
    }

    public void Validate() {
        Exceptions.CheckPackageFileNotFoundException(_package);
        Exceptions.WrongMagicException(_installerServiceProvider.ResourcesFile);
    }

    public async Task DownloadTranslationPackage() {
        (long translationId, string translationUrl, _, _) = await _installerServiceProvider.GetJsonDataAsync();

        await Check();
        if (IsExists && IsValidMagic && IsValidId && IsValidHash) {
            _installerServiceProvider.PersistenceRegister.SetInstalledTranslation(ReadFileId());
            return;
        }

        _ = Directory.CreateDirectory(@".\Resources");
        await DownloaderManager.DoUpdateAsync(translationUrl, _installerServiceProvider.ResourcesFile);

        _installerServiceProvider.PersistenceRegister.SetInstalledTranslation(translationId);
    }
    public long ReadFileId() {
        using Stream stream = TryOpen();
        using BinaryReader br = new(stream);
        br.BaseStream.Position = 8;

        return br.ReadInt64();
    }

    private Stream TryOpen() {
        Exceptions.CheckPackageFileNotFoundException(_package);
        return new FileStream(_package, FileMode.Open, FileAccess.Read);
    }

    private bool CompareMagic() {
        if (!_package.FileIsExists()) return false;
        using Stream stream = TryOpen();
        using BinaryReader br = new(stream);
        long magic = br.ReadInt64();
        return magic == 0x494949584646524c;
    }

    private bool CompareFileId(long value) {
        return _package.FileIsExists() && ReadFileId() >= value;
    }
    private bool CompareToFileHash(string hashSource) {
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