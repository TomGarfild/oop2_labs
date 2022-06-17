namespace Kernel.Data.Specifications;

public interface ISpecification<TEntity> : ISpecification<string, TEntity>
{

}

public interface ISpecification<TKey, TEntity>
{
    
}