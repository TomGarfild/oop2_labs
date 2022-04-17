using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Clients;

public class StatisticClient
{
    private readonly HttpClient _client;
    public StatisticClient(HttpClient httpClient)
    {
        _client = httpClient;
    }

    public async Task<string> GetLocalStatistic()
    {
        var response = await _client.GetAsync($"{_client.BaseAddress?.AbsoluteUri}/statistic/LocalStatistic");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetGlobalStatistic()
    {
        var response = await _client.GetAsync($"{_client.BaseAddress?.AbsoluteUri}/statistic/GlobalStatistic");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}