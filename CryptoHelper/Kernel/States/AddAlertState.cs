using Kernel.Strategies.TelegramBotStrategies;
using Kernel.Strategies.TelegramBotStrategies.Alerts;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Kernel.States;

public class AddAlertState : UpdateServiceState
{
    public override TelegramBotStrategy GetStrategy(Update update)
    {
        return update.Type switch
        {
            UpdateType.Message or UpdateType.EditedMessage => new AlertMessageStrategy(),
            UpdateType.CallbackQuery => new AlertCallbackQueryStrategy(),
            _ => new UnknownUpdateStrategy()
        };
    }
}