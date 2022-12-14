using Recipes.Domain.Exceptions.Core;
using Recipes.Domain.Shared;

namespace Recipes.Domain.Exceptions
{
    public class BuisnessErrorDomainException : DomainException
    {
        private readonly Error _error;

        public BuisnessErrorDomainException(Error error)
        {
            _error = error;
        }

        public string GetDescription() => _error.Description;
        public string GetCode() => _error.Code;

    }
}
