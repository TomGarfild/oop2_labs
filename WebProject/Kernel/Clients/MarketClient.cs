using Binance.Common;
using Binance.Spot;
using Kernel.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Kernel.Clients;

public class MarketClient
{
    private readonly Market _market;
    private readonly ILogger _logger;

    public MarketClient(IOptions<BinanceApiOptions> options, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(nameof(MarketClient));
        var loggingHandler = new BinanceLoggingHandler(_logger);
        var apiOptions = options.Value;
        _market = new Market(apiOptions.ApiUrl, apiOptions.ApiKey, apiOptions.ApiSecret);
    }

    public async Task<SymbolPrice> GetSymbolPriceTicker(string symbol)
    {
        var res = await _market.SymbolPriceTicker(symbol);
        return JsonConvert.DeserializeObject<SymbolPrice>(res);
    }
}