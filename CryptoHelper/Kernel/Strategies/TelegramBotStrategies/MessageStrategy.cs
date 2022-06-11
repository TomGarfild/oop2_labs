using System.Text;
using Kernel.Common.Bot;
using Kernel.Domain.Entities;
using Kernel.Requests.Queries;
using Mediator;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Kernel.Strategies.TelegramBotStrategies;

public sealed class MessageStrategy : TelegramBotStrategy
{
    public override async Task<Message> Execute(Update aggregate)
    {
        var message = aggregate.Message!;

        if (message.Type != MessageType.Text)
        {
            throw new ArgumentException($"Received wrong message type {message.Type}");
        }

        var action = message.Text!.Split(' ')[0] switch
        {
            BotCommands.Trending => Send("🔥 *Trending*", new GetTrendingQuery()),
            BotCommands.Gainers => Send("📈 *Gainers*", new GetGainersQuery()),
            BotCommands.Losers => Send("📉 *Losers*", new GetLosersQuery()),
            BotCommands.Alerts => SendAlertsMenu(),
            _ => Usage(BotClient, message)
        };
        var sentMessage = await action;
        return sentMessage;

        async Task<Message> Send(string title, IRequest<IEnumerable<InternalCryptocurrency>> query)
        {
            await BotClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            var result = (await Mediator.Send(query)).ToList();

            var keyboard = result.Select(t
                => new List<InlineKeyboardButton> { InlineKeyboardButton.WithUrl($"{t.Name}({t.Symbol})", t.Url) }).ToList();

            return await BotClient.SendTextMessageAsync(message.Chat.Id, title, ParseMode.MarkdownV2,
                                                        replyMarkup: new InlineKeyboardMarkup(keyboard));
        }

        async Task<Message> SendAlertsMenu()
        {
            var keyboard = new List<List<InlineKeyboardButton>> { new()
            {
                InlineKeyboardButton.WithCallbackData("Create alert", BotOperations.CreateAlert),
                InlineKeyboardButton.WithCallbackData("Show alerts", BotOperations.ShowAlerts)
            } };

            return await BotClient.SendTextMessageAsync(message.Chat.Id, "🔔 *Alerts*", ParseMode.MarkdownV2,
                                                        replyMarkup: new InlineKeyboardMarkup(keyboard));
        }

        static async Task<Message> Usage(ITelegramBotClient bot, Message message)
        {
            var botCommands = await bot.GetMyCommandsAsync();
            var strBuilder = new StringBuilder("Usage:\n");
            foreach (var botCommand in botCommands)
            {
                strBuilder.AppendLine($"/{botCommand.Command} - {botCommand.Description}");
            }

            return await bot.SendTextMessageAsync(chatId: message.Chat.Id, text: strBuilder.ToString(),
                                                  replyMarkup: new ReplyKeyboardRemove());
        }
    }
}