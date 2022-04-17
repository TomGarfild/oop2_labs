namespace Client.Domain.Common;

public class MenuConst
{
    public const string Main = "| Menu Rock Paper Scissors Game |";
    public const string Room = "|           Room Menu           |";
    public const string Stat = "|        Statistics Menu        |";
    public const string PrivateRoom = "|       Private Room Menu       |";

    public static readonly string[] AuthArgs =
    {
        "|       Register - press R      |",
        "|       Login    - press L      |",
        "|       Exit     - press E      |"
    };

    public static readonly string[] GameArgs =
    {
        "|     Public Room  - press 1    |",
        "|     Private Room - press 2    |",
        "|     Computer     - press 3    |",
        "|     Exit         - press E    |"
    };

    public static readonly string[] RoomArgs =
    {
        "|     Rock       -  press R     |",
        "|     Paper      -  press P     |",
        "|     Scissors   -  press S     |",
        "|     Exit Room  -  press E     |"
    };

    public static readonly string[] StatArgs =
    {
        "|   Local Statistic  - press 1  |",
        "|   Global Statistic - press 2  |",
        "|   Exit             - press E  |"
    };

    public static readonly string[] PrivateRoomArgs =
    {
        "|     Create Room  - press 1    |",
        "|     Enter Room   - press 2    |",
        "|     Exit         - press E    |"
    };
}