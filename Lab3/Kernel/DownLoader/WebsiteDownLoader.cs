using System.Net;
using System.Threading.Tasks;

namespace Kernel.DownLoader;

public class WebsiteDownLoader : IDownLoader<WebsiteData>
{
    private readonly WebClientWrapper _client;

    public WebsiteDownLoader(WebClientWrapper client)
    {
        _client = client;
    }

    /// <summary>
    /// Download data and format it
    /// </summary>
    /// <param name="url">Url from where download data</param>
    /// <returns>website data from url and data</returns>
    public WebsiteData Download(string url)
    {
        var data = _client.DownloadString(url);
        return new WebsiteData(url, data);
    }

    /// <summary>
    /// Download asynchronously data and format it
    /// </summary>
    /// <param name="url">Url from where download data</param>
    /// <returns>website data from url and data</returns>
    public async Task<WebsiteData> DownloadAsync(string url)
    {
        var data = await _client.DownloadStringTaskAsync(url);
        return new WebsiteData(url, data);
    }
}