using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;

namespace Kernel.Strategies.TelegramBotStrategies;

public class InlineQueryUpdateStrategy : TelegramBotStrategy
{
    public override Task<Message> Execute(Update aggregate)
    {
        throw new NotImplementedException();
    }
}