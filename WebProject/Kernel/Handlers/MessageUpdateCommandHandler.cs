using Kernel.Domain.Commands;
using Telegram.Bot.Types;

namespace Kernel.Handlers;

internal class MessageUpdateCommandHandler : ICommandHandler<MessageUpdateCommand>
{
    public Task Handle(MessageUpdateCommand command)
    {
        throw new NotImplementedException();
    }
}