using Kernel.Common;
using Kernel.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Kernel.Strategies.TelegramBotStrategies;

public class CallbackQueryUpdateStrategy : TelegramBotStrategy
{
    private readonly AlertsService _service;
    private readonly Random _random;
    public CallbackQueryUpdateStrategy(AlertsService service)
    {
        _service = service;
        _random = new Random();
    }

    public override async Task<Message> Execute(Update aggregate)
    {
        var callbackQuery = aggregate.CallbackQuery!;
        var chatId = callbackQuery.From.Id;
        switch (callbackQuery.Data)
        {
            case BotOperations.CreateAlert:
                await BotClient.AnswerCallbackQueryAsync(callbackQueryId: callbackQuery.Id);
                var message = "To create alert specify crypto trading pair and price:\n" +
                              "*BTCUSDT:-29500* - alert when BTCUSDT goes lower than 29500\n" +
                              "*ETHUSDT:2000* - alert when ETHUSDT goes higher than 2000\n";

                await _service.AddAsync(chatId, "BTCUSDT", _random.Next());
                return await BotClient.SendTextMessageAsync(chatId: callbackQuery.Message!.Chat.Id,
                    text: message, ParseMode.Markdown);
            case BotOperations.ShowAlerts:
                await BotClient.AnswerCallbackQueryAsync(callbackQueryId: callbackQuery.Id);
                var result = await _service.GetAsync(chatId);
                var keyboard = result.Select(t
                    => new List<InlineKeyboardButton> { InlineKeyboardButton.WithUrl($"{t.TradingPair}:{t.Price}", "https://stackoverflow.com/") }).ToList();
                return await BotClient.SendTextMessageAsync(chatId: callbackQuery.Message!.Chat.Id,
                    text: "Existing alerts:", replyMarkup: new InlineKeyboardMarkup(keyboard));
            default:
                throw new ArgumentException($"Operation {callbackQuery.Data} is unknown");
        }
    }
}