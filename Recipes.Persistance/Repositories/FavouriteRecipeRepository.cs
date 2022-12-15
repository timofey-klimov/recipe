using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Persistance.Repositories.Core;

namespace Recipes.Persistance.Repositories
{
    public class FavouriteRecipeRepository : EntityRepository<FavouriteRecipe, int>, IFavouriteRecipeRepository
    {
        public FavouriteRecipeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
