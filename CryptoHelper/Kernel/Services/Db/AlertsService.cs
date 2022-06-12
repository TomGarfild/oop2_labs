using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Kernel.Data.Managers;
using Kernel.Domain.Entities;

namespace Kernel.Services.Db;

public class AlertsService
{
    private readonly IManager<AlertData, AlertActionType> _manager;

    public AlertsService(IManager<AlertData, AlertActionType> manager)
    {
        _manager = manager;
    }

    public async Task AddAsync(InternalAlert internalAlert)
    {
        var alert = new AlertData(internalAlert.TradingPair, internalAlert.Price, internalAlert.IsLower, internalAlert.UserId);
        await _manager.UpdateAsync(alert, AlertActionType.Created);
    }

    public async Task<IEnumerable<InternalAlert>> Get(long chatId)
    {
        var result = await _manager.GetAllAsync();
        var alerts = result.Where(a => a.User.ChatId == chatId);
        return alerts.Select(r => new InternalAlert(r.TradingPair, r.Price, r.IsLower, r.UserId) { Id = r.Id });
    }
}