using FluentValidation;

namespace Recipes.Application.UseCases.RecipeCards.Commands.CreateRecipeCard
{
    public class CreateRecipeCardValidator : AbstractValidator<CreateRecipeCardCommand>
    {
        public CreateRecipeCardValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title should not be null")
                .WithErrorCode("title");

            RuleFor(x => x.File)
                .NotNull()
                .WithMessage("Image should not be null")
                .WithErrorCode("image");
        }
    }
}
