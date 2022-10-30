namespace Recipes.Domain.Core.Repositories
{
    public interface IEntityRepository<T>
        where T: Entity
    {
        void Add(T entity);

        void Update(T entity);

        Task<int> CountAsync(CancellationToken token = default);

        void AddRange(IEnumerable<T> entities);
    }
}
