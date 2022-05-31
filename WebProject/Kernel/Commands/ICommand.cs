namespace Kernel.Commands;

internal interface ICommand<in TArgument>
{
    public Task ExecuteAsync(TArgument argument);
}