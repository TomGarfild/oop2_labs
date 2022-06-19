using System.Net;
using System.Threading.Tasks;

namespace Lab3.DownLoader;

public class WebsiteDownLoader : IDownLoader<WebsiteData>
{
    private readonly WebClient _client;

    public WebsiteDownLoader()
    {
        _client = new WebClient();
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