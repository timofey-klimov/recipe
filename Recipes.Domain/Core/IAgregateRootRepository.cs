using System.Linq.Expressions;

namespace Recipes.Domain.Core
{
    public interface IAgregateRootRepository<T> : IEntityRepository<T>
        where T : AgregateRoot
    {
        Task<T?> GetByIdAsync(int id, CancellationToken token = default);
    }
}
