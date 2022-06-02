using Telegram.Bot.Types;

namespace Kernel.Commands;

internal class CallbackQueryUpdateCommand : ICommand<Update>
{
    public async Task ExecuteAsync(Update callbackQuery)
    {

    }
}