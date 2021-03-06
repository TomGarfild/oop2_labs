using Kernel.Data.Entities;

namespace Kernel.Data.Managers;

public abstract class Manager<TEntityData, TActionType> : IManager<TEntityData, TActionType>
    where TEntityData : BaseEntityData
    where TActionType : Enum
{

    protected readonly DataDbContext DbContext;

    protected Manager(DataDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public abstract Task<TEntityData> GetAsync(string key, CancellationToken cancellationToken = default);
    public abstract Task<IList<TEntityData>> GetAllAsync(CancellationToken cancellationToken = default);

    public abstract Task UpdateAsync(TEntityData entity, TActionType actionType, CancellationToken cancellationToken = default);

    public abstract Task DeleteAsync(string key, CancellationToken cancellationToken = default);
}