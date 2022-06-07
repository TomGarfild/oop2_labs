using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace Kernel.Factories;

public class TelegramBotFactory : IFactory<ITelegramBotClient>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;
    public TelegramBotFactory(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
    {
        _serviceProvider = serviceProvider;
        _logger = loggerFactory.CreateLogger(nameof(TelegramBotFactory));
    }

    public ITelegramBotClient GetProduct()
    {
        _logger.LogInformation("Create telegram bot");
        //var scope = _serviceProvider.CreateScope();
        //var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
        var botClient = _serviceProvider.GetRequiredService<ITelegramBotClient>();
        return botClient;
    }
}