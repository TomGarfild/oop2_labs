using Kernel.Common;
using Kernel.Data.Entities;
using Kernel.Services;
using Kernel.States;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Kernel.Strategies.TelegramBotStrategies;

public class CallbackQueryUpdateStrategy : TelegramBotStrategy
{
    public override async Task<Message> Execute(Update aggregate)
    {
        var callbackQuery = aggregate.CallbackQuery!;
        var chat = callbackQuery.Message!.Chat;
        switch (callbackQuery.Data)
        {
            case BotOperations.CreateAlert:
                await BotClient.AnswerCallbackQueryAsync(callbackQueryId: callbackQuery.Id);
                var message = "To create alert specify crypto trading pair and price:\n" +
                              "*BTCUSDT:-29500* - alert when BTCUSDT goes lower than 29500\n" +
                              "*ETHUSDT:2000* - alert when ETHUSDT goes higher than 2000\n";
                State = new AddAlertState();
                return await BotClient.SendTextMessageAsync(chatId: chat.Id, text: message, ParseMode.Markdown);
            case BotOperations.ShowAlerts:
                await BotClient.AnswerCallbackQueryAsync(callbackQueryId: callbackQuery.Id);
                // var result = await _service.GetAsync(chat.Id);
                var result = new List<AlertData>();
                var keyboard = result.Select(t
                    => new List<InlineKeyboardButton> { InlineKeyboardButton.WithUrl($"{t.TradingPair}:{t.Price}", "https://stackoverflow.com/") }).ToList();
                return await BotClient.SendTextMessageAsync(chatId: chat.Id,
                    text: "Existing alerts:", replyMarkup: new InlineKeyboardMarkup(keyboard));
            default:
                throw new ArgumentException($"Operation {callbackQuery.Data} is unknown");
        }
    }
}