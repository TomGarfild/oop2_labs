using Kernel.Domain.Commands;

namespace Kernel.Handlers;

internal class CallbackQueryUpdateCommandHandler : ICommandHandler<CallbackQueryUpdateCommand>
{
    public Task Handle(CallbackQueryUpdateCommand command)
    {
        throw new NotImplementedException();
    }
}