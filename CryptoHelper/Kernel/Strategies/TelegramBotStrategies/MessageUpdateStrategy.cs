using Kernel.Domain.Entities;
using Kernel.Requests.Queries;
using Mediator;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Kernel.Strategies.TelegramBotStrategies;

public class MessageUpdateStrategy : TelegramBotStrategy
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
            "/trending" => Send("🔥 *Trending*", new GetTrendingQuery()),
            "/gainers" => Send("📈 *Gainers*", new GetGainersQuery()),
            "/losers" => Send("📉 *Losers*", new GetLosersQuery()),
            _ => Usage(BotClient, message)
        };
        var sentMessage = await action;
        return sentMessage;

        async Task<Message> Send(string title, IRequest<IEnumerable<InternalCryptocurrency>> query)
        {
            await BotClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            var result = (await Mediator.Send(query)).ToList();

            var keyboard = new List<List<InlineKeyboardButton>>();
            for (var i = 0; i < result.Count; i++)
            {
                keyboard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithUrl($"{result[i].Name}({result[i].Symbol})", result[i].Url) });
            }

            return await BotClient.SendTextMessageAsync(message.Chat.Id, title, ParseMode.MarkdownV2,
                                                  replyMarkup: new InlineKeyboardMarkup(keyboard));
        }

        static async Task<Message> Usage(ITelegramBotClient bot, Message message)
        {
            const string usage = "Usage:\n" +
                                 "/trending - get 5 most trending in 24h\n" +
                                 "/gainers  - get 5 most gainers in 24h\n" +
                                 "/losers   - get 5 most losers in 24h\n";

            return await bot.SendTextMessageAsync(chatId: message.Chat.Id,
                                                  text: usage,
                                                  replyMarkup: new ReplyKeyboardRemove());
        }
    }
}