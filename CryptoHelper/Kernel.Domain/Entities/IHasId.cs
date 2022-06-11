namespace Kernel.Domain.Entities;

public interface IHasId : IHasId<string>
{
}

public interface IHasId<TKey>
{
    public TKey Id { get; init; }
}