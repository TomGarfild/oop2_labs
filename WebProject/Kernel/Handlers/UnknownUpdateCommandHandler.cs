using Kernel.Domain.Commands;
using Telegram.Bot.Types;

namespace Kernel.Handlers;

internal class UnknownUpdateCommandHandler : ICommandHandler<UnknownUpdateCommand>
{
    public Task Handle(UnknownUpdateCommand command)
    {
        throw new NotImplementedException();
    }
}