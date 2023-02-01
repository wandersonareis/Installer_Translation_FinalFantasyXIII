using System.Net;

namespace Installer.Common.Downloader;

public sealed class ServerApi
{
    private readonly string _url;
    public ServerApi(string url)
    {
        _url = url;
    }
    private static HttpClient ConstructHttpClient()
    {
        HttpClient result = new()
        {
            DefaultRequestVersion = HttpVersion.Version30,
            DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
            Timeout = TimeSpan.FromDays(1)
        };
        return result;
    }

    public async ValueTask<HttpResponseMessage> GetHttpResponse()
    {
        using HttpClient client = ConstructHttpClient();
        HttpResponseMessage httpResponse = await client.GetAsync(_url, HttpCompletionOption.ResponseHeadersRead);

        httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
        return httpResponse;
    }
}