using Kernel.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Data.Managers;

public class AlertsManager
{
    private readonly DataDbContext _dbContext;

    public AlertsManager(DataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AlertData>> GetAlerts(string userId)
    {
        return await _dbContext.Alerts.Where(e => e.UserId == userId).ToListAsync();
    }
}