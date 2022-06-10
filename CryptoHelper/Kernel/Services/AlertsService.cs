using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Kernel.Data.Managers;
using Kernel.Domain.Entities;

namespace Kernel.Services;

public class AlertsService
{
    private readonly AlertsManager _manager;

    public AlertsService(AlertsManager manager)
    {
        _manager = manager;
    }

    public async Task AddAsync(long chatId, string tradingPair, decimal price)
    {
        var user = new UserData(chatId, "", "", "", true);
        await _manager.UpdateAsync(new AlertData(tradingPair, price, price < 0, false, false, user, user.Id),
            AlertActionType.Created);
    }

    public async Task<IEnumerable<InternalAlert>> GetAsync(long chatId)
    {
        var result = await _manager.GetAlerts(chatId);
        return result.Select(r => new InternalAlert(r.TradingPair, r.Price, r.IsLower));
    }
}