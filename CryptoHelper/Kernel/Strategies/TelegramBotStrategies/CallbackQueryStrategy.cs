using Kernel.Common.Bot;
using Kernel.Data.Entities;
using Kernel.States;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Kernel.Strategies.TelegramBotStrategies;

public sealed class CallbackQueryStrategy : TelegramBotStrategy
{
    public override async Task<Message> Execute(Update aggregate)
    {
        var callbackQuery = aggregate.CallbackQuery!;
        var chat = callbackQuery.Message!.Chat;
        await BotClient.AnswerCallbackQueryAsync(callbackQueryId: callbackQuery.Id);
        switch (callbackQuery.Data)
        {
            case BotOperations.CreateAlert:
                State = new AddAlertState();
                return await BotClient.SendTextMessageAsync(chat.Id, BotMessages.AlertInstruction, ParseMode.Markdown);
            case BotOperations.ShowAlerts:
                // var result = await _service.GetAsync(chat.Id);
                var result = new List<AlertData>();
                var keyboard = result.Select(t
                    => new List<InlineKeyboardButton> { InlineKeyboardButton.WithUrl($"{t.TradingPair}:{t.Price}", "https://stackoverflow.com/") }).ToList();
                return await BotClient.SendTextMessageAsync(chat.Id, BotMessages.ExistingAlerts, replyMarkup: new InlineKeyboardMarkup(keyboard));
            default:
                throw new ArgumentException($"Operation {callbackQuery.Data} is unknown");
        }
    }
}