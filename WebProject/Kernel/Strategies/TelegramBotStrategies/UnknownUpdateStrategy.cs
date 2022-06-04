using Kernel.Strategies.TelegramBotStrategies;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;

namespace Kernel.Strategies.TelegramBotStrategies;

public class UnknownUpdateStrategy : TelegramBotStrategy
{
    public override Task<Message> Execute(Update aggregate)
    {
        throw new InvalidOperationException($"Unknown update type: {aggregate.Type}");
    }
}