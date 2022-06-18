namespace Lab3.Menu.Commands;

public interface ICommand
{
    public string Name { get; }
    public string Description { get; }
    public void Execute();
}