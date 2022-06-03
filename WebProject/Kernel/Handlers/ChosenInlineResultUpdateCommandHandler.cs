using Kernel.Domain.Commands;
using Telegram.Bot.Types;

namespace Kernel.Handlers;

internal class ChosenInlineResultUpdateCommandHandler : ICommandHandler<ChosenInlineResultUpdateCommand>
{
    public Task Handle(ChosenInlineResultUpdateCommand command)
    {
        throw new NotImplementedException();
    }
}