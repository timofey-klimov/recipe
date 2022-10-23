using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;

namespace Recipes.Domain.Repositories
{
    public interface IRecipeRepository : IAggregateRootRepository<Recipe>
    {
        Task<Recipe?> GetByIdWithIngredientsAndHashtags(int id, CancellationToken token = default);
    }
}
