using System.Net;
using System.Text.Json.Nodes;
using Kernel.Client.Contracts;
using Kernel.Client.Options;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

    public async Task<IEnumerable<Cryptocurrency>> GetTrending(CancellationToken cancellationToken)
    {
        var uri = QueryHelpers.AddQueryString($"{_apiUrl}v1/cryptocurrency/trending/latest",
            new Dictionary<string, string>
            {
                {"limit", "5"}
            });
        var result = await _httpClient.GetAsync(uri, cancellationToken);
        var json = await result.Content.ReadAsStringAsync(cancellationToken);
        return JObject.Parse(json)["data"]?["data"]?.Children()
            .Select(c => c.ToObject<Cryptocurrency>()).ToList() ?? new List<Cryptocurrency>();
    }

    public async Task<IEnumerable<Cryptocurrency>> GetGainers(CancellationToken cancellationToken)
    {
        var uri = QueryHelpers.AddQueryString($"{_apiUrl}v1/cryptocurrency/trending/gainers-losers",
            new Dictionary<string, string>
            {
                {"limit", "5"},
                {"sort_dir", "asc"}
            });
        var result = await _httpClient.GetAsync(uri, cancellationToken);
        var json = await result.Content.ReadAsStringAsync(cancellationToken);
        return JsonConvert.DeserializeObject<Cryptocurrency[]>(json);
    }

    public async Task<IEnumerable<Cryptocurrency>> GetLosers(CancellationToken cancellationToken)
    {
        var uri = QueryHelpers.AddQueryString($"{_apiUrl}v1/cryptocurrency/trending/gainers-losers",
            new Dictionary<string, string>
            {
                {"limit", "5"},
                {"sort_dir", "desc"}
            });
        var result = await _httpClient.GetAsync(uri, cancellationToken);
        var json = await result.Content.ReadAsStringAsync(cancellationToken);
        return JsonConvert.DeserializeObject<Cryptocurrency[]>(json);
    }
}