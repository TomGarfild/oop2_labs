using Kernel.Commands;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Kernel.Services;

public class HandleUpdateService
{
    public async Task UpdateAsync(Update update)
    {
        ICommand<Update> handler = update.Type switch
        {
            UpdateType.Message or UpdateType.EditedMessage => new MessageUpdateCommand(),
            UpdateType.CallbackQuery => new CallbackQueryUpdateCommand(),
            UpdateType.InlineQuery => new InlineQueryUpdateCommand(),
            UpdateType.ChosenInlineResult => new ChosenInlineResultUpdateCommand(),
            _ => new UnknownUpdateCommand()
        };
    }
}