using Installer.Common.Localizations;
using Installer.Common.Logger;

namespace Installer.Common.Framework;

public class ServiceException : Exception
{
    private static readonly ILogger Logger = LogManager.GetLogger();

    public ServiceException(string message) : base(message) { }

    public ServiceException(string message, Exception innerException) : base(message, innerException)
    {
        Logger.Error(innerException, message);
    }
}

public class GameFileNotFoundException : Exception
{
    private static readonly ILogger Logger = LogManager.GetLogger();
    public GameFileNotFoundException(string message) : base(message)
    {
        Logger.Error(message);
    }

    public GameFileNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
        Logger.Error(innerException, message);
    }
}

public class PackageFileNotFoundException : Exception
{
    private static readonly ILogger Logger = LogManager.GetLogger();
    private readonly string _message;

    public PackageFileNotFoundException(string message) : base(message)
    {
        _message = message;
        Logger.Error(message);
    }

    public PackageFileNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
        _message = message;
        Logger.Error(innerException, message);
    }
    public override string Message => string.Format(Localization.Instance.PackageFileNotFound, _message[AppDomain.CurrentDomain.BaseDirectory.Length..]);
}

public class PackageMagicException : Exception
{
    private static readonly ILogger Logger = LogManager.GetLogger();
    private readonly string _message;

    public PackageMagicException(string message) : base(message)
    {
        _message = message;
        Logger.Error(message);
    }

    public PackageMagicException(string message, Exception innerException) : base(message, innerException)
    {
        _message = message;
        Logger.Error(innerException, message);
    }
    public override string Message => Localization.Instance.WrongPackage;
}
