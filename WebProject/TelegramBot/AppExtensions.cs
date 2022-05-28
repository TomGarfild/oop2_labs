using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TelegramBot.Handlers;

namespace TelegramBot;

public static class AppExtensions
{
    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddSingleton<TelegramBotHandler>();
    }

    public static void UseHandlers(this IApplicationBuilder app)
    {
        app.ApplicationServices.GetRequiredService<TelegramBotHandler>().Start();
    }
}