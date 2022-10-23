namespace Recipes.Domain.Core.Repositories
{
    public interface IEntityRepository<T>
        where T: Entity
    {
        void Add(T entity);

        void Update(T entity);
    }
}
