using Kernel.Client.Clients;
using Kernel.Domain.Entities;
using Kernel.Requests.Queries;
using Mediator;

namespace Kernel.Requests.Handlers;

public class GetTrendingQueryHandler : IRequestHandler<GetTrendingQuery, IEnumerable<InternalCryptocurrency>>
{
    private readonly CoinMarketCapClient _client;

    public GetTrendingQueryHandler(CoinMarketCapClient client)
    {
        _client = client;
    }


    public async Task<IEnumerable<InternalCryptocurrency>> Handle(GetTrendingQuery request, CancellationToken cancellationToken)
    {
        var res = await _client.GetTrending(cancellationToken);
        return res.Select(r => new InternalCryptocurrency(r.Id, r.Name, r.Symbol, r.LastUpdated));
    }
}