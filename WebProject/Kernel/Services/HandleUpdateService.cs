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
            UpdateType.CallbackQuery => new CallbackQueryUpdateCommandHandler(),
            UpdateType.InlineQuery => new InlineQueryUpdateCommandHandler(),
            UpdateType.ChosenInlineResult => new ChosenInlineResultUpdateCommandHandler(),
            _ => new UnknownUpdateCommand()
        };  
    }
}