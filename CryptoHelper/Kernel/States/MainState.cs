using Kernel.Strategies.TelegramBotStrategies;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Kernel.States;

public class MainState : UpdateServiceState
{
    public override TelegramBotStrategy GetStrategy(Update update)
    {
       return update.Type switch
       {
           UpdateType.Message or UpdateType.EditedMessage => new MessageStrategy(), 
           UpdateType.CallbackQuery => new CallbackQueryStrategy(), 
           _ => new UnknownUpdateStrategy()
       };
    }
}