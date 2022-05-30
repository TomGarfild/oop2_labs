using Kernel.Builders;
using Kernel.Common;
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
    private readonly TimerBuilder _timerHandler;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private long _chatId;
    private CommandType _commandType;
    private string _symbol;

    public TelegramBotHandler(TimerBuilder timerHandler, ILoggerFactory loggerFactory)
    {
        _timerHandler = timerHandler;
        _timerHandler.reachedPrice += OnPriceReached;
        _client = new TelegramBotClient("5139788506:AAHgzrsxIYAsaUmWpnqfmRyXgluolfNwNRA");
        _cancellationTokenSource = new CancellationTokenSource();
        _logger = loggerFactory.CreateLogger<TelegramBotHandler>();
    }

    public void OnPriceReached(object? sender, EventArgs e)
    {
        _client.SendTextMessageAsync(_chatId, "REACHED STOP PRICE!!", cancellationToken: _cancellationTokenSource.Token).Wait();
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

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
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

    private async Task HandleMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message!.Type != MessageType.Text)
            return;

        _chatId = update.Message.Chat.Id;
        var messageText = update.Message.Text;

        if (_commandType == CommandType.Alert)
        {
            if (string.IsNullOrEmpty(_symbol))
            {
                _symbol = messageText;
                await botClient.SendTextMessageAsync(_chatId, "Enter price when notify (with - if below)", replyMarkup: RemoveMenu(), cancellationToken: cancellationToken);
                return;
            }
            if (double.TryParse(messageText, out var price))
            {
                _timerHandler.Start(_symbol, price);
                await botClient.SendTextMessageAsync(_chatId, $"Set stop price {price} for symbol {_symbol}", cancellationToken: cancellationToken);
                _commandType = CommandType.Undefined;
                _symbol = string.Empty;
                return;
            }
        }

        switch (messageText)
        {
            case "/start":
                await botClient.SendTextMessageAsync(_chatId, "Use menu", cancellationToken: cancellationToken);
                return;
            case "/wallet":
                await botClient.SendTextMessageAsync(_chatId, "Wallet", replyMarkup: WalletMenu(), cancellationToken: cancellationToken);
                return;
            case "/alert":
                await HandleAlert(cancellationToken);
                return;
            case "/":
                return;
        }
    }

    private async Task HandleAlert(CancellationToken cancellationToken)
    {
        await _client.SendTextMessageAsync(_chatId, "Chose crypto pair symbol or type it", replyMarkup: SymbolMenu(), cancellationToken: cancellationToken);
        _commandType = CommandType.Alert;
    }
    private static ReplyKeyboardMarkup SymbolMenu()
    {
        return new ReplyKeyboardMarkup
        (
            new List<List<KeyboardButton>>
            {
                new() { new KeyboardButton("BTCUSDT"), new KeyboardButton("ETHUSDT"), new KeyboardButton("BNBUSDT") },
                new() { new KeyboardButton("ETHBTC"), new KeyboardButton("BNBBTC"), new KeyboardButton("XRPBTC") },
                new() { new KeyboardButton("BNBETH"), new KeyboardButton("LTCETH"), new KeyboardButton("SOLETH") }
            }
        );
    }

    private static ReplyKeyboardRemove RemoveMenu()
    {
        return new ReplyKeyboardRemove();
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