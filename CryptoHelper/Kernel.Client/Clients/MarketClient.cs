using Binance.Common;
using Binance.Spot;
using Kernel.Client.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kernel.Client.Clients;

public class MarketClient
{
    private readonly Market _market;
    private readonly ILogger _logger;

    public MarketClient(IOptions<BinanceApiOptions> options, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(nameof(MarketClient));
        var loggingHandler = new BinanceLoggingHandler(_logger);
        var httpClient = new HttpClient(loggingHandler);
        var apiOptions = options.Value;
        _market = new Market(httpClient, apiOptions.ApiUrl, apiOptions.ApiKey, apiOptions.SecretKey);
    }

    public async Task<string> GetSymbolPriceTicker(string symbol)
    {
        var res = await _market.SymbolPriceTicker(symbol);
        return res;
    }
}