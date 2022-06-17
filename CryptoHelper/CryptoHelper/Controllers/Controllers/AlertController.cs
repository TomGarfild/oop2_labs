using Kernel.Domain.Entities;
using Kernel.Services.Db;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHelper.Controllers.Controllers;

[Route("[Controller]")]
public class AlertController : Controller
{
    private readonly AlertsService _alertsService;

    public AlertController(AlertsService alertsService)
    {
        _alertsService = alertsService;
    }

    [HttpGet("{chatId}")]
    public async Task<ActionResult<List<InternalAlert>>> GetForChatId([FromRoute] long chatId)
    {
        var res = await _alertsService.GetByChatId(chatId);
        return Ok(res);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] InternalAlert alert)
    {
        try
        {
            await _alertsService.AddAsync(alert);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return Conflict(ex.Message);
        }
    }
}