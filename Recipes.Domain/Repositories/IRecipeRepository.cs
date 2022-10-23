using Recipes.Domain.Core;
using Recipes.Domain.Entities;

namespace Recipes.Domain.Repositories
{
    public interface IRecipeRepository : IAgregateRootRepository<Recipe>
    {
        Task<Recipe?> GetByIdWithIngredientsAndHashtags(int id, CancellationToken token = default);
    }
}
