namespace Installer.Package;

public interface IPackageInfo {
    public bool IsExists { get; set; }
    public bool IsValidMagic { get; set; }
    public bool IsValidId { get; set; }
    public bool IsValidHash { get; set; }
    ValueTask Check();
    string ReadFileId();
    Task GetPackageTranslation();
    void Validate();
}