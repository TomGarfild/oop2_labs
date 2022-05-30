using System.Net;
using Binance.Common;
using Binance.Spot;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kernel.Clients;

public class WalletClient
{
    private readonly Wallet _wallet;

    public WalletClient(IOptions<BinanceApiOptions> options, ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger(nameof(WalletClient));
        var loggingHandler = new BinanceLoggingHandler(logger);
        var apiOptions = options.Value;
        _wallet = new Wallet(new HttpClient(loggingHandler), apiOptions.ApiUrl, apiOptions.ApiKey, apiOptions.SecretKey);
    }

    public async Task<string> GetCapitalInfo()
    {
        var res = await _wallet.AccountStatus();
        return res;
    }

    public async Task<string> Withdraw(string coin, string address, decimal amount)
    {
        var res = await _wallet.Withdraw(coin, address, amount);
        return res;
    }
}