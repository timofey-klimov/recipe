using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Core;
using Recipes.Domain.Core.Repositories;

namespace Recipes.Persistance.Repositories.Core
{
    public abstract class EntityRepository<TEntity, TId> : IEntityRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : IEquatable<TId>
    {
        protected ApplicationDbContext DbContext;
        public EntityRepository(ApplicationDbContext applicationDbContext)
        {
            DbContext = applicationDbContext;
        }

        public DbSet<TEntity> Entities() => DbContext.Set<TEntity>();

        public void Add(TEntity entity)
        {
            Entities().Add(entity);
        }

        public void Update(TEntity entity)
        {
            Entities().Update(entity);
        }

        public async Task<int> CountAsync(Specification<TEntity>? spec = null, CancellationToken token = default)
        {
            if (spec == null)
                return await Entities().CountAsync(token);
            else
                return await Entities().Where(spec.Criteria()).CountAsync();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entities().AddRange(entities);
        }

        protected int SkipItemsForPagination(int page, int itemsOnPage) => (page - 1) * itemsOnPage;
    }
}
