using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;

namespace Recipes.Domain.Repositories
{
    public interface IIngredientRepository : IAggregateRootRepository<Ingredient>
    {
    }
}
