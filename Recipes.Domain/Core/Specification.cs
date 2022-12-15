using System.Linq.Expressions;

namespace Recipes.Domain.Core
{
    public abstract class Specification<TEntity>
    {
        public abstract Expression<Func<TEntity, bool>> Criteria();

        protected Expression<Func<TEntity, bool>> Empty()
        {
            var parameter = Expression.Parameter(typeof(TEntity));
            var constant = Expression.Constant(true);

            return Expression.Lambda<Func<TEntity, bool>>(constant, parameter);
        }

    }
}
