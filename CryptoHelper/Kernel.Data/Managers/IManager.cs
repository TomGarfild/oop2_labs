using Kernel.Data.Entities;

namespace Kernel.Data.Managers;

public interface IManager<in TKey, TEntityData, in TActionType>
    where TEntityData : IEntityData<TKey>
    where TActionType : Enum
{
    public Task<TEntityData> GetAsync(TKey key, CancellationToken cancellationToken = default);
    public IEnumerable<TEntityData> GetAll();
    public Task UpdateAsync(TEntityData entity, TActionType actionType, CancellationToken cancellationToken = default);
    public Task DeleteAsync(TKey key, CancellationToken cancellationToken = default);
}

public interface IManager<TEntityData, in TActionType> : IManager<string, TEntityData, TActionType>
    where TEntityData : IEntityData<string>
    where TActionType : Enum
{
}