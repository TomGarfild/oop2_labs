using Kernel;
using Kernel.Data;
using Kernel.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace WebProject.Controllers.Telegram
{
    [Route("api/message")]
    public class TelegramBotController : Controller
    {
        private readonly TelegramBotClient _botClient;
        private readonly DataDbContext _context;

        public TelegramBotController(TelegramBot telegramBot, DataDbContext context)
        {
            _botClient = telegramBot.GetTelegramBot().Result;
            _context = context;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] object update)
        {
            var upd = JsonConvert.DeserializeObject<Update>(update.ToString() ?? string.Empty);
            var chat = upd?.Message?.Chat;

            if (chat == null) return BadRequest();

            var user = new UserData
            {
                ChatId = chat.Id,
                Username = chat.Username,
                FirstName = chat.FirstName,
                LastName = chat.LastName
            };

            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            await _botClient.SendTextMessageAsync(chat.Id, "Successful registration");

            return Ok();
        }
    }
}
