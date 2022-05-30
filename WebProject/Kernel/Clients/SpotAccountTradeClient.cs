using Binance.Common;
using Binance.Spot;
using Binance.Spot.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kernel.Clients;

public class SpotAccountTradeClient
{
    private readonly SpotAccountTrade _spotAccountTrade;
    private readonly ILogger _logger;
    public SpotAccountTradeClient(IOptions<BinanceApiOptions> options, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<SpotAccountTradeClient>();
        var binanceLoggingHandler = new BinanceLoggingHandler(_logger);
        var httpClient = new HttpClient(binanceLoggingHandler);
        var apiOptions = options.Value;
        _spotAccountTrade = new SpotAccountTrade(httpClient, apiOptions.ApiUrl, apiOptions.ApiKey, apiOptions.SecretKey);
    }

    public async Task<string> NewOrder(string symbol, Side side, OrderType orderType, decimal quantity)
    {
        var res = await _spotAccountTrade.NewOrder(symbol, side, orderType, quantity: quantity);
        return res;
    }

    public async Task<string> AllOrders(string symbol)
    {
        var res = await _spotAccountTrade.AllOrders(symbol);
        return res;
    }

    public async Task<string> CurrentOpenOrders(string symbol)
    {
        var res = await _spotAccountTrade.CurrentOpenOrders(symbol);
        return res;
    }
}