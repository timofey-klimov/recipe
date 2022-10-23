using Microsoft.EntityFrameworkCore;
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

        public async Task<Recipe?> GetByIdWithIngredientsAndHashtags(int id, CancellationToken token = default)
        {
            return await DbContext.Set<Recipe>()
                 .Include(x => x.Hashtags)
                 .Include(x => x.Ingredients)
                 .FirstOrDefaultAsync(x => x.Id == id, token);
        }
    }
}
