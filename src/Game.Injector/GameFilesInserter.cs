using System.Text;
using CliWrap;
using Installer.Common.Logger;
using Installer.Common.Service;

namespace Game.Injector;

public sealed class GameFilesInserter : IGameFilesInserter
{
    private readonly InstallerServiceProvider _installerServiceProvider;
    private ILogger _logger = null!;

    private string _tempPath = string.Empty;
    private string _crypt = string.Empty;
    private string _msvcp100 = string.Empty;
    private string _msvcr100 = string.Empty;

    public GameFilesInserter(InstallerServiceProvider installerServiceProvider)
    {
        _installerServiceProvider = installerServiceProvider;
    }
    public void Initializer(string tempPath)
    {
        _logger = LogManager.GetLogger();

        _tempPath = tempPath;
        _crypt = Path.Combine(_installerServiceProvider.GameLocationInfo.SystemDirectory, "ffxiiicrypt.exe");
        _msvcp100 = Path.Combine(_installerServiceProvider.GameLocationInfo.SystemDirectory, "msvcp100.dll");
        _msvcr100 = Path.Combine(_installerServiceProvider.GameLocationInfo.SystemDirectory, "msvcr100.dll");

        File.Copy(sourceFileName: _tempPath + @"\ffxiiicrypt.exe", destFileName: _crypt, true);
        File.Copy(_tempPath + @"\msvcp100.dll", destFileName: _msvcp100, overwrite: true);
        File.Copy(_tempPath + @"\msvcr100.dll", _msvcr100, true);
    }

    public async Task Insert(string filelist, string whiteFile, string folder)
    {
        var stdOutBuffer = new StringBuilder(capacity: 5000);
        var stdErrBuffer = new StringBuilder();

        _ = await Cli.Wrap(targetFilePath: _tempPath + @"\ff13tool.exe")
            .WithArguments(configure: args =>
                args.Add(value: "-i").Add(value: "-all").Add(value: "-ff133").Add(value: filelist).Add(value: whiteFile)
                    .Add(value: Path.Combine(path1: _tempPath, path2: folder)))
            .WithWorkingDirectory(workingDirPath: _installerServiceProvider.GameLocationInfo.SystemDirectory)
            .WithStandardOutputPipe(target: PipeTarget.ToStringBuilder(stringBuilder: stdOutBuffer))
            .WithStandardErrorPipe(target: PipeTarget.ToStringBuilder(stringBuilder: stdErrBuffer))
            .WithValidation(validation: CommandResultValidation.None)
            .ExecuteAsync();


        if (stdOutBuffer.Length > 0) _logger.Info(stdOutBuffer.ToString());

        if (stdErrBuffer.Length > 0)
        {
            _logger.Error($"Houve um erro inesperado no arquivo {filelist}");
            _logger.Error(stdOutBuffer.ToString());
        }
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempPath))
            Directory.Delete(_tempPath, true);

        if (File.Exists(_crypt))
            File.Delete(_crypt);
        if (File.Exists(_msvcp100))
            File.Delete(_msvcp100);
        if (File.Exists(_msvcr100))
            File.Delete(_msvcr100);
    }

    //[GeneratedRegex(@"\b(\d+)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled)]
    //public static partial Regex GetNumbersRegex();
}