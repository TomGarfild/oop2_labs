using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Handlers;

public class TelegramBotHandler : IDisposable
{
    private readonly ITelegramBotClient _client;
    private readonly ILogger _logger;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public TelegramBotHandler(CancellationTokenSource cancellationTokenSource)
    {
        _client = new TelegramBotClient("{BotKey}");
        _cancellationTokenSource = cancellationTokenSource;
        // _logger = factory.CreateLogger<TelegramBotHandler>();
    }

    public void Start()
    {
        _client.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync,
            new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() }, _cancellationTokenSource.Token);
    }

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
    }

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                await HandleMessage(botClient, update, cancellationToken);
                break;
            case UpdateType.CallbackQuery:
                await HandleCallbackQuery(botClient, update, cancellationToken);
                return;
        }
    }

    private static async Task HandleCallbackQuery(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        var chatId = update.CallbackQuery!.From.Id;
        switch (update.CallbackQuery.Data)
        {
            case "capital":
                return;
            case "deposit":
            case "withdraw":
                return;
        }
    }

    private static async Task HandleMessage(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Message!.Type != MessageType.Text)
            return;

        var chatId = update.Message.Chat.Id;
        var messageText = update.Message.Text;

        switch (messageText)
        {
            case "/start":
                await botClient.SendTextMessageAsync(chatId, "Use menu", cancellationToken: cancellationToken);
                return;
            case "/wallet":
                await botClient.SendTextMessageAsync(chatId, "Wallet", replyMarkup: WalletMenu(), cancellationToken: cancellationToken);
                return;
            case "/":
                return;
        }
    }

    private static InlineKeyboardMarkup WalletMenu()
    {
        return new InlineKeyboardMarkup
        (
            new List<List<InlineKeyboardButton>>
            {
                new() { new InlineKeyboardButton("Capital") { CallbackData = "capital" } },
                new() { new InlineKeyboardButton("Deposit") { CallbackData = "deposit" }, new InlineKeyboardButton("Withdraw") { CallbackData = "withdraw" } }
            }
        );
    }

    private static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}