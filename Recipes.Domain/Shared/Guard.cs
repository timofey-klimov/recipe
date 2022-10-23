using Recipes.Domain.Exceptions;

namespace Recipes.Domain.Shared
{
    public static class Guard
    {
        public static void ThrowBuisnessError(Error error) => throw new BuisnessErrorDomainException(error);

        public static void NotFound<T>(T entity, int id) => throw new NotFoundDomainException<T>(entity, id);
    }
}
