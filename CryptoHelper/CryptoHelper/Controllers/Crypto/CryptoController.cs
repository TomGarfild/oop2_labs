using Kernel.Domain.Entities;
using Kernel.Requests.Queries;
using Mediator.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHelper.Controllers.Crypto;

[Route("[Controller]")]
public class CryptoController : BaseController
{
    public CryptoController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("/trending")]
    public async Task<ActionResult<List<InternalCryptocurrency>>> GetTrending()
    {
        var result = await Send(new GetTrendingQuery());
        return Ok(result);
    }

    [HttpGet("/losers")]
    public async Task<ActionResult<List<InternalCryptocurrency>>> GetLosers()
    {
        var result = await Send(new GetLosersQuery());
        return Ok(result);
    }

    [HttpGet("/gainers")]
    public async Task<ActionResult<List<InternalCryptocurrency>>> GetGainers()
    {
        var result = await Send(new GetGainersQuery());
        return Ok(result);
    }

}