using Kernel.Domain.Entities;
using Kernel.Requests.Commands;
using Kernel.Services.Db;
using Mediator;

namespace Kernel.Requests.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly UsersService _usersService;

    public CreateUserCommandHandler(UsersService usersService)
    {
        _usersService = usersService;
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _usersService.AddAsync(new InternalUser(request.ChatId, request.UserName, request.FirstName,
                request.LastName));
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}