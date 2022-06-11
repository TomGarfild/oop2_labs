using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Data.Managers;

public class AlertsManager : Manager<AlertData, AlertActionType>
{
    public AlertsManager(DataDbContext dbContext) : base(dbContext)
    {
    }

    public override IEnumerable<AlertData> GetAll()
    {
        return DbContext.Alerts.Include(a => a.User).AsEnumerable();
    }

    public override async Task UpdateAsync(AlertData entity, AlertActionType actionType, CancellationToken cancellationToken = default)
    {
        await DbContext.AddAsync(entity, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public override async Task<AlertData> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        return await DbContext.Alerts.FirstOrDefaultAsync(x => x.Id == key, cancellationToken: cancellationToken);
    }

    public override async Task DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        var alert = await GetAsync(key, cancellationToken);

        if (alert == null)
        {
            throw new ArgumentNullException($"Alert with id {key} does not exist");
        }

        alert = alert with { IsActive = false };

        DbContext.Update(alert);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}