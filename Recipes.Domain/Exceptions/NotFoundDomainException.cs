using Recipes.Domain.Exceptions.Core;

namespace Recipes.Domain.Exceptions
{
    public class NotFoundDomainException<T> : DomainException
    {
        public NotFoundDomainException(T entity, int id)
            : base($"{entity!.GetType().Name} with {id} not found")
        {

        }
    }
}
