using Kernel.Builders;
using Kernel.Clients;
using Kernel.Data;
using Kernel.Factories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace Kernel;

public static class AppExtensions
{
    public static void AddKernel(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKernelData(configuration);
        services.AddSingleton<Client>();
        services.AddSingleton<WalletClient>();
        services.AddSingleton<MarketClient>();
        services.AddSingleton<SpotAccountTradeClient>();
        services.AddTransient<TimerBuilder>();
        services.AddSingleton<IFactory<TelegramBotClient>, TelegramBotFactory>();
    }

    public static void UseKernel(this IApplicationBuilder app)
    {
        // app.ApplicationServices.GetRequiredService<TelegramBotHandler>().Start();
    }
}