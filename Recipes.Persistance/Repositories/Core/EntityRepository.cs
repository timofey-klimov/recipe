using Recipes.Domain.Core;

namespace Recipes.Persistance.Repositories.Core
{
    public abstract class EntityRepository<T> : IEntityRepository<T>
        where T : Entity
    {
        protected ApplicationDbContext DbContext;
        public EntityRepository(ApplicationDbContext applicationDbContext)
        {
            DbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public void Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            DbContext.Set<T>().Update(entity);
        }
    }
}
