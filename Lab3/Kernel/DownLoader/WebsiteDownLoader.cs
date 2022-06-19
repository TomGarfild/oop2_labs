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

    public WebsiteData Download(string url)
    {
        var data = _client.DownloadString(url);
        return new WebsiteData(url, data);
    }

    public async Task<WebsiteData> DownloadAsync(string url)
    {
        var data = await _client.DownloadStringTaskAsync(url);
        return new WebsiteData(url, data);
    }
}