using Recipes.Domain.Core;

namespace Recipes.Application.Core.Pagination
{
    public interface IPaginationProvider<TEntity>
    {
        Task<int> ItemsCount();
    }
}
