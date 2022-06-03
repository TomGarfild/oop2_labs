using Kernel.Domain.Commands;

namespace Kernel.Handlers;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task Handle(TCommand command);
}