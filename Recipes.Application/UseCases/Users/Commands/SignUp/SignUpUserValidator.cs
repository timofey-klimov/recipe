using FluentValidation;

namespace Recipes.Application.UseCases.Users.Commands.CreateUser
{
    public class SignUpUserValidator : AbstractValidator<SignUpUserCommand>
    {
        public SignUpUserValidator()
        {
            RuleFor(x => x.User)
                .NotNull()
                .WithErrorCode("user")
                .WithMessage("User should not be null");

            RuleFor(x => x.User.Login)
                .NotEmpty()
                .WithErrorCode("login")
                .WithMessage("Login should not be null or empty");

            RuleFor(x => x.User.Password)
                .NotEmpty()
                .WithMessage("User password should not be empty")
                .WithErrorCode("password");

            RuleFor(x => x.User.Password)
                .MinimumLength(6)
                .WithMessage("User password length should be at least 6 symbols")
                .WithErrorCode("password");

            RuleFor(x => x.User.Email)
                .NotEmpty()
                .WithMessage("Email should not be empty")
                .WithErrorCode("email");

            RuleFor(x => x.User.Email)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Wrong email format")
                .WithErrorCode("email");
        }
    }
}
