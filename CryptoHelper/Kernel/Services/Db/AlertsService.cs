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
        var isValid = await IsValid(internalAlert);
        // if (!isValid) throw new ConstraintException("You cannot have more than 10 alerts");
        var alert = new AlertData(internalAlert.TradingPair, internalAlert.Price, internalAlert.IsLower, internalAlert.UserId);
        await _manager.UpdateAsync(alert, AlertActionType.Created);
    }

    public async Task<List<InternalAlert>> GetByUserId(string userId)
    {
        var result = await _manager.GetAllAsync();
        var alerts = result.Where(a => a.User.Id == userId)
            .Select(r => new InternalAlert(r.TradingPair, r.Price, r.IsLower, r.UserId) { Id = r.Id }).ToList();
        return alerts;
    }

    public async Task<List<InternalAlert>> GetByChatId(long chatId)
    {
        var result = await _manager.GetAllAsync();
        var alerts = result.Where(a => a.User.ChatId == chatId)
            .Select(r => new InternalAlert(r.TradingPair, r.Price, r.IsLower, r.UserId) { Id = r.Id }).ToList();
        return alerts;
    }

    private async Task<bool> IsValid(InternalAlert alert)
    {
        return (await GetByUserId(alert.UserId)).Count < 10;
    }
}