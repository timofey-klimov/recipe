namespace Recipes.Domain.Core
{
    public interface IEntityRepository<T>
        where T: Entity
    {
        void Add(T entity);

        void Update(T entity);
    }
}
