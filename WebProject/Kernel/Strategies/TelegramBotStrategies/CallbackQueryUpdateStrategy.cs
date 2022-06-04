using Telegram.Bot;
using Telegram.Bot.Types;

namespace Kernel.Strategies.TelegramBotStrategies;

public class CallbackQueryUpdateStrategy : TelegramBotStrategy
{
    public override async Task<Message> Execute(Update aggregate)
    {
        var callbackQuery = aggregate.CallbackQuery!;

        await BotClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            text: $"Received {callbackQuery.Data}");

        var sentMessage = await BotClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: $"Received {callbackQuery.Data}");

        return sentMessage;
    }
}