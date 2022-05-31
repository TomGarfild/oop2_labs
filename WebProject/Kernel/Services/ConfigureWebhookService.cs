using Kernel.Factories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace Kernel.Services;

public class ConfigureWebhookService : IHostedService
{
    private readonly IFactory<TelegramBotClient> _botClientFactory;
    private TelegramBotClient _botClient;
    private readonly ILogger _logger;

    public ConfigureWebhookService(IFactory<TelegramBotClient> factory, ILoggerFactory loggerFactory)
    {
        _botClientFactory = factory;
        _logger = loggerFactory.CreateLogger(nameof(ConfigureWebhookService));
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start configuring telegram bot client");
        _botClient = await _botClientFactory.CreateAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Remove webhook");
        await _botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
    }
}