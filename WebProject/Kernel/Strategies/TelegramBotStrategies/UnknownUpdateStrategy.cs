using Kernel.Strategies.TelegramBotStrategies;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;

namespace Kernel.Strategies.TelegramBotStrategies;

public class UnknownUpdateStrategy : TelegramBotStrategy
{
    public async override Task Execute(Update aggregate)
    {
    }
}