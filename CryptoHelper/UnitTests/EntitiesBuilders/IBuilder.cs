namespace UnitTests.EntitiesBuilders;

public interface IBuilder<TAggregate>
{
    public TAggregate Build();
}