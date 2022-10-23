using MediatR;
using Recipes.Contracts;

namespace Recipes.Application.UseCases.Recipes.Commands.CreateRecipe
{
    public record CreateRecipeCommand(RecipeDto Recipe) : IRequest<RecipeDto>;
}
