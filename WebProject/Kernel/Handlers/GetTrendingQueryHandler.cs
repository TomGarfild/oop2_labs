using Kernel.Domain.Requests.Queries;
using Kernel.Mediator;

namespace Kernel.Handlers;

internal class GetTrendingQueryHandler : IRequestHandler<GetTrendingQuery, bool>
{


    public Task<bool> Handle(GetTrendingQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}