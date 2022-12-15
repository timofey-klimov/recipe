using Recipes.Domain.Exceptions;

namespace Recipes.Domain.Shared
{
    public static class Guard
    {
        public static void ThrowBuisnessError(Error error) => throw new BuisnessErrorDomainException(error);

        public static void NotFound(string entityName) => throw new NotFoundDomainException(entityName);

        public static void Forbidden() => throw new ForbiddenException();
    }
}
