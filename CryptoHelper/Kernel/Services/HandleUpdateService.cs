using Kernel.States;
using Mediator.Mediator;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace Kernel.Services;

public class HandleUpdateService
{
    public readonly ITelegramBotClient BotClient;
    public readonly IMediator Mediator;
    private readonly ILogger _logger;
    private UpdateServiceState _state;

    public HandleUpdateService(ITelegramBotClient botClient, IMediator mediator, ILoggerFactory loggerFactory, UpdateServiceState state)
    {
        BotClient = botClient;
        Mediator = mediator;
        _logger = loggerFactory.CreateLogger<HandleUpdateService>();
        TransitionTo(state);
    }

    public async Task UpdateAsync(Update update)
    {
        try
        {
            var strategy = _state.GetStrategy(update).SetClient(BotClient).SetMediator(Mediator).SetState(_state);
            await strategy.Execute(update);
            TransitionTo(strategy.State);
        }
        catch (ApiRequestException ex)
        {
            _logger.LogError($"Telegram API Error:\n[{ex.ErrorCode}]\n{ex.Message}", ex);
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

    private void TransitionTo(UpdateServiceState state)
    {
        _state = state;
    }
}