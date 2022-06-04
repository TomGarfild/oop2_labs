namespace Kernel.Factories;

public interface IFactory<out TProduct>
{
    public TProduct GetProduct();
}