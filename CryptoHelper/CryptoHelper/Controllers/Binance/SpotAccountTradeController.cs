using Binance.Spot.Models;
using Kernel.Client.Clients;
using Kernel.Common.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHelper.Controllers.Binance;

[Route("binance/spot")]
public class SpotAccountTradeController : Controller
{
    private readonly SpotAccountTradeClient _client;

    public SpotAccountTradeController(SpotAccountTradeClient client)
    {
        _client = client;
    }

    [HttpGet("orders/{symbol}")]
    public async Task<ActionResult<string>> GetAllOrders([FromRoute] string symbol)
    {
        var res = await _client.AllOrders(symbol);
        return Ok(res.ToFormattedJson());
    }

    [HttpGet("orders/open/{symbol}")]
    public async Task<ActionResult<string>> GetCurrentOpenOrders([FromRoute] string symbol)
    {
        var res = await _client.AllOrders(symbol);
        return Ok(res.ToFormattedJson());
    }

    [HttpPost("order/buy/market/{symbol}/{quantity}")]
    public async Task<ActionResult<string>> BuyMarket([FromRoute] string symbol, [FromRoute] decimal quantity)
    {
        var res = await _client.NewOrder(symbol, Side.BUY, OrderType.MARKET, quantity);
        return Ok(res.ToFormattedJson());
    }

    [HttpPost("order/sell/market/{symbol}/{quantity}")]
    public async Task<ActionResult<string>> SellMarket([FromRoute] string symbol, [FromRoute] decimal quantity)
    {
        var res = await _client.NewOrder(symbol, Side.SELL, OrderType.MARKET, quantity);
        return Ok(res.ToFormattedJson());
    }
}