using FluentValidation;

namespace Recipes.Application.UseCases.Users.Commands.SignIn
{
    public class SignInUserValidator : AbstractValidator<SignInUserCommand>
    {
        public SignInUserValidator()
        {
            RuleFor(x => x.User.Password)
                .NotEmpty()
                .WithMessage("Password should not be empty")
                .WithErrorCode("password");

            RuleFor(x => x.User.Credential)
                .NotEmpty()
                .WithMessage("Credential should not be empty")
                .WithErrorCode("credential");
        }
    }
}
