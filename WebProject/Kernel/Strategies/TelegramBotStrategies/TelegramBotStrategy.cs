using Telegram.Bot;
using Telegram.Bot.Types;

namespace Kernel.Strategies.TelegramBotStrategies;

public abstract class TelegramBotStrategy : IStrategy<Update, Message>
{
    protected ITelegramBotClient BotClient;

    public abstract Task<Message> Execute(Update aggregate);

    public void SetClient(ITelegramBotClient botClient)
    {
        BotClient ??= botClient;
    }
}