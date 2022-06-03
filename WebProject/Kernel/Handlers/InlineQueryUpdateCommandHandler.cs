using Kernel.Domain.Commands;
using Telegram.Bot.Types;

namespace Kernel.Handlers;

internal class InlineQueryUpdateCommandHandler : ICommandHandler<InlineQueryUpdateCommand>
{
    public Task Handle(InlineQueryUpdateCommand command)
    {
        return Task.CompletedTask;
    }
}