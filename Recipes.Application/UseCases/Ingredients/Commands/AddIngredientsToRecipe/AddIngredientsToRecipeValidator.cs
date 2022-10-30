using FluentValidation;

namespace Recipes.Application.UseCases.Ingredients.Commands.AddIngredientsToRecipe
{
    public class AddIngredientsToRecipeValidator 
        : AbstractValidator<AddIngredientsToRecipeCommand>
    {
        public AddIngredientsToRecipeValidator()
        {
            RuleFor(x => x.Items)
                .NotNull()
                .NotNull()
                .WithErrorCode("ingredients")
                .WithMessage("Ingredients should not be null");

            RuleFor(x => x.Items)
                .Must(x => x.Any())
                .WithErrorCode("ingredients")
                .WithMessage("Ingredients should contains at least 1 item");
            RuleForEach(x => x.Items)
                .NotNull()
                .WithErrorCode("ingredients")
                .WithMessage("Ingredients should not be null")
                .ChildRules(ingredient =>
                {
                    ingredient.RuleFor(x => x.Quantity)
                        .NotEmpty()
                        .WithErrorCode("ingredient.quantity")
                        .WithMessage("Quantity should no be null");

                    ingredient.RuleFor(x => x.Name)
                        .NotEmpty()
                        .WithErrorCode("ingredient.name")
                        .WithMessage("Name should not be null");
                });
        }
    }
}
