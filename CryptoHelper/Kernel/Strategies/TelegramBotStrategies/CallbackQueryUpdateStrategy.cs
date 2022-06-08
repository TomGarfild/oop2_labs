using Kernel.Common;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Kernel.Strategies.TelegramBotStrategies;

public class CallbackQueryUpdateStrategy : TelegramBotStrategy
{
    public override async Task<Message> Execute(Update aggregate)
    {
        var callbackQuery = aggregate.CallbackQuery!;

        switch (callbackQuery.Data)
        {
            case BotOperations.CreateAlert:
                await BotClient.AnswerCallbackQueryAsync(callbackQueryId: callbackQuery.Id);
                var message = "To create alert specify crypto trading pair and price:\n" +
                              "*BTCUSDT:-29500* - alert when BTCUSDT goes lower than 29500\n" +
                              "*ETHUSDT:2000* - alert when ETHUSDT goes higher than 2000\n";
                return await BotClient.SendTextMessageAsync(chatId: callbackQuery.Message!.Chat.Id,
                    text: message, ParseMode.Markdown);
            case BotOperations.ShowAlerts:
                await BotClient.AnswerCallbackQueryAsync(callbackQueryId: callbackQuery.Id);
                return await BotClient.SendTextMessageAsync(chatId: callbackQuery.Message!.Chat.Id,
                    text: $"Existing alerts:");
            default:
                throw new ArgumentException($"Operation {callbackQuery.Data} is unknown");
        }
    }
}