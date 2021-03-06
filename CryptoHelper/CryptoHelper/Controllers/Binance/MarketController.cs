using Binance.Common;
using Kernel.Client.Clients;
using Kernel.Common.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHelper.Controllers.Binance;

[Route("binance/market")]
public class MarketController : Controller
{
    private readonly MarketClient _marketClient;
    public MarketController(MarketClient marketClient)
    {
        _marketClient = marketClient;
    }

    [HttpGet("price/{symbol}")]
    public async Task<ActionResult<string>> GetSymbolPriceTicker([FromRoute] string symbol)
    {
        try
        {
            var res = await _marketClient.GetSymbolPriceTicker(symbol);
            return Ok(res.ToFormattedJson());
        }
        catch (BinanceClientException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
    }
}