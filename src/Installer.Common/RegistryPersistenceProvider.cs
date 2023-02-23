using System.Runtime.Versioning;
using Installer.Common.Framework.Extensions;
using Installer.Common.localization;
using Microsoft.Win32;

namespace Installer.Common;

/// <summary>
/// Provides a mechanism for storing data on the Windows Registry.
/// </summary>
public class RegistryPersistenceProvider : IPersistenceRegisterProvider
{
    /// <summary>
    /// Gets/sets the path for the Windows Registry key that will contain the data.
    /// </summary>
    private string RegistryLocation { get; }

    private const string GamePathValue = "Path";

    private const string TranslationIdValue = "TranslationId";

    public RegistryPersistenceProvider()
    {
        RegistryLocation = $@"Software\{Localization.Localizer.Get("HomePage.Title")}\AutoUpdater"; ;
    }

    [SupportedOSPlatform("windows")]
    public string GetGamePath()
    {
        try
        {
            using RegistryKey? updateKey = Registry.CurrentUser.OpenSubKey(RegistryLocation);
            object? gamePathValue = updateKey?.GetValue(GamePathValue);

            if (gamePathValue != null)
            {
                return gamePathValue.ToString() ?? string.Empty;
            }
        }
        catch (Exception)
        {
            // ignored
        }

        return string.Empty;
    }

    [SupportedOSPlatform("windows")]
    public long GetInstalledTranslation()
    {
        using RegistryKey? updateKey = Registry.CurrentUser.OpenSubKey(RegistryLocation);
        object? translationIdValue = updateKey?.GetValue(TranslationIdValue);

        if (translationIdValue == null) return 0;

        try
        {
            return long.Parse(TranslationIdValue);
        }
        catch (FormatException)
        {
            // ignored
        }

        return 0;
    }

    [SupportedOSPlatform("windows")]
    public void SetGamePath(string path)
    {
        using RegistryKey autoUpdaterKey = Registry.CurrentUser.CreateSubKey(RegistryLocation);
        autoUpdaterKey.SetValue(GamePathValue, path);
    }

    [SupportedOSPlatform("windows")]
    public void SetInstalledTranslation(string id)
    {
        using RegistryKey autoUpdaterKey = Registry.CurrentUser.CreateSubKey(RegistryLocation);
        autoUpdaterKey.SetValue(TranslationIdValue, id);
    }
}