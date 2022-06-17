using Kernel.Domain.Entities;
using Mediator;

namespace Kernel.Requests.Queries;

public record GetAlertsQuery(long ChatId) : IRequest<IEnumerable<InternalAlert>>;