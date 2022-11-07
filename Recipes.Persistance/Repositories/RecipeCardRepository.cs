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

        public async Task<RecipeCard?> GetByIdWithDetailsAsync(int id, CancellationToken token = default)
        {
            return await Entities()
                    .AsSplitQuery()
                    .Include(x => x.Ingredients)
                    .Include(x => x.Stages)
                    .Include(x => x.Hashtags)
                    .FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public async Task<RecipeCard?> GetByIdWithIngredientsAsync(int id, CancellationToken token = default)
        {
            return await Entities()
                .AsSplitQuery()
                .Include(x => x.Ingredients)
                .FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public async Task<RecipeCard?> GetByIdWithStagesAsync(int id, CancellationToken token = default)
        {
            return await Entities()
                .AsSplitQuery()
                .Include(x => x.Stages)
                    .ThenInclude(x => x.Image)
                .FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public async Task<IReadOnlyCollection<RecipeCard>> GetRecipesForPageAsync(
            int page, int itemsOnPage, CancellationToken token = default)
        {
            var skip = SkipItemsForPagination(page, itemsOnPage);

            return await Entities()
                .Skip(skip)
                .Take(itemsOnPage)
                .OrderBy(x => x.CreateDate)
                .ToListAsync();
        }
    }
}
