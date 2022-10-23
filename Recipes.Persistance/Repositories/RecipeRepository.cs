using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Persistance.Repositories.Core;

namespace Recipes.Persistance.Repositories
{
    public class RecipeRepository : AgregateRootRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {
        }
    }
}
