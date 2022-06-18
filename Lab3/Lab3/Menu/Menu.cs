using Lab3.Menu.Commands;

namespace Lab3.Menu;

public abstract class Menu
{
    protected readonly List<ICommand> Commands;

    protected Menu()
    {
        Commands = new List<ICommand>();
        Init();
    }

    private void Init()
    {
        InitCommands();
    }

    protected virtual void InitCommands()
    {
        Commands.Add(new ReturnCommand());
    }

    public abstract void Start();
}