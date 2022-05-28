using Kernel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebProject.Controllers
{
    [Route("Binance")]
    public class BinanceController : Controller
    {
        private readonly Client _client;
        public BinanceController(Client client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<ActionResult<string>> AccountStatus()
        {
            var result = await _client.AccountStatus();
            dynamic parsedJson = JsonConvert.DeserializeObject(result);
            return Ok(JsonConvert.SerializeObject(parsedJson, Formatting.Indented));
        }
    }
}
