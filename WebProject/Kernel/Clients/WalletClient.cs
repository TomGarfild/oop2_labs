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
        _wallet = new Wallet(new HttpClient(loggingHandler), apiOptions.ApiUrl, apiOptions.ApiKey, apiOptions.ApiSecret);
    }

    public async Task<string> GetCapitalInfo()
    {
        return await _wallet.BnbConvertableAssets();
    }

    public async Task<string> Deposit()
    {
        return await _wallet.BnbConvertableAssets();
    }

    public async Task<string> Withdraw()
    {
        return await _wallet.BnbConvertableAssets();
    }
}