using Kernel.Strategies.TelegramBotStrategies;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Kernel.Services;

public class HandleUpdateService
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger _logger;

    public HandleUpdateService(ITelegramBotClient botClient, ILoggerFactory loggerFactory)
    {
        _botClient = botClient;
        _logger = loggerFactory.CreateLogger<HandleUpdateService>();
    }

    public async Task UpdateAsync(Update update)
    {
        TelegramBotStrategy strategy = update.Type switch
        {
            UpdateType.Message or UpdateType.EditedMessage => new MessageUpdateStrategy(),
            UpdateType.CallbackQuery => new CallbackQueryUpdateStrategy(),
            UpdateType.InlineQuery => new InlineQueryUpdateStrategy(),
            UpdateType.ChosenInlineResult => new ChosenInlineResultUpdateStrategy(),
            _ => new UnknownUpdateStrategy()
        };
        strategy.SetClient(_botClient);

        try
        {
            await strategy.Execute(update);
        }
        catch (ApiRequestException ex)
        {
            _logger.LogError($"Telegram API Error:\n[{ex.ErrorCode}]\n{ex.Message}", ex);
            throw;
        }
    }
}