using Kernel.Builders;
using Kernel.Client;
using Kernel.Data;
using Kernel.Factories;
using Kernel.Services;
using Kernel.Services.Db;
using Kernel.States;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace Kernel;

public static class AppExtensions
{
    public class AssemblyClass
    {
    }

    public static void AddKernel(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediator(ServiceLifetime.Singleton, typeof(AssemblyClass).Assembly);
        services.AddKernelData(configuration);
        services.AddKernelClient();
        services.AddTransient<TimerBuilder>();
        services.AddSingleton<IFactory<ITelegramBotClient>, TelegramBotFactory>();
        services.AddHostedService<ConfigureWebhookService>();
        services.AddSingleton<UpdateServiceState, MainState>();
        services.AddSingleton<HandleUpdateService>();
        services.AddSingleton<UsersService>();
        services.AddSingleton<AlertsService>();
    }

    public static void UseKernel(this IApplicationBuilder app)
    {
    }
}