using Telegram.Bot.Types;

namespace Kernel.Strategies.TelegramBotStrategies;

public class ChosenInlineResultUpdateStrategy : TelegramBotStrategy
{
    public override Task<Message> Execute(Update aggregate)
    {
        throw new NotImplementedException();
    }
}