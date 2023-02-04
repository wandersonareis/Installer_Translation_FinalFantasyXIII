using System.Reflection;
using libc.translation;

namespace Installer.Common.localization;

public class Localization
{
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
    private const string ResourceId = "Installer.Common.localization.localizations.json";
    private static readonly ILocalizationSource Source = new JsonLocalizationSource(Assembly, ResourceId, PropertyCaseSensitivity.CaseSensitive);
    public static readonly ILocalizer Localizer = new Localizer(Source, "en");
}