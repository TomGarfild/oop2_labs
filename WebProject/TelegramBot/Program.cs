using TelegramBot.Handlers;

namespace TelegramBot;

internal class Program
{
    private static void Main(string[] args)
    {
        var cts = new CancellationTokenSource();
        var telegramBotHandler =  new TelegramBotHandler(cts);
        telegramBotHandler.Start();
        Console.ReadLine();
        cts.Cancel();
    }
}