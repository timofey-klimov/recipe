using Recipes.Domain.Entities;
using Recipes.Domain.Repositories;
using Recipes.Persistance.Repositories.Core;

namespace Recipes.Persistance.Repositories
{
    public class CookingStageRepository : EntityRepository<CookingStage, int>, ICookingStageRepository
    {
        public CookingStageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
