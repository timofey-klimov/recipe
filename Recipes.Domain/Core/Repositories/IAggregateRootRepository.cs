namespace Recipes.Domain.Core.Repositories
{
    public interface IAggregateRootRepository<TEntity, TId> : IEntityRepository<TEntity,TId>
        where TEntity : AggregateRoot<TId>
        where TId : IEquatable<TId>
    {
        Task<TEntity?> GetByIdAsync(TId id, CancellationToken token = default);
    }
}
