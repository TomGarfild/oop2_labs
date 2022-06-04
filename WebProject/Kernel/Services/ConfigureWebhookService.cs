using Kernel.Client.Options;
using Kernel.Factories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Kernel.Services;

public class ConfigureWebhookService : IHostedService
{
    private readonly IFactory<ITelegramBotClient> _botClientFactory;
    private readonly TelegramOptions _options;
    private readonly ILogger _logger;

    public ConfigureWebhookService(IFactory<ITelegramBotClient> factory, IOptions<TelegramOptions> options, ILoggerFactory loggerFactory)
    {
        _botClientFactory = factory;
        _options = options.Value;
        _logger = loggerFactory.CreateLogger(nameof(ConfigureWebhookService));
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start configuring telegram bot client");
        var botClient = _botClientFactory.GetProduct();
        _logger.LogInformation("Set up webhook");
        await botClient.SetWebhookAsync($"{_options.Url}/api/{_options.ApiToken}",
            allowedUpdates: Array.Empty<UpdateType>(), cancellationToken: cancellationToken);

    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var botClient = _botClientFactory.GetProduct();
        _logger.LogInformation("Remove webhook");
        await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
    }
}