using Recipes.Domain.Core;
using Recipes.Domain.Core.Errors;
using Recipes.Domain.Shared;

namespace Recipes.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; private set; }

        private Email() { }
        protected Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string value)
        {
            if (value.Contains('@') && value.Contains('.'))
            {
                return new Email(value);
            }
            return UserErrors.EmailInvalidFormat();
        }

        protected override IEnumerable<object> GetInternalValues()
        {
            yield return Value;
        }
    }
}
