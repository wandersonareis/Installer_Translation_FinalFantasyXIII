using System.Globalization;

namespace Installer.Common.Localizations;

public static class Localization
{
    private static LocalizedStrings? _currentLocalization;

    public static LocalizedStrings Instance => _currentLocalization ??= GetLocalizedStrings();


    public static LocalizedStrings GetLocalizedStrings()
    {
        string currentCultureName = CultureInfo.CurrentCulture.Name;

        string currentLanguage = currentCultureName switch
        {
            "en-US" => "English",
            "pt-BR" => "Brazillian",
            _ => "Brazillian"
        };

        LocalizedStrings currentLocalization = currentLanguage switch
        {
            "English" => new English().GetStrings,
            "Brazillian" => new Brazillian().GetStrings,
            _ => new Brazillian().GetStrings
        };

        return currentLocalization;
    }
}