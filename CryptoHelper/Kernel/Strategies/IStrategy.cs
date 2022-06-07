namespace Kernel.Strategies;

public interface IStrategy<in TAggregate, TResult>
{
    Task<TResult> Execute(TAggregate aggregate);
}