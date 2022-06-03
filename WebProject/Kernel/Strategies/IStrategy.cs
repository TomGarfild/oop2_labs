namespace Kernel.Strategies;

public interface IStrategy<in TAggregate>
{
    Task Execute(TAggregate aggregate);
}