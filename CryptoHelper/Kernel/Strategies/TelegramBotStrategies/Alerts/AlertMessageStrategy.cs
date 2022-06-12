using Kernel.Common.Bot;
using Kernel.Requests.Commands;
using Kernel.States;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Kernel.Strategies.TelegramBotStrategies.Alerts;

public sealed class AlertMessageStrategy : TelegramBotStrategy
{
    public override async Task<Message> Execute(Update update)
    {
        var message = update.Message!;

        if (message.Type != MessageType.Text)
        {
            throw new ArgumentException($"Received wrong message type {message.Type}");
        }

        var chat = message.Chat;
        await BotClient.SendChatActionAsync(chat.Id, ChatAction.Typing);
        var pairAndPrice = message.Text!.Split(':').Select(m => m.Trim()).ToArray();
        Message sentMessage;

        if (decimal.TryParse(pairAndPrice[1], out var price))
        {
            await Mediator.Send(new CreateAlertCommand(chat.Id, pairAndPrice[0], price));
            sentMessage = await BotClient.SendTextMessageAsync(chat.Id, $"Successfully added alert for {message.Text}");
            State = new MainState();
        }
        else
        {
            sentMessage = await BotClient.SendTextMessageAsync(chat.Id, BotMessages.WrongAlert, ParseMode.Markdown, replyMarkup: BotKeyboards.Return);
        }
        return sentMessage;


    }
}