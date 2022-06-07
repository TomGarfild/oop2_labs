using Kernel.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace CryptoHelper.Controllers.Telegram
{
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
