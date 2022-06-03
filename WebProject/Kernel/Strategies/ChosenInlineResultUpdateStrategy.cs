using Telegram.Bot.Types;

namespace Kernel.Strategies;

public class ChosenInlineResultUpdateStrategy : IStrategy<Update>
{
    public async Task Execute(Update aggregate)
    {
        throw new NotImplementedException();
    }
}