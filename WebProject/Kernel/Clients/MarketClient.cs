using Binance.Common;
using Binance.Spot;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
        _market = new Market(new HttpClient(loggingHandler), apiOptions.ApiUrl, apiOptions.ApiKey, apiOptions.ApiSecret);
    }
}