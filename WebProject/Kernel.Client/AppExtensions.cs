using Kernel.Client.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Kernel.Client;

public static class AppExtensions
{
    public static void AddKernelClient(this IServiceCollection services)
    {
        services.AddSingleton<WalletClient>();
        services.AddSingleton<MarketClient>();
        services.AddSingleton<SpotAccountTradeClient>();
        services.AddSingleton<CoinMarketCapClient>();
    }
}