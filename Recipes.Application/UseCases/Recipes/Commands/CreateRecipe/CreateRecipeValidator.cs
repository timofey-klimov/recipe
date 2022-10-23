using FluentValidation;
using Recipes.Contracts;

namespace Recipes.Application.UseCases.Recipes.Commands.CreateRecipe
{
    public class CreateRecipeValidator : AbstractValidator<CreateRecipeCommand>
    {
        public CreateRecipeValidator()
        {
            RuleFor(x => x.Recipe.Title)
                .NotEmpty()
                .WithMessage("Recipe title should not be null or empty")
                .WithErrorCode("title");

            RuleFor(x => x.Recipe.CookingProcess)
                .NotEmpty()
                .WithMessage("Cooking process should not be null or empty")
                .WithErrorCode("cookingProcess");

            RuleFor(x => x.Recipe.Ingredients)
                .Must(items => items?.Any() == true)
                .WithMessage("Recipe should have at least 1 ingredient")
                .WithErrorCode("ingredients");

            RuleForEach(x => x.Recipe.Ingredients)
                .SetValidator(new IngredientValidator());
        }
    }

    internal class IngredientValidator : AbstractValidator<IngredientDto> 
    {
        public IngredientValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Ingredient name should not be null or empty")
                .WithErrorCode("ingredientName");

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Ingredient quantity should not be null or empty")
                .WithErrorCode("ingredientQuantity");
        }
    }
}
