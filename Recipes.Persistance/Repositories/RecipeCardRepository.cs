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
                .FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public async Task<IReadOnlyCollection<RecipeCard>> GetRecipesForPageAsync(
            int page, int itemsOnPage, string? search = default, CancellationToken token = default)
        {
            var skip = SkipItemsForPagination(page, itemsOnPage);

            if (!string.IsNullOrEmpty(search))
            {
                return await Entities()
                    .Where(x => x.Title.Contains(search))
                    .Skip(skip)
                    .Take(itemsOnPage)
                    .OrderByDescending(x => x.CreateDate)
                    .ToListAsync();
            } 
            else
            {
                return await Entities()
                    .Skip(skip)
                    .Take(itemsOnPage)
                    .OrderByDescending(x => x.CreateDate)
                    .ToListAsync();
            }
        }

        public async Task<IReadOnlyCollection<RecipeCard>> GetRecipesForQueryAsync(string query, CancellationToken token = default)
        {
            return await Entities()
                .Include(x => x.Ingredients)
                .Where(recipe => 
                    recipe.Ingredients.Any(x => x.Name.Contains(query)) || recipe.Title.Contains(query))
                .Take(10)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }
    }
}
