using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Kernel.Data.Managers;
using Kernel.Domain.Entities;

namespace Kernel.Services;

public class AlertsService
{
    private readonly IManager<AlertData, AlertActionType> _manager;

    public AlertsService(IManager<AlertData, AlertActionType> manager)
    {
        _manager = manager;
    }

    public async Task AddAsync(InternalUser user, string tradingPair, decimal price)
    {
        var alert = new AlertData(tradingPair, price, price < 0, user.Id);
        await _manager.UpdateAsync(alert, AlertActionType.Created);
    }

    public async Task<IEnumerable<InternalAlert>> GetAsync(long chatId)
    {
        // var result = await _manager.GetAlerts(chatId);
        var result = new List<AlertData>();
        return result.Select(r => new InternalAlert(r.Id, r.TradingPair, r.Price, r.IsLower));
    }
}