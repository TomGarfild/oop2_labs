using Binance.Common;
using Binance.Spot;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace Kernel;

public class Client
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly Wallet _wallet;

    public Client(IOptions<ApiOptions> options, ILoggerFactory loggerFactory)
    {
        #region
        var apiOptions = options.Value;
        _httpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })
        {
            BaseAddress = new Uri(apiOptions.ApiUrl)
        };
        foreach (var (key, value) in apiOptions.Headers)
        {
            _httpClient.DefaultRequestHeaders.Add(key, value);
        }
        _apiUrl = apiOptions.ApiUrl;
        #endregion
    }

    public async Task<string> Get()
    {
        var result = await _httpClient.GetAsync($"{_apiUrl}v1/cryptocurrency/listings/latest");
        return await result.Content.ReadAsStringAsync();
    }

    public async Task<string> AccountStatus()
    {
        return await _wallet.BnbConvertableAssets();
    }
}