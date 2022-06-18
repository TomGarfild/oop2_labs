using Lab3.Menu.Commands;

namespace Lab3.Menu;

public class MainMenu : Menu
{
    protected override void InitCommands()
    {
        Commands.AddRange(new List<ICommand>
        {

            new ReturnCommand()
        });
    }

    public override void Start()
    {
        while (true)
        {
            
        }
    }
}