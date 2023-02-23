namespace Installer.Package;

public interface IPackageInfo
{
    public bool IsExists { get; set; }
    public bool IsValidMagic { get; set; }
    public bool IsValidId { get; set; }
    public bool IsValidHash { get; set; }
    ValueTask Check();
    Task GetPackageTranslation();
    void Validate();
}