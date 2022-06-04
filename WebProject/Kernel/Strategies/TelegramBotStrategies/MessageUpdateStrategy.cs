using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Kernel.Strategies.TelegramBotStrategies;

public class MessageUpdateStrategy : TelegramBotStrategy
{
    public override async Task Execute(Update aggregate)
    {
        
    }
}