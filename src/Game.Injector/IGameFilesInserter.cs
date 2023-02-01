namespace Game.Injector;

public interface IGameFilesInserter : IDisposable
{
    void Initializer(string tempPath);
    Task Insert(string fileList, string whiteFile, string folder);
}