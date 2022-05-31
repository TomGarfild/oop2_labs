namespace Kernel.Factories;

public interface IFactory<TProduct>
{
    public Task<TProduct> CreateAsync();
}