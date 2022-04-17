using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Clients;

public class AuthClient
{
    private readonly HttpClient _httpClient;
    public AuthClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> Register(StringContent content)
    {
        var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress?.AbsoluteUri}/account/register", content);
        return response;
    }

    public async Task<string> Login(StringContent content)
    {
        var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress?.AbsoluteUri}/account/login", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task LogOut(string token)
    {
        await _httpClient.DeleteAsync($"{_httpClient.BaseAddress?.AbsoluteUri}/account/logout/{token}");
    }
}