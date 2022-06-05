using Kernel.Client.Contracts;
using Kernel.Client.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Kernel.Client.Clients;

public class CoinGeckoClient : BaseClient
{
    private readonly ILogger _logger;   

    public CoinGeckoClient(IOptions<Dictionary<string, ApiOptions>> options, ILoggerFactory loggerFactory) : base(options.Value["CoinGecko"])
    {
        _logger = loggerFactory.CreateLogger<CoinGeckoClient>();
    }

    public override async Task<IEnumerable<Cryptocurrency>> GetTrending(CancellationToken cancellationToken)
    {
        var result = await HttpClient.GetAsync($"{ApiUrl}api/v3/search/trending", cancellationToken);
        var json = await result.Content.ReadAsStringAsync(cancellationToken);
        return ParseJson(json);
    }

    protected override List<Cryptocurrency> ParseJson(string json)
    {
        var res = JObject.Parse(json)["coins"]?.Children()
            .Select(c => c["item"]?.ToObject<Cryptocurrency>()).ToList() ?? new List<Cryptocurrency>();
        return res;
    }
}