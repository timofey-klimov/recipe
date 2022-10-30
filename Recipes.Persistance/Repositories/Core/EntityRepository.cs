using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Core;
using Recipes.Domain.Core.Repositories;

namespace Recipes.Persistance.Repositories.Core
{
    public abstract class EntityRepository<T> : IEntityRepository<T>
        where T : Entity
    {
        protected ApplicationDbContext DbContext;
        public EntityRepository(ApplicationDbContext applicationDbContext)
        {
            DbContext = applicationDbContext;
        }

        public DbSet<T> Entities() => DbContext.Set<T>();

        public void Add(T entity)
        {
            Entities().Add(entity);
        }

        public void Update(T entity)
        {
            Entities().Update(entity);
        }

        public async Task<int> CountAsync(CancellationToken token = default)
        {
            return await Entities().CountAsync(token);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Entities().AddRange(entities);
        }
    }
}
