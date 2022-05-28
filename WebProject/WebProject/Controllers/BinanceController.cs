using Kernel;
using Kernel.Clients;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebProject.Controllers
{
    [Route("Binance")]
    public class BinanceController : Controller
    {
        private readonly Client _client;
        private readonly MarketClient _marketClient;
        public BinanceController(Client client, MarketClient marketClient)
        {
            _client = client;
            _marketClient = marketClient;
        }

        [HttpGet]
        public async Task<ActionResult<string>> AccountStatus()
        {
            var result = await _client.AccountStatus();
            dynamic parsedJson = JsonConvert.DeserializeObject(result);
            return Ok(JsonConvert.SerializeObject(parsedJson, Formatting.Indented));
        }

        [HttpGet("ticker/price")]
        public async Task<IActionResult> GetSymbolPriceTicker(string ticker)
        {
            await _marketClient.GetSymbolPriceTicker(ticker);
            return Ok();
        }
    }
}
