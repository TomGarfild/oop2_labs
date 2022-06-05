using Kernel.Client.Contracts;
using Kernel.Common.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Kernel.Client.Clients.Cached;

public class CashedClient : BaseClient
{
    private readonly IMemoryCache _memoryCache;
    private readonly BaseClient _client;

    public CashedClient(IMemoryCache memoryCache, BaseClient baseClient)
    {
        _memoryCache = memoryCache;
        _client = baseClient;
    }

    public override async Task<IEnumerable<Cryptocurrency>> GetTrending(CancellationToken cancellationToken)
    {
        var cacheName = $"{_client.GetType().Name}_trending";

        if (_memoryCache.TryGetValue<Cached<List<Cryptocurrency>>>(cacheName, out var cached) && !DateTime.UtcNow.IsExpired(cached.ExpirationDateTime))
        {
            return cached.Value;
        }

        var result = (await _client.GetTrending(cancellationToken)).ToList();
        _memoryCache.Set(cacheName, new Cached<List<Cryptocurrency>>(result, DateTime.UtcNow.AddMinutes(10)));
        return result;

    }
}