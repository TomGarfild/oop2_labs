using Kernel.Strategies.TelegramBotStrategies;
using Telegram.Bot.Types;

namespace Kernel.States;

public abstract class UpdateServiceState
{
    public abstract TelegramBotStrategy GetStrategy(Update update);
}