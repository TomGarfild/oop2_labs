using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Clients;

public class SeriesClient
{
    private readonly HttpClient _httpClient;
    public SeriesClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<HttpResponseMessage> GetSeries(string route)
    {
        return _httpClient.GetAsync($"{_httpClient.BaseAddress?.AbsoluteUri}{route}");
        
    }

    public async Task<string> GetPrivateSeries()
    {
        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress?.AbsoluteUri}/series/NewPrivateSeries");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public void SetRoomCode(string entranceCode)
    {
        _httpClient.DefaultRequestHeaders.Remove("x-code");
        _httpClient.DefaultRequestHeaders.Add("x-code", entranceCode);
    }
}