using Kernel.Strategies.TelegramBotStrategies;
using Mediator.Mediator;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Kernel.Services;

public class HandleUpdateService
{
    private readonly ITelegramBotClient _botClient;
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    private readonly AlertsService _alertsService;

    public HandleUpdateService(ITelegramBotClient botClient, IMediator mediator, ILoggerFactory loggerFactory, AlertsService service)
    {
        _botClient = botClient;
        _mediator = mediator;
        _logger = loggerFactory.CreateLogger<HandleUpdateService>();
        _alertsService = service;
    }

    public async Task UpdateAsync(Update update)
    {
        TelegramBotStrategy strategy = update.Type switch
        {
            UpdateType.Message or UpdateType.EditedMessage => new MessageUpdateStrategy(),
            UpdateType.CallbackQuery => new CallbackQueryUpdateStrategy(_alertsService),
            _ => new UnknownUpdateStrategy()
        };
        strategy.SetClient(_botClient);
        strategy.SetMediator(_mediator);

        try
        {
            var sentMessage = await strategy.Execute(update);
            _logger.LogInformation($"The message {sentMessage.MessageId} was sent");
        }
        catch (ApiRequestException ex)
        {
            _logger.LogError($"Telegram API Error:\n[{ex.ErrorCode}]\n{ex.Message}", ex);
            throw;
        }
        catch (ArgumentException ex)
        {
            _logger.LogInformation(ex.Message, ex);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogInformation(ex.Message);
        }
    }
}