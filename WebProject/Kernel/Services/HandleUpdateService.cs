using Kernel.Strategies;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Kernel.Services;

public class HandleUpdateService
{
    public async Task UpdateAsync(Update update)
    {
        IStrategy<Update> strategy = update.Type switch
        {
            UpdateType.Message or UpdateType.EditedMessage => new MessageUpdateStrategy(),
            UpdateType.CallbackQuery => new CallbackQueryUpdateStrategy(),
            UpdateType.InlineQuery => new InlineQueryUpdateStrategy(),
            UpdateType.ChosenInlineResult => new ChosenInlineResultUpdateStrategy(),
            _ => new UnknownUpdateStrategy()
        };

        try
        {
            await strategy.Execute(update);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}