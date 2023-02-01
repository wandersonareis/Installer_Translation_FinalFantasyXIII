using Installer.Common.Framework;

namespace Installer.LightningReturnFF13.Shared.Interfaces;

public interface ITranslationIntaller
{
    Task Install(LoadingHandler progress);
}