using Kernel.Factories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace Kernel.Services;

public class ConfigureWebhookService : IHostedService
{
    private readonly IFactory<TelegramBotClient> _botClientFactory;
    private readonly ILogger _logger;

    public ConfigureWebhookService(IFactory<TelegramBotClient> factory, ILoggerFactory loggerFactory)
    {
        _botClientFactory = factory;
        _logger = loggerFactory.CreateLogger(nameof(ConfigureWebhookService));
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start con");
        await _botClientFactory.CreateAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {

    }
}