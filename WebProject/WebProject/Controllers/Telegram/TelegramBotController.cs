using Kernel;
using Kernel.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace WebProject.Controllers.Telegram
{
    [Route("api")]
    public class TelegramBotController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Update([FromServices] HandleUpdateService handleUpdateService, [FromBody] Update update)
        {
            await handleUpdateService.UpdateAsync(update);
            return Ok();
        }
    }
}
