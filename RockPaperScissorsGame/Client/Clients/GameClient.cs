using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Clients;

public class GameClient
{
    private readonly HttpClient _httpClient;
    private string Route { get; set; }
    public GameClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public void SetRoute(string route)
    {
        Route = route;
    }

    public void SetSeriesId(string seriesId)
    {
        _httpClient.DefaultRequestHeaders.Remove("x-series");
        _httpClient.DefaultRequestHeaders.Add("x-series", seriesId);
    } 

    public async Task<string> GetResult(string move)
    {
        _httpClient.DefaultRequestHeaders.Remove("x-choice");
        _httpClient.DefaultRequestHeaders.Add("x-choice", move);
        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress?.AbsoluteUri}{Route}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}