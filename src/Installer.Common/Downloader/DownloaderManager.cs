using System.Text.Json;
using Installer.Common.Models;

namespace Installer.Common.Downloader;

public class DownloaderManager
{
    public static async Task<JsonData> GetApiJsonAsync(string uri)
    {
        var api = new ServerApi(uri);
        using HttpResponseMessage httpResponse = await api.GetHttpResponse();

        if (httpResponse.Content.Headers.ContentType?.MediaType != "application/json")
            throw new HttpRequestException("HTTP Response was invalid and cannot be deserialised.");

        await using Stream contentStream = await httpResponse.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<JsonData>(contentStream) ?? throw new InvalidOperationException();
    }

    public static async Task DoUpdateAsync(string downloadUrl, string updateFile)
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