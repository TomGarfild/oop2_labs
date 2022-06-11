using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Data.Managers;

public class AlertsManager : Manager<AlertData, AlertActionType>
{
    public AlertsManager(DataDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task UpdateAsync(AlertData entity, AlertActionType actionType)
    {
        await DbContext.AddAsync(entity);
        await DbContext.SaveChangesAsync();
    }

    public override async Task<AlertData> GetAsync(string key)
    {
        return await DbContext.Alerts.FirstOrDefaultAsync(x => x.Id == key);
    }

    public override async Task DeleteAsync(string key)
    {
        var alert = await GetAsync(key);

        if (alert == null)
        {
            throw new ArgumentNullException($"Alert with id {key} does not exist");
        }

        alert = alert with { IsActive = false };

        DbContext.Update(alert);
        await DbContext.SaveChangesAsync();
    }
}