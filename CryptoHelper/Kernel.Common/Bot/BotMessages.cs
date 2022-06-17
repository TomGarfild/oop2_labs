namespace Kernel.Common.Bot;

public class BotMessages
{
    public const string Abort = "Aborted creating alert";
    public const string AlertInstruction = "To create alert specify crypto trading pair and price:\n" + AlertExample;
    public const string ExistingAlerts = "Existing alerts:";
    public const string WrongAlert = "Wrong price. Try again or return:\n" + AlertExample;
    public const string AlertExample = "*BTCUSDT:-29500* - alert when BTCUSDT goes lower than 29500\n" +
                                       "*ETHUSDT:2000* - alert when ETHUSDT goes higher than 2000\n";
}