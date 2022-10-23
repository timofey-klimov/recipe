using FluentValidation.Results;

namespace Recipes.Application.Shared.Exceptions
{
    public class AppValidationException : Exception
    {
        public Dictionary<string, string[]> Errors { get; }
        public AppValidationException(Dictionary<string, string[]> errors)
        {
            Errors = errors;
        }
    }
}
