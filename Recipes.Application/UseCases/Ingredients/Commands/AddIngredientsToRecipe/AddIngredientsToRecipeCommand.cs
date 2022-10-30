using MediatR;
using Recipes.Contracts.Ingredients;

namespace Recipes.Application.UseCases.Ingredients.Commands.AddIngredientsToRecipe
{
    public record AddIngredientsToRecipeCommand(IEnumerable<IngredientDto> Items, int RecipeId)
        : IRequest<IReadOnlyCollection<IngredientDto>>;
}