namespace Installer.Common.Localizations;

public class English
{
    private LocalizedStrings EnglishStrings { get; }

    public English()
    {
        EnglishStrings = new LocalizedStrings
        {
            PackageFileNotFound = "O pacote com a tradução: {0}<br />Você precisa baixar o pacote antes de tentar instalar a tradução!",
            TranslationUpdateMessageSnack = "Uma nova versão da tradução está disponível.",
            UninstallConfirm = "Tem certeza que quer desinstalar a tradução?",
            UninstallFailed = "É possível que o LRFF13 tenha sido desinstalado ou, durante a desinstalação foi interrompido inesperadamente.",
            UpdateAppMessageSnack = "Uma nova versão do instalador está disponível.",
            WithoutInstalledVersion = "Sem versão instalada!",
            WrongPackage = "Este não é um pacote de tradução para LRFFXIII.",
        };
    }

    public LocalizedStrings GetStrings => EnglishStrings;
}