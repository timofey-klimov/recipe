using Recipes.Domain.Core;

namespace Recipes.Application.Core.Pagination
{
    public interface IPaginationProvider<T>
        where T : Entity
    {
        int ItemsCount();
    }
}
