# Installer_Translation_FinalFantasyXIII_BlazorHybrid

Instalador feito usando winforms e blazor.

Usado para baixar o pacote de tradução do jogo
**Lightning Return Final Fantasy XIII**

Usando *blazor* eu puder trabalhar em um design moderno com toda a praticidade do *html e css*.
Este foi o meu primeiro contato com blazor.

Para este efeito de raio;

![Efeito de raio nas letras](https://media.giphy.com/media/v6nIom7MIzBgAQGS0e/giphy.gif)

Eu usei o efeito de [Raio](https://codepen.io/wikyware-net/pen/mdRVdpw), feito com *html, css e javascript*.
Com isso em mãos, eu usei o site [Html5toGif](https://html5animationtogif.com/html5togif) para converter este efeito em um gif animado. O gif ficou muito pesado - 20mb. Então, converti para *webp animado*, reduzindo o número de frames e a duração.
 O arquivo final ficou com 900kb.
 
 Com a animação pronta, usei:

    background-image: url("images/lightning_effect.webp");
    -webkit-background-clip: text;
    background-size: 50%;
    background-repeat: repeat-x;

*Blazor hybrid* precisa dos arquivos html, css e js no diretório raiz do programa.
Adicionei estes arquivos como recurso inserido e criei uma função para extraí-los na abertura da janela do winforms. Resolvendo o problema destes arquivos se perderem na distribuição.



## Build

Installer_Translation_FinalFantasyXIII_BlazorHybrid requer [DotNet](https://dotnet.microsoft.com/en-us/download) v6+ para funcionar.

### Build a partir do código-fonte

Para  release:

```sh
cd {Diretório do projeto}
dotnet publish -c release --sc -r win-x64 -p:PublishSingleFile=true --self-contained false
```

Ou execute:

```sh
Build.cmd
```
Dentro do diretório do projeto
## License

MIT
