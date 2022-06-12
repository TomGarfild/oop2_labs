namespace UnitTests.TestAsync;

internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _enumerator;

    public TestAsyncEnumerator(IEnumerator<T> enumerator)
    {
        this._enumerator = enumerator;
    }

    public T Current => _enumerator.Current;

    public ValueTask DisposeAsync()
    {
        return new ValueTask(Task.Run(() => _enumerator.Dispose()));
    }

    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(_enumerator.MoveNext());
    }
}