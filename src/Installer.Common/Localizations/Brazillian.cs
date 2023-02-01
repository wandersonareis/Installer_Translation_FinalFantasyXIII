namespace Installer.Common.Localizations;

public class Brazillian
{
    private static LocalizedStrings BrazilianStrings { get; set; } = null!;

    public Brazillian()
    {
        BrazilianStrings = new LocalizedStrings
        {
            AppUpdate = "Fazendo download da atualização do programa!",
            AppVersion = "<b>Versão do programa:</b> v",
            BtnInstall = "Instalar tradução",
            BtnUnistall = "Desinstalar tradução",
            BtnUpdate = "Atualizar tradução",
            PackageDirectoryNotFound = "Diretório de extração do pacote não encontrado!",
            PackageFileNotFound = "O pacote com a tradução: {0}<br />Você precisa baixar o pacote antes de tentar instalar a tradução!",
            UninstallConfirm = "Tem certeza que quer desinstalar a tradução?",
            UninstallFailed = "É possível que o LRFF13 tenha sido desinstalado ou, durante a desinstalação foi interrompido inesperadamente.",
            UpdateAppMessageSnack = "Uma nova versão do instalador está disponível.",
            WithoutInstalledVersion = "Sem versão instalada!",
            WrongPackage = "Este não é um pacote de tradução para LRFFXIII.",
            FreeSpace =
        "<b>Aviso:</b><br />Para instalar a tradução, a unidade precisa ter cerca de 5 GB livres.<br />Sua unidade tem {0} de espaço livre.<br /><br />Continuar?",
            GameDirectoryNot = "Como não pode ter diretório do jogo?",
            GameFilesBackupTitle = "Preparando arquivos para desinstalação",
            GameLocation = "LIGHTNING RETURNS FINAL FANTASY XIII",
            GameLocationBySettings = "Jogo encontrado pelo histórico do aplicativo.",
            LightningReturnFinalFantasy13 = "Lightning Return FF13 pt-BR",
            HttpRequestBroken = "Sem resposta do servidor! Você está conectado à internet?",
            InjectingFiles = "Inserindo tradução em {0} e {1}",
            InjectingFilesSucess = "Arquivos inserido com sucesso.",
            InstallCanceled = "Instalação da tradução cancelada.",
            InstallTranslationComplete = "Tradução instalada com sucesso!",
            LocationException = "Selecione a pasta correta do jogo.",
            MakeBackup = "Fazendo cópia de segurança",
            ServerResponse = "Obtendo informações do servidor",
            SteamGameNotFounded = "Versão steam do jogo não encontrada!",
            TranslationId = "Id da tradução: {0}",
            TranslationInstallingLoadingTitle = "Instalando a tradução em {0}",
            TranslationLocalDate = "<b>Data da tradução atual:</b> ",
            TranslationFromServerDate = "<b>Data da tradução no servidor:</b> ",
            TranslationUpdateMessage = """
                                        Uma nova versão da tradução está disponível.
                                        Deseja atualizar?
                                        """,
            TranslationUpdateMessageSnack = "Uma nova versão da tradução está disponível.",
            UninstallComplete = "Desinstalação completa.",
            UninstallFailedTitle = "Falha na desinstalação!",
            WriteFailed = """
                            O arquivo não pôde ser gravado.
                            Você pode estar instalando no diretório somente leitura ou o programa não tem permissões.
                            Verifique o diretório ou execute o programa abaixo. Direitos de administrador.
                            """,
        };
    }

    public LocalizedStrings GetStrings => BrazilianStrings;
}