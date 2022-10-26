using Recipes.Application.Core.Pagination;
using Recipes.Domain.Entities;

namespace Recipes.Infrustructure.Pagination
{
    public class RecipeCardPaginationProvider : IPaginationProvider<RecipeCard>
    {
        public int ItemsCount()
        {
            return 30;
        }
    }
}
