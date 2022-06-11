using Kernel.Domain.Entities;
using Kernel.Requests.Commands;
using Kernel.Services.Db;
using Mediator;

namespace Kernel.Requests.Handlers;

public class CreateAlertCommandHandler : IRequestHandler<CreateAlertCommand, bool>
{
    private readonly AlertsService _alertsService;
    private readonly UsersService _usersService;

    public CreateAlertCommandHandler(AlertsService alertsService, UsersService usersService)
    {
        _alertsService = alertsService;
        _usersService = usersService;
    }

    public async Task<bool> Handle(CreateAlertCommand request, CancellationToken cancellationToken)
    {
        var user = _usersService.GetByChatId(request.ChatId);
        if (user == null) return false;
        await _alertsService.AddAsync(new InternalAlert(request.TradingPair, request.Price, request.Price < 0, user.Id));
        return true;
    }
}