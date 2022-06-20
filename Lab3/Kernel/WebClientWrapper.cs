using System.Net;
using System.Threading.Tasks;

namespace Kernel;

/// <summary>
/// Wrapper for web client
/// </summary>
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

    /// <summary>
    /// Wrapped method download string
    /// </summary>
    /// <param name="address">From where download</param>
    /// <returns>Result string</returns>
    public virtual string DownloadString(string address)
    {
        return _client.DownloadString(address);
    }

    /// <summary>
    /// Wrapped method for download string async
    /// </summary>
    /// <param name="address">From where download asynchronously</param>
    /// <returns>Result string</returns>
    public virtual async Task<string> DownloadStringTaskAsync(string address)
    {
        return await _client.DownloadStringTaskAsync(address);
    }
}