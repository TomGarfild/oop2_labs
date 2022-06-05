using Kernel.Client.Clients;
using Kernel.Client.Clients.Cached;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Kernel.Client;

public static class AppExtensions
{
    public static void AddKernelClient(this IServiceCollection services)
    {
        services.AddSingleton<WalletClient>();
        services.AddSingleton<MarketClient>();
        services.AddSingleton<SpotAccountTradeClient>();
        services.AddMemoryCache();
        services.AddSingleton<CoinGeckoClient>();
        services.AddSingleton<BaseClient, CashedClient>(serviceProvider =>
        {
            var memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();
            var coinGeckoClient = serviceProvider.GetRequiredService<CoinGeckoClient>();
            return new CashedClient(memoryCache, coinGeckoClient);
        });
    }
}