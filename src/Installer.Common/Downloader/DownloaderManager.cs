using System.Text.Json;
using Installer.Common.Models;

namespace Installer.Common.Downloader;

public class DownloaderManager
{
    public static readonly DownloaderManager Instance = new();

    public async Task<JsonData> GetApiJson(string uri)
    {
        try
        {
            var api = new ServerApi(uri);
            using HttpResponseMessage httpResponse = await api.GetHttpResponse();

            if (httpResponse.Content.Headers.ContentType?.MediaType != "application/json")
                throw new HttpRequestException("HTTP Response was invalid and cannot be deserialised.");

            await using var contentStream = await httpResponse.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<JsonData>(contentStream) ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // ReSharper disable once MemberCanBeMadeStatic.Global
    public async Task DoUpdate(string downloadUrl, string updateFile)
    {
        try
        {
            var api = new ServerApi(downloadUrl);
            using HttpResponseMessage response = await api.GetHttpResponse();

            await using Stream remoteFileStream = await response.Content.ReadAsStreamAsync();

            await using FileStream updateFileStream = File.Create(updateFile);
            await remoteFileStream.CopyToAsync(updateFileStream);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}