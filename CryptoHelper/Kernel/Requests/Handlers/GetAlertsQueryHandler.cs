using Kernel.Domain.Entities;
using Kernel.Requests.Queries;
using Kernel.Services.Db;
using Mediator;

namespace Kernel.Requests.Handlers;

public class GetAlertsQueryHandler : IRequestHandler<GetAlertsQuery, IEnumerable<InternalAlert>>
{
    private readonly AlertsService _alertsService;

    public GetAlertsQueryHandler(AlertsService alertsService)
    {
        _alertsService = alertsService;
    }

    public async Task<IEnumerable<InternalAlert>> Handle(GetAlertsQuery request, CancellationToken cancellationToken)
    {
        var result = await _alertsService.Get(request.ChatId);
        return result.Select(r => new InternalAlert(r.TradingPair, r.Price, r.IsLower, r.UserId));
    }
}