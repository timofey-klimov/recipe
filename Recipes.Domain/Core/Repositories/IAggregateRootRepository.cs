namespace Recipes.Domain.Core.Repositories
{
    public interface IAggregateRootRepository<T> : IEntityRepository<T>
        where T : AggregateRoot
    {
        Task<T?> GetByIdAsync(int id, CancellationToken token = default);
    }
}
