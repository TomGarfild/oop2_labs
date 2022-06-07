using Telegram.Bot.Types;

namespace Kernel.Strategies.TelegramBotStrategies;

public class UnknownUpdateStrategy : TelegramBotStrategy
{
    public override Task<Message> Execute(Update aggregate)
    {
        throw new InvalidOperationException($"Unknown update type: {aggregate.Type}");
    }
}