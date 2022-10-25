using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Persistance.Repositories.Core;

namespace Recipes.Persistance.Repositories
{
    public class RecipeCardDetailRepository : EntityRepository<RecipeCardDetails>, IRecipeCardIDetailRepository
    {
        public RecipeCardDetailRepository(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {
        }
    }
}
