using Kernel.Client.Clients;
using Kernel.Domain.Entities;
using Kernel.Requests.Queries;
using Mediator;

namespace Kernel.Requests.Handlers;

public class GetGainersQueryHandler : IRequestHandler<GetGainersQuery, IEnumerable<InternalCryptocurrency>>
{
    private readonly CoinMarketCapClient _client;

    public GetGainersQueryHandler(CoinMarketCapClient client)
    {
        _client = client;
    }


    public async Task<IEnumerable<InternalCryptocurrency>> Handle(GetGainersQuery request, CancellationToken cancellationToken)
    {
        var res = await _client.GetGainers(cancellationToken);
        return res.Select(r => new InternalCryptocurrency(r.Id, r.Name, r.Symbol, r.LastUpdated));
    }
}