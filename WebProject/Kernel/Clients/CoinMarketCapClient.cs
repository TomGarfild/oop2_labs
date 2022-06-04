using Kernel.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace Kernel.Clients;

public class CoinMarketCapClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly ILogger _logger;

    public CoinMarketCapClient(IOptions<ApiOptions> options, ILoggerFactory loggerFactory)
    {
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
        _logger = loggerFactory.CreateLogger<CoinMarketCapClient>();
    }

    public async Task<string> GetTrending(CancellationToken cancellationToken)
    {
        var result = await _httpClient.GetAsync($"{_apiUrl}v1/cryptocurrency/trending/latest", cancellationToken);
        return await result.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<string> GetGainers(CancellationToken cancellationToken)
    {
        var result = await _httpClient.GetAsync($"{_apiUrl}v1/cryptocurrency/trending/gainers-losers", cancellationToken);
        return await result.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<string> GetLosers(CancellationToken cancellationToken)
    {
        var result = await _httpClient.GetAsync($"{_apiUrl}v1/cryptocurrency/trending/gainers-losers", cancellationToken);
        return await result.Content.ReadAsStringAsync(cancellationToken);
    }
}