using Kernel.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace Kernel.Factories;

public class TelegramBotFactory : IFactory<TelegramBotClient>
{
    private readonly TelegramOptions _options;
    private readonly ILogger _logger;
    public TelegramBotFactory(IOptions<TelegramOptions> options, ILoggerFactory loggerFactory)
    {
        _options = options.Value;
        _logger = loggerFactory.CreateLogger(nameof(TelegramBotFactory));
    }

    public async Task<TelegramBotClient> CreateAsync()
    {
        _logger.LogInformation("Start creating telegram bot");
        var botClient = new TelegramBotClient(_options.ApiToken);

        _logger.LogInformation("Setting up webhook for telegram bot");
        await botClient.SetWebhookAsync($"{_options.Url}/api/{_options.ApiToken}");

        return botClient;
    }
}