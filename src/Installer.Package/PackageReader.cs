using System.Text;
using Installer.Common.Framework;
using Installer.Common.Service;
using ZstdSharp;

namespace Installer.Package;

public sealed class PackageReader : IPackageReader
{
    private readonly InstallerServiceProvider _installerServiceProvider;

    public PackageReader(InstallerServiceProvider installerServiceProvider)
    {
        _installerServiceProvider = installerServiceProvider;
    }

    public async Task<string> ReadPackage(LoadingHandler progress)
    {
        progress.IsIndeterminate = true;
        progress.IsLoading = true;

        string patchDirectory = _installerServiceProvider.PatchDirectory;
        string gameLocation = _installerServiceProvider.GameLocationInfo.SystemDirectory;

        await using (FileStream stream = File.OpenRead(_installerServiceProvider.ResourcesFile))
        using (BinaryReader br = new(stream))
        {
            byte[] _ = br.ReadBytes(16);

            int fileCount = br.ReadInt32();
            using Decompressor decompressor = new();
            for (var i = 0; i < fileCount; i++)
            {
                int pathLen = br.ReadInt32();
                string pathName = Encoding.ASCII.GetString(br.ReadBytes(pathLen));
                int dataLen = br.ReadInt32();
                byte[] data = br.ReadBytes(dataLen);

                string newPath = Path.Combine(patchDirectory, pathName);
                Directory.CreateDirectory(Path.GetDirectoryName(newPath) ?? throw new InvalidOperationException());

                bool isCompressed = br.ReadBoolean();

                progress.Title = _installerServiceProvider.ExtractingPackageFiles(i + 1, fileCount);

                if (isCompressed)
                {
                    byte[] decompressedData = decompressor.Unwrap(data).ToArray();
                    await File.WriteAllBytesAsync(newPath, decompressedData);
                }
                else
                {
                    await File.WriteAllBytesAsync(Path.Combine(gameLocation, pathName), data);
                }
            }
        }
        progress.Finish();
        return patchDirectory;
    }
}
