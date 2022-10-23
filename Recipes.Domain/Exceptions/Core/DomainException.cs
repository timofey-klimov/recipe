using Recipes.Domain.Shared;

namespace Recipes.Domain.Exceptions.Core
{
    public class DomainException : Exception
    {
        public static void ThrowBuisnessError(Error error) => throw new BuisnessErrorDomainException(error);
    }
}
