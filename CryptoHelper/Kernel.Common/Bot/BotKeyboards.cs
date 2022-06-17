using Telegram.Bot.Types.ReplyMarkups;

namespace Kernel.Common.Bot;

public class BotKeyboards
{
    public static InlineKeyboardMarkup Return => new(InlineKeyboardButton.WithCallbackData("Return", BotOperations.Return));
}