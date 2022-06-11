using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Data.Managers;

public class AlertsManager : IManager<AlertData, AlertActionType>
{
    private readonly DataDbContext _dbContext;

    public AlertsManager(DataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AlertData>> GetAlerts(long chatId)
    {
        return await _dbContext.Alerts.Where(e => e.User.ChatId == chatId).ToListAsync();
    }

    public async Task UpdateAsync(AlertData entity, AlertActionType actionType)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<AlertData> GetAsync(string key)
    {
        return await _dbContext.Alerts.FirstOrDefaultAsync(x => x.Id == key);
    }

    public async Task DeleteAsync(string key)
    {
        var alert = await GetAsync(key);

        if (alert == null)
        {
            throw new ArgumentNullException($"Alert with id {key} does not exist");
        }

        alert = alert with { IsActive = false };

        _dbContext.Update(alert);
        await _dbContext.SaveChangesAsync();
    }
}