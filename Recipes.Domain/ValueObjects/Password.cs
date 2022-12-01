using Recipes.Domain.Core;
using Recipes.Domain.Core.Errors;
using Recipes.Domain.Shared;

namespace Recipes.Domain.ValueObjects
{
    public class Password : ValueObject
    {
        private const int MinLength = 6;
        public string Value { get; private set; }

        private Password(string value) => Value = value;

        public static Result<Password> Create(string value)
        {
            if (value.Length < MinLength)
            {
                return UserErrors.PasswordMinLength(MinLength);
            }

            return new Password(value);
        }

        protected override IEnumerable<object> GetInternalValues()
        {
            yield return Value;
        }
    }
}
