using Kernel.Common.Bot;
using Kernel.Requests.Queries;
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
        await BotClient.SendChatActionAsync(chat.Id, ChatAction.Typing);
        switch (callbackQuery.Data)
        {
            case BotOperations.CreateAlert:
                State = new AddAlertState();
                return await BotClient.SendTextMessageAsync(chat.Id, BotMessages.AlertInstruction, ParseMode.Markdown);
            case BotOperations.ShowAlerts:
                var result = await Mediator.Send(new GetAlertsQuery(chat.Id));
                var keyboard = result.Select(t
                    => new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithUrl($"{t.TradingPair} {IsLower(t.IsLower)} {Math.Abs(t.Price)}", "https://stackoverflow.com/")
                    }).ToList();
                return await BotClient.SendTextMessageAsync(chat.Id, BotMessages.ExistingAlerts, replyMarkup: new InlineKeyboardMarkup(keyboard));
            default:
                throw new ArgumentException($"Operation {callbackQuery.Data} is unknown");
        }

        static string IsLower(bool isLower) => (isLower ? "lower" : "higher") + " than";
    }
}