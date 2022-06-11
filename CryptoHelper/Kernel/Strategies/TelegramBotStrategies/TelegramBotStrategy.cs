using Kernel.States;
using Mediator.Mediator;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Kernel.Strategies.TelegramBotStrategies;

public abstract class TelegramBotStrategy : IStrategy<Update, Message>
{
    protected ITelegramBotClient BotClient;
    protected IMediator Mediator;
    public UpdateServiceState State { get; protected set; } = new MainState();

    public abstract Task<Message> Execute(Update aggregate);

    public void SetClient(ITelegramBotClient botClient)
    {
        BotClient ??= botClient;
    }

    public void SetMediator(IMediator mediator)
    {
        Mediator ??= mediator;
    }
}