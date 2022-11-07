﻿using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;

namespace Recipes.Domain.Repositories
{
    public interface IRecipeCardRepository : IAggregateRootRepository<RecipeCard>
    {
        Task<RecipeCard?> GetByIdWithDetailsAsync(int id, CancellationToken token = default);
        Task<RecipeCard?> GetByIdWithIngredientsAsync(int id, CancellationToken token = default);
        Task<RecipeCard?> GetByIdWithStagesAsync(int id, CancellationToken token = default);
        Task<IReadOnlyCollection<RecipeCard>> GetRecipesForPageAsync(
            int page, int itemsOnPage, CancellationToken token = default);
    }
}
