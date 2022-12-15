namespace Recipes.Domain.Core.Repositories
{
    public interface IEntityRepository<TEntity, TId>
        where TEntity: Entity<TId>
        where TId: IEquatable<TId>
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        Task<int> CountAsync(Specification<TEntity>? spec = null, CancellationToken token = default);

        void AddRange(IEnumerable<TEntity> entities);
    }
}
