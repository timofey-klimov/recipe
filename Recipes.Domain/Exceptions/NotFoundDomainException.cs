using Recipes.Domain.Exceptions.Core;

namespace Recipes.Domain.Exceptions
{
    public class NotFoundDomainException : DomainException
    {
        public NotFoundDomainException(string entityName)
            : base($"{entityName} not found")
        {

        }
    }
}
