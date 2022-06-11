using Kernel.Data.Entities;

namespace Kernel.Data.Managers;

public interface IManager<in TKey, TEntityData, in TActionType>
    where TEntityData : IEntityData<TKey>
    where TActionType : Enum
{
    public Task<TEntityData> GetAsync(TKey key);
    public Task UpdateAsync(TEntityData entity, TActionType actionType);
    public Task DeleteAsync(TKey key);
}

public interface IManager<TEntityData, in TActionType> : IManager<string, TEntityData, TActionType>
    where TEntityData : BaseEntityData
    where TActionType : Enum
{
}