using Kernel.Common.Bot;
using Kernel.States;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Kernel.Strategies.TelegramBotStrategies.Alerts;

public sealed class AlertCallbackQueryStrategy : TelegramBotStrategy
{
    public override async Task<Message> Execute(Update aggregate)
    {
        var callbackQuery = aggregate.CallbackQuery!;
        var chat = callbackQuery.Message!.Chat;
        await BotClient.AnswerCallbackQueryAsync(callbackQueryId: callbackQuery.Id);
        switch (callbackQuery.Data)
        {
            case BotOperations.Return:
                State = new MainState();
                return await BotClient.SendTextMessageAsync(chat.Id, BotMessages.Abort);
            default:
                throw new ArgumentException($"Operation {callbackQuery.Data} is unknown");
        }
    }
}