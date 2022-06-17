using Kernel.Domain.Entities;
using Kernel.Services.Db;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHelper.Controllers.Controllers;

[Route("[Controller]")]
public class UserController : Controller
{
    private readonly UsersService _usersService;

    public UserController(UsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet("{chatId}")]
    public async Task<ActionResult<InternalUser>> GetUser([FromRoute]long chatId)
    {
        var user = await _usersService.GetByChatId(chatId);
        return user != null ? Ok(user) : NotFound();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody]InternalUser user)
    {
        try
        {
            await _usersService.AddAsync(user);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return Conflict(ex.Message);
        }
    }
}