using FluentValidation;

namespace Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeImage
{
    public class CreateRecipeImageValidator : AbstractValidator<CreateRecipeImageCommand>
    {
        public CreateRecipeImageValidator()
        {
            RuleFor(x => x.File)
                .NotNull()
                .WithErrorCode("image")
                .WithMessage("Image should not be empty");
        }
    }
}
