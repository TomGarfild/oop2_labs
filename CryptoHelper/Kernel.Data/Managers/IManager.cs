using Kernel.Data.Entities;

namespace Kernel.Data.Managers;

public interface IManager<TEntityData, in TActionType>
    where TEntityData : BaseEntityData
    where TActionType : Enum
{
    public Task UpdateAsync(TEntityData entity, TActionType actionType);
    public Task<TEntityData> GetAsync(string key);
}