namespace Kernel.Data.Entities;

public interface IEntityData<TKey>
{
    public TKey Id { get; init; }
}