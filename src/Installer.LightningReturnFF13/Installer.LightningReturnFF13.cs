using System.Reflection;
using Installer.Common.Logger;
using Installer.Common.Models;
using Installer.LightningReturnFF13.Services;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Installer.LightningReturnFF13;

public partial class LightningReturnFf13 : Form
{
    private readonly DateOnly _date = DateOnly.FromDateTime(DateTime.Now);

    public LightningReturnFf13()
    {
        LightningReturnFf13_Load();
        var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("wwwroot/appsettings.json", optional: true, reloadOnChange: true).Build();
        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(config);
        services.AddWindowsFormsBlazorWebView();

        services.AddLrff13Services();

        services.AddMudServices();

        InitializeComponent();
        LogManager.Initialize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"installer-{_date:O}.log"));

        blazorWebView1.HostPage = @"wwwroot\index.html";
        blazorWebView1.Services = services.BuildServiceProvider();
        blazorWebView1.RootComponents.Add<Main>("#app");
    }

    private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
    {
        LogManager.Dispose();
        Environment.Exit(0); //calling Exit kills the background process associated with WebView2
    }

    private void LightningReturnFf13_Load()
    {
        IList<ResourceInfo> resourcesList = new List<ResourceInfo>(11)
        {
            new() { Name = "first.webp", FullPath = "images\\first.webp" },
            new() { Name = "two.webp", FullPath = "images\\two.webp" },
            new() { Name = "third.webp", FullPath = "images\\third.webp" },
            new() { Name = "four.webp", FullPath = "images\\four.webp" },
            new() { Name = "five.webp", FullPath = "images\\five.webp" },
            new()
            {
                Name = "Lightning_Returns_Final_Fantasy_XIII_Logo.webp",
                FullPath = "images\\Lightning_Returns_Final_Fantasy_XIII_Logo.webp"
            },
            new() { Name = "lightning_effect.webp", FullPath = "images\\lightning_effect.webp" },
            new() { Name = "index.html", FullPath = "index.html" },
            new() { Name = "app.min.css", FullPath = "css\\app.min.css" },
            new() { Name = "MudBlazor.min.css", FullPath = "_content\\MudBlazor\\MudBlazor.min.css" },
            new() { Name = "MudBlazor.min.js", FullPath = "_content\\MudBlazor\\MudBlazor.min.js" }
        };

        string[] resourcesNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();

        foreach (string resourceName in resourcesNames)
        {
            ResourceInfo? resObjList =
                resourcesList.FirstOrDefault(resourceObj => resourceName.EndsWith(resourceObj.Name));
            if (resObjList == null) continue;

            string dir =
                Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", resObjList.FullPath));
            using Stream? resStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (resStream == null) continue;

            CreateFile(resStream, dir);
        }

        return;

        static void CreateFile(Stream resStream, string dir)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dir)!);

            using var fileStream = new FileStream(dir, FileMode.Create);
            resStream.CopyTo(fileStream);
        }
    }
}