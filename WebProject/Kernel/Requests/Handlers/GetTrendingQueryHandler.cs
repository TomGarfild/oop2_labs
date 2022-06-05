using Kernel.Client.Clients;
using Kernel.Domain.Entities;
using Kernel.Requests.Queries;
using Mediator;

namespace Kernel.Requests.Handlers;

public class GetTrendingQueryHandler : IRequestHandler<GetTrendingQuery, IEnumerable<InternalCryptocurrency>>
{
    private readonly BaseClient _client;

    public GetTrendingQueryHandler(BaseClient client)
    {
        _client = client;
    }


    public async Task<IEnumerable<InternalCryptocurrency>> Handle(GetTrendingQuery request, CancellationToken cancellationToken)
    {
        var res = await _client.GetTrending(cancellationToken);
        return res.Select(r => new InternalCryptocurrency(r.Id, r.Name, r.Symbol, r.LastUpdated, $"{_client.Url}{r.Slug}"));
    }
}