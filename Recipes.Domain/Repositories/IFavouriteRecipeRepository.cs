using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain.Repositories
{
    public interface IFavouriteRecipeRepository : IEntityRepository<FavouriteRecipe, int>
    {

    }
}
