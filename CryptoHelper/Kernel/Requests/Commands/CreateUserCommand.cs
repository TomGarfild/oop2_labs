using Mediator;

namespace Kernel.Requests.Commands;

public record CreateUserCommand(long ChatId, string UserName, string FirstName, string LastName) : IRequest<bool>;