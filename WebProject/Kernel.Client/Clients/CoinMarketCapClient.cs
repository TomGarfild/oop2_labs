using System.Net;
using Kernel.Client.Options;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kernel.Client.Clients;

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
        var uri = QueryHelpers.AddQueryString($"{_apiUrl}v1/cryptocurrency/trending/latest",
            new Dictionary<string, string>
            {
                {"limit", "5"}
            });
        var result = await _httpClient.GetAsync(uri, cancellationToken);
        return await result.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<string> GetGainers(CancellationToken cancellationToken)
    {
        var uri = QueryHelpers.AddQueryString($"{_apiUrl}v1/cryptocurrency/trending/gainers-losers",
            new Dictionary<string, string>
            {
                {"limit", "5"},
                {"sort_dir", "asc"}
            });
        var result = await _httpClient.GetAsync(uri, cancellationToken);
        return await result.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<string> GetLosers(CancellationToken cancellationToken)
    {
        var uri = QueryHelpers.AddQueryString($"{_apiUrl}v1/cryptocurrency/trending/gainers-losers",
            new Dictionary<string, string>
            {
                {"limit", "5"},
                {"sort_dir", "desc"}
            });
        var result = await _httpClient.GetAsync(uri, cancellationToken);
        return await result.Content.ReadAsStringAsync(cancellationToken);
    }
}