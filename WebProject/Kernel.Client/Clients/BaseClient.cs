using System.Net;
using Kernel.Client.Contracts;
using Kernel.Client.Options;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Kernel.Client.Clients;

public abstract class BaseClient
{
    protected readonly HttpClient HttpClient;
    protected readonly string ApiUrl;

    protected BaseClient(ApiOptions options)
    {
        HttpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })
        {
            BaseAddress = new Uri(options.ApiUrl)
        };
        var headers = options.Headers ?? new Dictionary<string, string>();
        foreach (var (key, value) in headers)
        {
            HttpClient.DefaultRequestHeaders.Add(key, value);
        }
        ApiUrl = options.ApiUrl;
    }

    public virtual Task<IEnumerable<Cryptocurrency>> GetTrending(CancellationToken cancellationToken)
    {
        return Task.FromResult((IEnumerable<Cryptocurrency>)new List<Cryptocurrency>());
    }

    public virtual Task<IEnumerable<Cryptocurrency>> GetGainers(CancellationToken cancellationToken)
    {
        return Task.FromResult((IEnumerable<Cryptocurrency>)new List<Cryptocurrency>());
    }

    public virtual Task<IEnumerable<Cryptocurrency>> GetLosers(CancellationToken cancellationToken)
    {
        return Task.FromResult((IEnumerable<Cryptocurrency>)new List<Cryptocurrency>());
    }

    protected virtual List<Cryptocurrency> ParseJson(string json)
    {
        return JObject.Parse(json)["data"]?["data"]?.Children()
            .Select(c => c.ToObject<Cryptocurrency>()).ToList() ?? new List<Cryptocurrency>();
    }
}