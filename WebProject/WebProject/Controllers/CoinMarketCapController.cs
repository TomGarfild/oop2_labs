using Kernel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebProject.Controllers;
[Route("CoinMarketCap")]
public class CoinMarketCapController : Controller
{
    private readonly Client _client;
    public CoinMarketCapController(Client client)
    {
        _client = client;
    }

    [HttpGet]
    public async Task<ActionResult<string>> Get()
    {
        var result = await _client.Get();
        dynamic parsedJson = JsonConvert.DeserializeObject(result);
        return Ok(JsonConvert.SerializeObject(parsedJson, Formatting.Indented));
    }
}