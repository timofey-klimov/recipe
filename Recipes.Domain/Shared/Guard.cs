using Recipes.Domain.Exceptions;

namespace Recipes.Domain.Shared
{
    public static class Guard
    {
        public static void ThrowBuisnessError(Error error) => throw new BuisnessErrorDomainException(error);

        public static void NotFound(string entityName, int id) => throw new NotFoundDomainException(entityName, id);
    }
}
