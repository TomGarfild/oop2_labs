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
            "/trending" => SendTrending(BotClient, message),
            "/gainers" => SendGainers(BotClient, message),
            "/losers" => SendLosers(BotClient, message),
            _ => Usage(BotClient, message)
        };
        var sentMessage = await action;
        return sentMessage;

        static async Task<Message> SendTrending(ITelegramBotClient bot, Message message)
        {
            await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);



            InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithUrl("", ""),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithUrl("", ""),
                    },
                });

            return await bot.SendTextMessageAsync(chatId: message.Chat.Id,
                                                  text: "Choose",
                                                  replyMarkup: inlineKeyboard);
        }

        static async Task<Message> SendGainers(ITelegramBotClient bot, Message message)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(
                new[]
                {
                        new KeyboardButton[] { "1.1", "1.2" },
                        new KeyboardButton[] { "2.1", "2.2" },
                })
            {
                ResizeKeyboard = true
            };

            return await bot.SendTextMessageAsync(chatId: message.Chat.Id,
                                                  text: "Choose",
                                                  replyMarkup: replyKeyboardMarkup);
        }

        static async Task<Message> SendLosers(ITelegramBotClient bot, Message message)
        {
            return await bot.SendTextMessageAsync(chatId: message.Chat.Id,
                                                  text: "Removing keyboard",
                                                  replyMarkup: new ReplyKeyboardRemove());
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