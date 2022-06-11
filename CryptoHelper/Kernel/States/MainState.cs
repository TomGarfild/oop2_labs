using Kernel.Strategies.TelegramBotStrategies;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Kernel.States;

public class MainState : UpdateServiceState
{
    public override async Task Handle(Update update)
    {
        TelegramBotStrategy strategy = update.Type switch
        {
            UpdateType.Message or UpdateType.EditedMessage => new MessageUpdateStrategy(),
            UpdateType.CallbackQuery => new CallbackQueryUpdateStrategy(),
            _ => new UnknownUpdateStrategy()
        };
        strategy.SetClient(_service.BotClient);
        strategy.SetMediator(_service.Mediator);

        await strategy.Execute(update);
        _service.TransitionTo(strategy.State);
    }
}