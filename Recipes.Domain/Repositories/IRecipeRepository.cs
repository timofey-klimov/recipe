using Recipes.Domain.Core;
using Recipes.Domain.Entities;

namespace Recipes.Domain.Repositories
{
    public interface IRecipeRepository : IAgregateRootRepository<Recipe>
    {
    }
}
