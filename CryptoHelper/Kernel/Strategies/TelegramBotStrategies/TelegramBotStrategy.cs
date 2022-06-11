using Kernel.States;
using Mediator.Mediator;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Kernel.Strategies.TelegramBotStrategies;

public abstract class TelegramBotStrategy : IStrategy<Update, Message>
{
    protected ITelegramBotClient BotClient { get; private set; }
    protected IMediator Mediator { get; private set; }
    public UpdateServiceState State { get; protected set; } = new MainState();

    public abstract Task<Message> Execute(Update aggregate);

    private TelegramBotStrategy Clone()
    {
        return (TelegramBotStrategy)MemberwiseClone();
    }

    public TelegramBotStrategy SetClient(ITelegramBotClient botClient)
    {
        var clone = Clone();
        clone.BotClient = botClient;
        return clone;
    }

    public TelegramBotStrategy SetMediator(IMediator mediator)
    {
        var clone = Clone();
        clone.Mediator = mediator;
        return clone;
    }

    public TelegramBotStrategy SetState(UpdateServiceState state)
    {
        var clone = Clone();
        clone.State = state;
        return clone;
    }
}