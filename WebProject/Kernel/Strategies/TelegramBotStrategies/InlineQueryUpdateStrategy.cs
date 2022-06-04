using Telegram.Bot.Types;

namespace Kernel.Strategies.TelegramBotStrategies;

public class InlineQueryUpdateStrategy : TelegramBotStrategy
{
    public override Task<Message> Execute(Update aggregate)
    {
        throw new NotImplementedException();
    }
}