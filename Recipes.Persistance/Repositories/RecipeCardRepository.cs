using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Persistance.Repositories.Core;

namespace Recipes.Persistance.Repositories
{
    public class RecipeCardRepository : AggregateRootRepository<RecipeCard>, IRecipeCardRepository
    {
        public RecipeCardRepository(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {
        }

        public async Task<RecipeCard?> GetByIdWithImageAsync(int id, CancellationToken token = default)
        {
            return await Entities()
                    .Include(x => x.Image)
                    .FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public async Task<RecipeCard?> GetByIdWithDetailsAsync(int id, CancellationToken token = default)
        {
            return await Entities()
                    .Include(x => x.Details)
                    .FirstOrDefaultAsync(x => x.Id == id, token);
        }
    }
}
