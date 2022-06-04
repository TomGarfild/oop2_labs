using Kernel.Builders;
using Kernel.Data;
using Kernel.Factories;
using Kernel.Services;
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
        services.AddTransient<TimerBuilder>();
        services.AddSingleton<IFactory<ITelegramBotClient>, TelegramBotFactory>();
        services.AddHostedService<ConfigureWebhookService>();
        services.AddSingleton<HandleUpdateService>();
    }

    public static void UseKernel(this IApplicationBuilder app)
    {
    }
}