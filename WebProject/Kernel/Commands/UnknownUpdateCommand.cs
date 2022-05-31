using Telegram.Bot.Types;

namespace Kernel.Commands;

internal class UnknownUpdateCommand : ICommand<Update>
{
    public async Task ExecuteAsync(Update update)
    {

    }
}