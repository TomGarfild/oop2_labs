using Kernel.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Kernel;

public static class AppExtensions
{
    public static void AddKernel(this IServiceCollection services)
    {
        services.AddSingleton<Client>();
        services.AddSingleton<MarketClient>();
        services.AddTransient<TimerHandler>();
    }

    public static void UseKernel(this IApplicationBuilder app)
    {
    }
}