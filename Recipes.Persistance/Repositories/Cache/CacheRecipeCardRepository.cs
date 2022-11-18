using Microsoft.Extensions.Caching.Memory;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Persistance.Repositories.Cache.Core;
using Recipes.Persistance.Repositories.Core;

namespace Recipes.Persistance.Repositories.Cache
{
    public class CacheRecipeCardRepository : AggregateRootRepository<RecipeCard>, IRecipeCardRepository
    {
        private readonly IRecipeCardRepository _recipeCardRepository;
        private readonly ICachingProvider _cachingProvider;
        public CacheRecipeCardRepository(
            ApplicationDbContext applicationDbContext, 
            IRecipeCardRepository recipeCardRepository,
            ICachingProvider cachingProvider) 
            : base(applicationDbContext)
        {
            _recipeCardRepository = recipeCardRepository;
            _cachingProvider = cachingProvider;
        }

        public override async Task<RecipeCard?> GetByIdAsync(int id, CancellationToken token = default)
        {
            var memoryCache = _cachingProvider.MemoryCache();
            var key = $"recipeCard-{id}";

            return await memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                    return _recipeCardRepository.GetByIdAsync(id, token);
                });
        }

        public async Task<RecipeCard?> GetByIdWithDetailsAsync(int id, CancellationToken token = default)
        {
            var memoryCache = _cachingProvider.MemoryCache();
            var key = $"recipeCardWithDetails-{id}";
            return await memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                    return _recipeCardRepository.GetByIdWithDetailsAsync(id, token);
                });
        }

        public Task<RecipeCard?> GetByIdWithIngredientsAsync(int id, CancellationToken token = default)
        {
            return _recipeCardRepository.GetByIdWithIngredientsAsync(id, token);
        }

        public Task<RecipeCard?> GetByIdWithStagesAsync(int id, CancellationToken token = default)
        {
            return _recipeCardRepository.GetByIdWithStagesAsync(id, token);
        }

        public Task<IReadOnlyCollection<RecipeCard>> GetRecipesForPageAsync(int page, int itemsOnPage, CancellationToken token = default)
        {
            return _recipeCardRepository.GetRecipesForPageAsync(page, itemsOnPage, token);
        }

        public Task<IReadOnlyCollection<RecipeCard>> GetRecipesForQueryAsync(string query, CancellationToken token = default)
        {
            return _recipeCardRepository.GetRecipesForQueryAsync(query, token);
        }
    }
}
