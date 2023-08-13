using Installer.Common.Models;

namespace Installer.Common.Abstract;

public abstract class InstallerBase
{
    public const string MessageTitle = "Lightning Return FF13 PT-BR";
    public const int SteamGameId = 345350;
    //protected const string PackageFileName = "[CentralDeTraducoes.net.br]Lightning_Return_FF13.bin";
    protected const string PackageRelatieFileName = "Lightning_Return_FF13.bin";

    protected const string DataUriLrff13 = "https://api.npoint.io/b7fa9dd201b5e145b492";

    private protected string AppDirectory => AppDomain.CurrentDomain.BaseDirectory;
    private protected string ResourcesDirectory => Path.Combine(AppDirectory, "Resources");

    public string PatchDirectory { get; } = Path.GetTempPath() + Guid.NewGuid();

    protected GameSysFiles[] FilesLrff13 =
    {
        new()
        {
            FileList = "filelist2a.win32.bin", WhiteFile = "white_img2a.win32.bin", FileFolder = "white_img2a",
            Language = "inglês"
        },
        new()
        {
            FileList = "filelista.win32.bin", WhiteFile = "white_imga.win32.bin", FileFolder = "white_imga",
            Language = "inglês"
        },
        new()
        {
            FileList = "filelist2v.win32.bin", WhiteFile = "white_img2v.win32.bin", FileFolder = "white_img2a",
            Language = "japonês"
        },
        new()
        {
            FileList = "filelistv.win32.bin", WhiteFile = "white_imgv.win32.bin", FileFolder = "white_imga",
            Language = "japonês"
        }
    };

    protected IEnumerable<GameSysFiles> DlcLrff13 => Enumerable.Range(1, 15)
        .Select(i =>
        {
            var directoryName = $"{i:D7}";
            string fileList = Path.Combine(directoryName,
                $"filelist_p{directoryName}img_a.win32.bin");
            string whiteFile = Path.Combine(directoryName,
                $"white_p{directoryName}img_a.win32.bin");

            return new GameSysFiles
            {
                FileList = fileList,
                WhiteFile = whiteFile,
                FileFolder = "white_imga"
            };
        });

    public void MoveFiles(string source, string des) => File.Move(sourceFileName: source, destFileName: des, overwrite: true);

    protected void MoveDirectory(string source, string des)
    {
        if (!Directory.Exists(path: des)) Directory.CreateDirectory(path: des);
        string[] files = Directory.GetFiles(path: source, searchPattern: "*.*", searchOption: SearchOption.TopDirectoryOnly);
        foreach (string file in files)
        {
            if (File.Exists(path: Path.Combine(path1: des, path2: Path.GetFileName(path: file)))) File.Delete(path: Path.Combine(path1: des, path2: Path.GetFileName(path: file)));
            File.Move(sourceFileName: file, destFileName: Path.Combine(path1: des, path2: Path.GetFileName(path: file)));
        }
        string[] folders = Directory.GetDirectories(path: source);
        foreach (string folder in folders)
        {
            MoveDirectory(source: folder, des: Path.Combine(path1: des, path2: Path.GetFileName(path: folder)));
        }
    }
}