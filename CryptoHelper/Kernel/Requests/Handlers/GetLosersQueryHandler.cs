using Kernel.Client.Clients;
using Kernel.Domain.Entities;
using Kernel.Requests.Queries;
using Mediator;

namespace Kernel.Requests.Handlers;

public class GetLosersQueryHandler : IRequestHandler<GetLosersQuery, IEnumerable<InternalCryptocurrency>>
{
    private readonly BaseClient _client;

    public GetLosersQueryHandler(BaseClient client)
    {
        _client = client;
    }


    public async Task<IEnumerable<InternalCryptocurrency>> Handle(GetLosersQuery request, CancellationToken cancellationToken)
    {
        var res = await _client.GetLosers(cancellationToken);
        return res.Select(r => new InternalCryptocurrency(r.Id, r.Name, r.Symbol, $"{_client.Url}{r.Slug}"));
    }
}