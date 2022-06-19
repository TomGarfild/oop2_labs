using System.Net;
using System.Threading.Tasks;

namespace Kernel;

public class WebClientWrapper
{
    private readonly WebClient _client;

    public WebClientWrapper()
    {
        _client = new WebClient();
    }

    public WebClientWrapper(WebClient webClient)
    {
        _client = webClient;
    }

    public virtual string DownloadString(string address)
    {
        return _client.DownloadString(address);
    }

    public virtual async Task<string> DownloadStringTaskAsync(string address)
    {
        return await _client.DownloadStringTaskAsync(address);
    }
}