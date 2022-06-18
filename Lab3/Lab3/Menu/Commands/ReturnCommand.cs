namespace Lab3.Menu.Commands;

public class ReturnCommand : ICommand
{
    public string Name => "Return";
    public string Description => "Returns to last menu";
    public void Execute()
    {
        throw new NotImplementedException();
    }
}