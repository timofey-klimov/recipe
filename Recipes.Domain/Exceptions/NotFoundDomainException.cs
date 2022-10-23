using Recipes.Domain.Exceptions.Core;

namespace Recipes.Domain.Exceptions
{
    public class NotFoundDomainException : DomainException
    {
        public NotFoundDomainException(string entityName, int id)
            : base($"{entityName} with id {id} not found")
        {

        }
    }
}
