using Kernel.Client.Contracts;
using Kernel.Client.Options;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Kernel.Client.Clients;

public class CoinMarketCapClient : BaseClient
{
    private readonly ILogger _logger;

    public CoinMarketCapClient(IOptions<Dictionary<string, ApiOptions>> options, ILoggerFactory loggerFactory) : base(options.Value["CoinMarketCap"])
    {
        _logger = loggerFactory.CreateLogger<CoinMarketCapClient>();
    }

    public override async Task<IEnumerable<Cryptocurrency>> GetTrending(CancellationToken cancellationToken)
    {
        var uri = QueryHelpers.AddQueryString($"{ApiUrl}v1/cryptocurrency/trending/latest",
            new Dictionary<string, string>
            {
                {"limit", "5"}
            });
        var result = await HttpClient.GetAsync(uri, cancellationToken);
        var json = await result.Content.ReadAsStringAsync(cancellationToken);
        return ParseJson(json);
    }

    public override async Task<IEnumerable<Cryptocurrency>> GetGainers(CancellationToken cancellationToken)
    {
        var uri = QueryHelpers.AddQueryString($"{ApiUrl}v1/cryptocurrency/trending/gainers-losers",
            new Dictionary<string, string>
            {
                {"limit", "5"},
                {"sort_dir", "asc"}
            });
        var result = await HttpClient.GetAsync(uri, cancellationToken);
        var json = await result.Content.ReadAsStringAsync(cancellationToken);
        return ParseJson(json);
    }

    public override async Task<IEnumerable<Cryptocurrency>> GetLosers(CancellationToken cancellationToken)
    {
        var uri = QueryHelpers.AddQueryString($"{ApiUrl}v1/cryptocurrency/trending/gainers-losers",
            new Dictionary<string, string>
            {
                {"limit", "5"},
                {"sort_dir", "desc"}
            });
        var result = await HttpClient.GetAsync(uri, cancellationToken);
        var json = await result.Content.ReadAsStringAsync(cancellationToken);
        return ParseJson(json);
    }
}