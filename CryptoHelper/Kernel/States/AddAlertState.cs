using Kernel.Common;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Kernel.States;

public class AddAlertState : UpdateServiceState
{
    public override async Task Handle(Update update)
    {
        if (update.Type != UpdateType.Message)
            throw new ArgumentException($"Received wrong update type {update.Type}");

        var message = update.Message!;

        if (message.Type != MessageType.Text)
        {
            throw new ArgumentException($"Received wrong message type {message.Type}");
        }

        var chat = message.Chat;
        var pairAndPrice = message.Text!.Split(':').Select(m => m.Trim()).ToArray();

        if (decimal.TryParse(pairAndPrice[1], out var price))
        {
            // await _service.AlertsService.AddAsync(null, pairAndPrice[0], price);
            await _service.BotClient.SendTextMessageAsync(chat.Id, $"Successfully added alert for {message.Text}");
            _service.TransitionTo(new MainState());
        }
        else
        {
            var msg = "Wrong price. Try again or return:\n" +
                          "*BTCUSDT:-29500* - alert when BTCUSDT goes lower than 29500\n" +
                          "*ETHUSDT:2000* - alert when ETHUSDT goes higher than 2000\n";
            await _service.BotClient.SendTextMessageAsync(chat.Id, msg, ParseMode.Markdown, replyMarkup: BotKeyboards.Return);
        }
    }
}