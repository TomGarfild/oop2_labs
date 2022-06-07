using Kernel.Domain.Entities;
using Mediator;

namespace Kernel.Requests.Queries;

public record GetGainersQuery : IRequest<IEnumerable<InternalCryptocurrency>>;