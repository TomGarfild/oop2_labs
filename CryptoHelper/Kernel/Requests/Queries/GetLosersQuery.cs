using Kernel.Domain.Entities;
using Mediator;

namespace Kernel.Requests.Queries;

public record GetLosersQuery : IRequest<IEnumerable<InternalCryptocurrency>>;