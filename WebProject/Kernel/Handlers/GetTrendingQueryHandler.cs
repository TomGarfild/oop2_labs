using Kernel.Domain.Requests.Queries;
using Kernel.Mediator;

namespace Kernel.Handlers;

public class GetTrendingQueryHandler : IRequestHandler<GetTrendingQuery, bool>
{

    public GetTrendingQueryHandler()
    {

    }


    public Task<bool> Handle(GetTrendingQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}