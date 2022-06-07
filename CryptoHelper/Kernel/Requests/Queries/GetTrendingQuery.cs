using Kernel.Domain.Entities;
using Mediator;

namespace Kernel.Requests.Queries;

public record GetTrendingQuery : IRequest<IEnumerable<InternalCryptocurrency>>;