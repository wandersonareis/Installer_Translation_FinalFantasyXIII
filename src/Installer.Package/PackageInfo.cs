using System.Security.Cryptography;
using Installer.Common;
using Installer.Common.Downloader;
using Installer.Common.Framework;
using Installer.Common.Framework.Extensions;
using Installer.Common.Models;
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
    public bool IsValid(JsonData json)
    {
        return _package.FileIsExists() && CompareFileId(json.UpdateTranslation.TranslationId) && CompareToFileHash(json.UpdateTranslation.Hash);
    }

    public void Validate()
    {
        Exceptions.CheckPackageFileNotFoundException(_package);
        Exceptions.WrongMagicException(_installerServiceProvider.ResourcesFile);
    }

    public async Task GetPackageTranslation()
    {
        JsonData json = await _installerServiceProvider.GetJsonDataAsync();
        if (IsValid(json))
        {
            _installerServiceProvider.PersistenceRegister.SetInstalledTranslation(ReadFileId());
            return;
        }

        _ = Directory.CreateDirectory(@".\Resources");
        await DownloaderManager.DoUpdateAsync(json.UpdateTranslation.TranslationUrl, _installerServiceProvider.ResourcesFile);

        _installerServiceProvider.InstallerConfig.TranslationId = json.UpdateTranslation.TranslationId;

        _installerServiceProvider.PersistenceRegister.SetInstalledTranslation(translationId);
    }

    private Stream TryOpen()
    {
        Exceptions.CheckPackageFileNotFoundException(_package);
        return new FileStream(_package, FileMode.Open, FileAccess.Read);
    }
    private string ReadFileId()
    {
        using Stream stream = TryOpen();
        using BinaryReader br = new(stream);
        br.BaseStream.Position = 8;

        return br.ReadInt64().ToString();
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