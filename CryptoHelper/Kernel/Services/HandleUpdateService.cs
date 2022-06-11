using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Kernel.Data.Managers;
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
            await _state.Handle(update);
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

    public void TransitionTo(UpdateServiceState state)
    {
        _state = state;
        _state.SetContext(this);
    }
}