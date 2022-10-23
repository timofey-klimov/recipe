using Recipes.Domain.Shared;

namespace Recipes.Domain.Exceptions.Core
{
    public class DomainException : Exception
    {
        public DomainException()
        {

        }

        public DomainException(string message)
            : base(message)
        {

        }
    }
}
