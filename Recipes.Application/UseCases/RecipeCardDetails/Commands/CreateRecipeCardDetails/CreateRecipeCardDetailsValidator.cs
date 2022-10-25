using FluentValidation;
using Recipes.Application.UseCases.RecipeCardInfo.Commands.CreateRecipeCardDetails;

namespace Recipes.Application.UseCases.RecipeCardDetails.Commands.CreateRecipeCardDetails
{
    public class CreateRecipeCardDetailsValidator : AbstractValidator<CreateRecipeCardDetailsCommand>
    {
        public CreateRecipeCardDetailsValidator()
        {
            RuleFor(x => x.RecipeInfo.CookingProcess)
                .NotEmpty()
                .WithErrorCode("cookingProcess")
                .WithMessage("cookingProccess should not be null");

            RuleFor(x => x.RecipeInfo.Ingredients)
                .NotNull()
                .WithMessage("Should be at least 1 ingredient")
                .WithErrorCode("ingredient");

            RuleForEach(x => x.RecipeInfo.HashTags)
                .ChildRules(x =>
                {
                    x.RuleFor(x => x)
                        .NotEmpty()
                        .WithMessage("hashtag should not be null")
                        .WithErrorCode("hashtag");
                })
                .When(x => x.RecipeInfo.HashTags?.Any() == true);

            RuleForEach(x => x.RecipeInfo.Ingredients)
                .ChildRules(x =>
                {
                    x.RuleFor(x => x.Name)
                        .NotEmpty()
                        .WithMessage("Ingredient name should not be null")
                        .WithErrorCode("ingredients");
                    x.RuleFor(x => x.Quantity)
                        .NotEmpty()
                        .WithMessage("Ingredient quantity should not be null")
                        .WithErrorCode("ingredients");
                });
        }
    }
}
