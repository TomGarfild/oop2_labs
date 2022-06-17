using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Data.Managers;

public class AlertsManager : Manager<AlertData, AlertActionType>
{
    public AlertsManager(DataDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IList<AlertData>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Alerts.Include(a => a.User).Where(a => a.IsActive && !a.IsExecuted).ToListAsync(cancellationToken);
    }

    public override async Task UpdateAsync(AlertData entity, AlertActionType actionType, CancellationToken cancellationToken = default)
    {
        await DbContext.Alerts.AddAsync(entity, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public override async Task<AlertData> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        return await DbContext.Alerts.FirstOrDefaultAsync(x => x.Id == key && x.IsActive, cancellationToken: cancellationToken);
    }

    public override async Task DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        var alert = await GetAsync(key, cancellationToken);

        if (alert == null)
        {
            throw new ArgumentNullException($"Alert with id {key} does not exist");
        }

        alert = alert with { IsActive = false };

        DbContext.Alerts.Update(alert);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}