using System.Linq.Expressions;

namespace Recipes.Domain.Core
{
    public interface IAggregateRootRepository<T> : IEntityRepository<T>
        where T : AggregateRoot
    {
        Task<T?> GetByIdAsync(int id, CancellationToken token = default);
    }
}
